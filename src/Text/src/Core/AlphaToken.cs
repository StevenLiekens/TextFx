﻿namespace Text.Core
{
    /// <summary>A-Z / a-z</summary>
    public class AlphaToken : Token
    {
        public AlphaToken(string data, ITextContext context)
            : base(data, context)
        {
        }
    }
}