using System;
using System.IO;
using Xceed.Words.NET;

namespace VigenereEncoder
{
    public static class ExportManager
    {
        private delegate void Exporter(MainFormResponse response, String text);

        // MainForm самостоятельно выводит текст на экран, OnScreenExporter необходим для унификации экспорта
        private static readonly Exporter OnScreenExporter = (response, text) => { };

        private static readonly Exporter TxtExporter = (response, text) =>
        {
            using(StreamWriter writer = new StreamWriter(new FileStream(response.OutputFilePath, FileMode.Create), GlobalConstants.Encoding))
            {
                writer.Write(text);
            }
        };

        private static readonly Exporter DocxExporter = (response, text) =>
        {
            DocX docx = DocX.Create(response.OutputFilePath);
            docx.InsertParagraph(text);
            docx.Save();
        };

        public static Boolean Export(MainFormResponse response, String text, out String errorMessage)
        {
            const String defaultEmptyPathErrorMessage = "Путь сохранения не указан.";
            errorMessage = null;
            if(Enum.TryParse(response.OutputType, out ExporterType type))
                try
                {
                    if (type != ExporterType.OnScreen && String.IsNullOrEmpty(response.OutputFilePath))
                    {
                        errorMessage = defaultEmptyPathErrorMessage;
                        return false;
                    }
                    GetImporter(type).Invoke(response, text);
                    return true;
                }
                catch(Exception e)
                {
                    errorMessage = e.Message;
                }
            else
                errorMessage = GetUnknownTypeMessage(response.InputType);
            return false;
        }

        private static Exporter GetImporter(ExporterType type)
        {
            switch(type)
            {
                case ExporterType.OnScreen:
                    return OnScreenExporter;
                case ExporterType.Txt:
                    return TxtExporter;
                case ExporterType.Docx:
                    return DocxExporter;
                default:
                    throw new ArgumentException(GetUnknownTypeMessage(type.ToString()));
            }
        }

        private static String GetUnknownTypeMessage(String type)
        {
            return $"Неизвестный способ вывода: {type}.";
        }
    }
}