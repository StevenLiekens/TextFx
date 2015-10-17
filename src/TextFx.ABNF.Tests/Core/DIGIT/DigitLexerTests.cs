﻿namespace TextFx.ABNF.Core
{
    using Xunit;

    public class DigitLexerTests
    {
        [Theory]
        [InlineData("\x30")]
        [InlineData("\x31")]
        [InlineData("\x32")]
        [InlineData("\x33")]
        [InlineData("\x34")]
        [InlineData("\x35")]
        [InlineData("\x36")]
        [InlineData("\x37")]
        [InlineData("\x38")]
        [InlineData("\x39")]
        public void ReadSuccess(string input)
        {
            var factory = new DigitLexerFactory(new ValueRangeLexerFactory());
            var digitLexer = factory.Create();
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var digit = digitLexer.Read(scanner, null);
                Assert.Equal(input, digit.Text);
            }
        }
    }
}
