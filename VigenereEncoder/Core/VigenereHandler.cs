using System;
using System.Linq;
using System.Text;

namespace VigenereEncoder
{
    public static class VigenereHandler
    {
        private delegate Char VigenereConverter(Char inputChar, Char keyChar);

        public static readonly String Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        private static readonly VigenereConverter EncodeConverter =
            (inputChar, keyChar) => Alphabet[(Alphabet.IndexOf(inputChar) + Alphabet.IndexOf(keyChar)) % Alphabet.Length];

        private static readonly VigenereConverter DecodeConverter =
            (inputChar, keyChar) => Alphabet[(Alphabet.IndexOf(inputChar) - Alphabet.IndexOf(keyChar) + Alphabet.Length) % Alphabet.Length];

        public static String Encode(String text, String keyPattern)
        {
            Validate(text);
            return Convert(text, keyPattern, EncodeConverter);
        }

        public static String Decode(String text, String keyPattern)
        {
            Validate(text);
            return Convert(text, keyPattern, DecodeConverter);
        }

        private static void Validate(String text)
        {
            if(text == null)
                throw new ArgumentNullException("Текст не может быть null.");
        }

        private static String Convert(String text, String keyPattern, VigenereConverter converter)
        {
            KeyCharSource source = new KeyCharSource(keyPattern);
            StringBuilder builder = new StringBuilder(text.Length);
            foreach (Char inputChar in text)
            {
                Char lowerInputChar = Char.ToLower(inputChar, GlobalConstants.Culture);
                builder.Append(Alphabet.Contains(lowerInputChar) ? converter.Invoke(lowerInputChar, source.Next) : inputChar);
            }
            return builder.ToString();
        }
    }
}