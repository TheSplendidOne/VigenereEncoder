using System.Globalization;
using System.Text;

namespace VigenereEncoder
{
    public static class GlobalConstants
    {
        public static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("ru");

        public static readonly Encoding Encoding = Encoding.Default;
    }
}