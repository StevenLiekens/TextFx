﻿namespace Text.Scanning.Core
{
    public class DigitLexer : Lexer<Digit>
    {
        /// <inheritdoc />
        public override Digit Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            Digit element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'DIGIT'");
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out Digit element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Digit);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '0'; c <= '9'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new Digit(c, context);
                    return true;
                }
            }

            element = default(Digit);
            return false;
        }
    }
}