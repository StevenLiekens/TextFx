﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DigitLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.Core
{
    using System;

    [RuleName("DIGIT")]
    public class DigitLexer : Lexer<Digit>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x30-39</param>
        public DigitLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Digit element)
        {
            Terminal result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new Digit(result);
                return true;
            }

            element = default(Digit);
            return false;
        }
    }
}