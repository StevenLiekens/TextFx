﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;
using Text.Scanning.Core;

namespace Text
{
    [TestClass]
    public class VCharLexerTests
    {
        [TestMethod]
        public void ReadAlpha()
        {
            var text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lexer = new VCharLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                while (!scanner.EndOfInput)
                {
                    var element = lexer.Read(scanner);
                    Assert.IsNotNull(element);
                    Assert.IsTrue(char.IsLetter(element.Data[0]));
                }
            }
        }

        [TestMethod]
        public void FailHTab()
        {
            var text = "\t";
            var lexer = new VCharLexer();
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                VCharElement element;
                if (lexer.TryRead(scanner, out element))
                {
                    Assert.Fail();
                }

                Assert.IsNull(element);
            }
        }
    }
}
