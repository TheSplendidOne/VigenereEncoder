using System;

namespace VigenereEncoder
{
    public class MainFormResponse
    {
        public String KeyPattern { get; set; }

        public String InputType { get; set; }

        public String InputFilePath { get; set; }

        public String OutputType { get; set; }

        public String OutputFilePath { get; set; }

        public String Input { get; set; }

        public String Output { get; set; }
    }
}