﻿namespace Text.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a text scanner that gets text from an instance of the <see cref="T:Text.Scanning.TextScanner" />
    /// class.
    /// </summary>
    public sealed class TextScanner : ITextScanner
    {
        /// <summary>Indicates whether this object has been disposed.</summary>
        private bool disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool endOfInput;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private char nextCharacter;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int offset = -1;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stack<char> buffer = new Stack<char>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly TextReader textReader;

        /// <summary>Initializes a new instance of the <see cref="T:Text.Scanning.TextScanner" /> class for the given data source.</summary>
        /// <param name="textReader">The <see cref="T:System.IO.TextReader" /> to read data from.</param>
        public TextScanner(TextReader textReader)
        {
            Contract.Requires(textReader != null);
            this.textReader = textReader;
        }

        /// <inheritdoc />
        private bool EndOfInput
        {
            [Pure]
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                return this.endOfInput;
            }
        }

        /// <inheritdoc />
        bool ITextScanner.EndOfInput
        {
            [Pure]
            get
            {
                return this.EndOfInput;
            }
        }

        /// <inheritdoc />
        private char? NextCharacter
        {
            [Pure]
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                if (this.offset == -1)
                {
                    return null;
                }

                if (this.endOfInput)
                {
                    return null;
                }

                return this.nextCharacter;
            }
        }

        /// <inheritdoc />
        char? ITextScanner.NextCharacter
        {
            [Pure]
            get
            {
                return this.NextCharacter;
            }
        }

        /// <inheritdoc />
        public int Offset
        {
            [Pure]
            get
            {
                if (this.disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                return this.offset;
            }
        }

        /// <inheritdoc />
        int ITextContext.Offset
        {
            [Pure]
            get
            {
                return this.Offset;
            }
        }

        /// <summary>This method calls <see cref="Dispose(bool)" />, specifying <c>true</c> to release all resources.</summary>
        public void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        void IDisposable.Dispose()
        {
            this.Close();
        }

        /// <inheritdoc />
        [Pure]
        private ITextContext GetContext()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            return new TextContext(this.offset);
        }

        /// <inheritdoc />
        [Pure]
        ITextContext ITextScanner.GetContext()
        {
            return this.GetContext();
        }

        /// <inheritdoc />
        private void PutBack(char c)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.offset == 0)
            {
                throw new InvalidOperationException("Precondition failed: Offset > 0");
            }

            if (this.endOfInput)
            {
                this.endOfInput = false;
            }
            else
            {
                this.buffer.Push(this.nextCharacter);
            }

            this.offset -= 1;
            this.nextCharacter = c;
        }

        /// <inheritdoc />
        void ITextScanner.PutBack(char c)
        {
            this.PutBack(c);
        }

        /// <inheritdoc />
        private bool Read()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.endOfInput)
            {
                return false;
            }

            if (this.buffer.Count > 0)
            {
                this.nextCharacter = this.buffer.Pop();
            }
            else
            {
                lock (this.textReader)
                {
                    this.nextCharacter = (char)this.textReader.Read();
                }
            }

            this.offset += 1;
            if (this.nextCharacter == char.MaxValue)
            {
                this.endOfInput = true;
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        bool ITextScanner.Read()
        {
            return this.Read();
        }

        /// <inheritdoc />
        private bool TryMatch(char c)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (this.offset == -1)
            {
                throw new InvalidOperationException("No next character available: call 'Read()' to initialize.");
            }

            if (this.endOfInput)
            {
                throw new InvalidOperationException("No next character available: end of input has been reached.");
            }

            if (this.nextCharacter != c)
            {
                return false;
            }

            this.Read();
            return true;
        }

        /// <inheritdoc />
        bool ITextScanner.TryMatch(char c)
        {
            return this.TryMatch(c);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        /// <c>true</c> to clean up both managed and unmanaged resources; otherwise, <c>false</c> to clean up only unmanaged
        /// resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.textReader.Dispose();
            }

            this.disposed = true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.textReader != null);
            Contract.Invariant(this.buffer != null);
        }
    }
}