using System;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using VigenereEncoder;

namespace Tests
{
    [TestFixture]
    public class KeyCharSourceTests
    {
        [Test]
        public void Source_CanRepeatKeyPatternSeveralTimes()
        {
            Int32 repeatNumber = 10;
            String keyPattern = "скорпион";
            StringBuilder builder = new StringBuilder(keyPattern.Length * repeatNumber);
            KeyCharSource source = new KeyCharSource(keyPattern);

            for(Int32 i = 0; i < keyPattern.Length * repeatNumber; ++i)
                builder.Append(source.Next);

            builder.ToString().Should().BeEquivalentTo(String.Concat(Enumerable.Repeat(keyPattern, repeatNumber)));
        }

        [Test]
        public void Source_ThrowArgumentException_IfKeyPatternEmpty()
        {
            Func<KeyCharSource> act = () => new KeyCharSource(String.Empty);
            
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Source_ThrowArgumentNullException_IfKeyPatternNull()
        {
            Func<KeyCharSource> act = () => new KeyCharSource(null);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
