﻿using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;

namespace Txt.ABNF.Core.DIGIT
{
    public sealed class DigitLexer : RuleLexer<Digit>, IInitializable
    {
        public DigitLexer([NotNull] Grammar grammar)
            : base(grammar)
        {
        }

        public ILexer<Element> InnerLexer { get; private set; }

        public void Initialize()
        {
            InnerLexer = ValueRange.Create('\x30', '\x39');
        }

        protected override IEnumerable<Digit> ReadImpl(ITextScanner scanner, ITextContext context)
        {
            foreach (var terminal in InnerLexer.Read(scanner, context))
            {
                yield return new Digit(terminal);
            }
        }
    }
}
