﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacterLexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SLANG.Core
{
    using System;

    [RuleName("CTL")]
    public class ControlCharacterLexer : Lexer<ControlCharacter>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x00-1F / %x7F</param>
        public ControlCharacterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out ControlCharacter element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new ControlCharacter(result);
                return true;
            }

            element = default(ControlCharacter);
            return false;
        }
    }
}