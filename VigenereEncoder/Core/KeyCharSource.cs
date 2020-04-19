using System;
using System.Collections.Generic;

namespace VigenereEncoder
{
    public class KeyCharSource
    {
        private readonly IEnumerator<Char> _enumerator;

        public Char Next
        {
            get
            {
                _enumerator.MoveNext();
                return _enumerator.Current;
            }
        }

        public KeyCharSource(String keyPattern)
        {
            Validate(keyPattern);
            _enumerator = GetInfinityCycledKey(keyPattern.ToLower(VigenereHandler.Culture)).GetEnumerator();
        }

        private static void Validate(String keyPattern)
        {
            if(keyPattern == null)
                throw new ArgumentNullException("Ключ не может быть null.");
            if(keyPattern == String.Empty)
                throw new ArgumentException("Ключ не может быть пустой строкой.");
        }

        private static IEnumerable<Char> GetInfinityCycledKey(String keyPattern)
        {
            while(true)
                foreach(Char c in keyPattern)
                    yield return c;
        }
    }
}