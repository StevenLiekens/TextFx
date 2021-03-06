﻿using Txt.Core;
using Xunit;

namespace Txt.ABNF.Core.BIT
{
    public class BitLexerTests
    {
        [Fact]
        public void CannotReadNegativeOne()
        {
            var input = "-1";
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("BIT");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Null(result);
            }
        }

        [Fact]
        public void CanReadOne()
        {
            var input = "1";
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("BIT");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }

        [Fact]
        public void CanReadZero()
        {
            var input = "0";
            var grammar = new CoreGrammar();
            grammar.Initialize();
            var sut = grammar.Rule("BIT");
            using (var scanner = new TextScanner(new StringTextSource(input)))
            {
                var result = sut.Read(scanner);
                Assert.Equal(input, result.Text);
            }
        }
    }
}
