﻿namespace SLANG
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG.Core;

    [TestClass]
    public class AlphaLexerTests
    {
        [TestMethod]
        public void ReadLowerCaseAlphabet()
        {
            var text = "abcdefghijklmnopqrstuvwxyz";
            var lexer = new AlphaLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                for (int i = 0; i < text.Length; i++)
                {
                    var element = lexer.Read(scanner);
                    Assert.AreEqual(text.Substring(i, 1), element.Data);
                }
            }
        }

        [TestMethod]
        public void ReadUpperCaseAlphabet()
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lexer = new AlphaLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                for (int i = 0; i < text.Length; i++)
                {
                    var element = lexer.Read(scanner);
                    Assert.AreEqual(text.Substring(i, 1), element.Data);
                }
            }
        }

        [TestMethod]
        public void FailSymbol()
        {
            var text = "%";
            var lexer = new AlphaLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                Alpha element;
                var result = lexer.TryRead(scanner, out element);
                Assert.IsFalse(result);
                Assert.IsNull(element);
            }
        }
    }
}