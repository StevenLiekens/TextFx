﻿using System.Diagnostics;

namespace Text
{
    public abstract class Token : ITextContext
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int offset;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string data;

        protected Token(string data, ITextContext context)
        {
            this.data = data;
            this.offset = context.Offset;
        }

        public string Data
        {
            get { return this.data; }
        }

        public override string ToString()
        {
            return this.Data;
        }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset
        {
            get
            {
                return this.offset;
            }
        }
    }
}