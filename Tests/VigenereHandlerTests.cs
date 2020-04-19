using System;
using FluentAssertions;
using NUnit.Framework;
using VigenereEncoder;

namespace Tests
{
    [TestFixture]
    public class VigenereHandlerTests
    {
        //для достоверности тесты проводятся на двух разных ключах

        public const String Key1 = "скорпион";

        public const String Key2 = "змееносец";

        public const String DefaultText = "з04ем писать тесты на C#, если м0жн0 не писать...";

        [TestCase("", Key1, ExpectedResult = "", TestName = "Encode_WithEmptyText")]
        [TestCase("", Key2, ExpectedResult = "", TestName = "Encode_WithEmptyText")]
        [TestCase("поздравляю", Key1, ExpectedResult = "бщцфаирщри", TestName = "Encode_WithAlphabeticCharacters")]
        [TestCase("поздравляю", Key2, ExpectedResult = "чымиюоурхё", TestName = "Encode_WithAlphabeticCharacters")]
        [TestCase("ПоЗДравЛЯЮ", Key1, ExpectedResult = "бщцфаирщри", TestName = "Encode_WithUpperAlphabeticCharacters")]
        [TestCase("ПоЗДравЛЯЮ", Key2, ExpectedResult = "чымиюоурхё", TestName = "Encode_WithUpperAlphabeticCharacters")]
        [TestCase(DefaultText, Key1, ExpectedResult = "щ04пы ашъоан эуввд ьн C#, цьъщ ь0пь0 ыц ъчвпык...", TestName = "Encode_WithNoneAlphabeticCharacters")]
        [TestCase(DefaultText, Key2, ExpectedResult = "п04сс фцасчт ъсцчи ьс C#, йзух с0лы0 ьц фящмчб...", TestName = "Encode_WithNoneAlphabeticCharacters")]
        public String CommonEncodeTest(String text, String keyPattern)
        {
            return VigenereHandler.Encode(text, keyPattern);
        }

        [TestCase(Key1)]
        [TestCase(Key2)]
        public void Encode_AlwaysReturnsTheSameResultWithTheSameArguments(String keyPattern)
        {
            VigenereHandler.Encode(DefaultText, keyPattern).Should().BeEquivalentTo(VigenereHandler.Encode(DefaultText, keyPattern));
        }

        [TestCase("", Key1, ExpectedResult = "", TestName = "Decode_WithEmptyText")]
        [TestCase("", Key2, ExpectedResult = "", TestName = "Decode_WithEmptyText")]
        [TestCase("бщцфаирщри", Key1, ExpectedResult = "поздравляю", TestName = "Decode_WithAlphabeticCharacters")]
        [TestCase("чымиюоурхё", Key2, ExpectedResult = "поздравляю", TestName = "Decode_WithAlphabeticCharacters")]
        [TestCase("БЩцФАирщрИ", Key1, ExpectedResult = "поздравляю", TestName = "Decode_WithUpperAlphabeticCharacters")]
        [TestCase("чыМИюоУрХЁ", Key2, ExpectedResult = "поздравляю", TestName = "Decode_WithUpperAlphabeticCharacters")]
        [TestCase("щ04пы ашъоан эуввд ьн C#, цьъщ ь0пь0 ыц ъчвпык...", Key1, ExpectedResult = DefaultText, TestName = "Decode_WithNoneAlphabeticCharacters")]
        [TestCase("п04сс фцасчт ъсцчи ьс C#, йзух с0лы0 ьц фящмчб...", Key2, ExpectedResult = DefaultText, TestName = "Decode_WithNoneAlphabeticCharacters")]
        public String CommonDecodeTest(String text, String keyPattern)
        {
            return VigenereHandler.Decode(text, keyPattern);
        }

        [TestCase("щ04пы ашъоан эуввд ьн C#, цьъщ ь0пь0 ыц ъчвпык...", Key1)]
        [TestCase("п04сс фцасчт ъсцчи ьс C#, йзух с0лы0 ьц фящмчб...", Key2)]
        public void Decode_AlwaysReturnsTheSameResultWithTheSameArguments(String text, String keyPattern)
        {
            VigenereHandler.Decode(text, keyPattern).Should().BeEquivalentTo(VigenereHandler.Decode(text, keyPattern));
        }

        [TestCase(Key1)]
        [TestCase(Key2)]
        public void TextWithoutUpperAlphabeticCharacters_DoesNotChange_AfterEncodingAndDecoding(String keyPattern)
        {
            VigenereHandler.Decode(VigenereHandler.Encode(DefaultText, keyPattern), keyPattern).Should().BeEquivalentTo(DefaultText);
        }

        [Test]
        public void Encode_ThrowArgumentNullException_IfTextNull()
        {
            Func<String> act = () => VigenereHandler.Encode(null, Key1);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Encode_ThrowArgumentException_IfKeyPatternEmpty()
        {
            Func<String> act = () => VigenereHandler.Encode(DefaultText, String.Empty);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Encode_ThrowArgumentNullException_IfKeyPatternNull()
        {
            Func<String> act = () => VigenereHandler.Encode(DefaultText, null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Decode_ThrowArgumentNullException_IfTextNull()
        {
            Func<String> act = () => VigenereHandler.Decode(null, Key1);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Decode_ThrowArgumentException_IfKeyPatternEmpty()
        {
            Func<String> act = () => VigenereHandler.Decode(DefaultText, String.Empty);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Decode_ThrowArgumentNullException_IfKeyPatternNull()
        {
            Func<String> act = () => VigenereHandler.Decode(DefaultText, null);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
