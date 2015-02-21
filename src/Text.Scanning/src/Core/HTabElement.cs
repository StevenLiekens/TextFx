﻿namespace Text.Scanning.Core
{
    using System.Diagnostics.Contracts;

    /// <summary>Represents the HTAB rule: 1 horizontal tab. Unicode: U+0009.</summary>
    public class HTabElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.Core.HTabElement" /> class with a specified context.</summary>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public HTabElement(ITextContext context)
            : base("\u0009", context)
        {
            Contract.Requires(context != null);
        }
    }
}