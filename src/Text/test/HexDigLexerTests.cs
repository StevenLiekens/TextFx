﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Core;

namespace Text
{
    [TestClass]
    public class HexDigLexerTests
    {
        [TestMethod]
        public void ReadHexDigs()
        {
            var text = "0123456789ABCDEF";
            using (var reader = new StringReader(text))
            using (var scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new HexDigLexer(scanner);
                while (!scanner.EndOfInput)
                {
                    var token = lexer.Read();
                    Assert.IsNotNull(token);
                    Assert.IsTrue(Uri.IsHexDigit(token.Data[0]));
                }
            }
        }
    }
}