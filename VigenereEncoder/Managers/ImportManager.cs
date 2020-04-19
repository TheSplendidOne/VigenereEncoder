using System;
using System.IO;
using System.Text;
using Xceed.Words.NET;

namespace VigenereEncoder
{
    public static class ImportManager
    {
        private delegate String Importer(MainFormResponse response);

        private static readonly Importer UserInputImporter = (response) => response.Input;

        private static readonly Importer TxtImporter = (response) =>
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(response.InputFilePath), Encoding.Default))
            {
                return reader.ReadToEnd();
            }
        };

        private static readonly Importer DocxImporter = (response) => DocX.Load(response.InputFilePath).Text;

        public static Boolean Import(MainFormResponse response, out String text, out String errorMessage)
        {
            text = null;
            errorMessage = null;
            if (Enum.TryParse(response.InputType, true, out ImporterType type))
                try
                {
                    text = GetImporter(type).Invoke(response);
                    if (!String.IsNullOrEmpty(text)) return true;
                    errorMessage = "Текст не может быть пустой строкой.";
                    return false;
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            else
                errorMessage = GetUnknownTypeMessage(response.InputType);
            return false;
        }

        private static Importer GetImporter(ImporterType type)
        {
            switch(type)
            {
                case ImporterType.UserInput:
                    return UserInputImporter;
                case ImporterType.Txt:
                    return TxtImporter;
                case ImporterType.Docx:
                    return DocxImporter;
                default:
                    throw new ArgumentException(GetUnknownTypeMessage(type.ToString()));
            }
        }

        private static String GetUnknownTypeMessage(String type)
        {
            return $"Неизвестный способ ввода: {type}.";
        }
    }
}