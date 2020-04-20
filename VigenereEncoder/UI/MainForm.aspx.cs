using System;
using System.IO;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI;

namespace VigenereEncoder
{
    public partial class MainForm : Page
    {
        private String DefaultKeyPattern => KeyPattern.Attributes["placeholder"];

        protected void EncodeOnClick(Object sender, EventArgs e)
        {
            Handle(GetResponse(), VigenereHandler.Encode);
        }

        protected void DecodeOnClick(Object sender, EventArgs e)
        {
            Handle(GetResponse(), VigenereHandler.Decode);
        }

        private void Handle(MainFormResponse response, Func<String, String, String> handler)
        {
            if(ImportManager.Import(response, out String text, out String errorMessage) &&
               ValidateKeyPattern(response.KeyPattern, out String keyPattern, out errorMessage))
            {
                Input.Value = text;
                String handled = handler.Invoke(text, keyPattern);
                Output.Value = handled;
                Message.InnerText = ExportManager.Export(response, handled, out errorMessage)
                    ? GetSuccessfulConversionMessage(response)
                    : errorMessage;
            }
            else
                Message.InnerText = errorMessage;
        }

        private MainFormResponse GetResponse()
        {
            MainFormResponse response = new MainFormResponse();
            TryUpdateModel(response, new FormValueProvider(ModelBindingExecutionContext));
            ExporterType type = (ExporterType)Enum.Parse(typeof(ExporterType), response.InputType, true);
            if (type == ExporterType.Txt || type == ExporterType.Docx)
                response.InputFileStream = new Lazy<Stream>(() => InputFile.PostedFile.InputStream);
            return response;
        }

        private Boolean ValidateKeyPattern(String keyPattern, out String validatedKeyPattern, out String errorMessage)
        {
            validatedKeyPattern = null;
            errorMessage = null;
            if (String.IsNullOrEmpty(keyPattern))
            {
                validatedKeyPattern = DefaultKeyPattern;
                return true;
            }
            if (!keyPattern.ToLower().Except(VigenereHandler.Alphabet).Any())
            {
                validatedKeyPattern = keyPattern;
                return true;
            }
            errorMessage = "Ключ может содержать только символы русского алфавита.";
            return false;
        }

        private static String GetSuccessfulConversionMessage(MainFormResponse response)
        {
            ExporterType type = (ExporterType)Enum.Parse(typeof(ExporterType), response.OutputType, true);
            if (type == ExporterType.Txt || type == ExporterType.Docx)
                return $"Текст успешно преобразован и сохранён в {response.OutputFilePath}.";
            return "Текст успешно преобразован.";
        }
    }
}