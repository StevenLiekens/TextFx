﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlternativeLexer{TAlternative,T1,T2,T3}.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the base class for lexers whose lexer rule has three alternatives.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SLANG
{
    using System;

    /// <summary>Provides the base class for lexers whose lexer rule has three alternatives.</summary>
    /// <typeparam name="TAlternative">The type of the lexer rule.</typeparam>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    /// <typeparam name="T3">The type of the third alternative element.</typeparam>
    public abstract class AlternativeLexer<TAlternative, T1, T2, T3> : Lexer<TAlternative>
        where TAlternative : Alternative<T1, T2, T3>
        where T1 : Element
        where T2 : Element
        where T3 : Element
    {
        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative,T1,T2,T3}"/> class for a specified rule.</summary>
        /// <param name="ruleName">The name of the lexer rule. Rule names are case insensitive.</param>
        /// <exception cref="ArgumentException">The value of <paramref name="ruleName"/> is a <c>null</c> reference (<c>Nothing</c> in Visual Basic) -or- the value of <paramref name="ruleName"/> does not start with a letter -or- the value of <paramref name="ruleName"/> contains one or more characters that are not letters, digits or hyphens.</exception>
        protected AlternativeLexer(string ruleName)
            : base(ruleName)
        {
        }

        /// <inheritdoc />
        public override bool TryRead(ITextScanner scanner, out TAlternative element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TAlternative);
                return false;
            }

            var context = scanner.GetContext();
            T1 alternative1;
            if (this.TryRead1(scanner, out alternative1))
            {
                element = this.CreateInstance1(alternative1, context);
                return true;
            }

            T2 alternative2;
            if (this.TryRead2(scanner, out alternative2))
            {
                element = this.CreateInstance2(alternative2, context);
                return true;
            }

            T3 alternative3;
            if (this.TryRead3(scanner, out alternative3))
            {
                element = this.CreateInstance3(alternative3, context);
                return true;
            }

            element = default(TAlternative);
            return false;
        }

        /// <summary>Creates a new instance of the lexer rule for the first alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance1(T1 element, ITextContext context);

        /// <summary>Creates a new instance of the lexer rule for the second alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance2(T2 element, ITextContext context);

        /// <summary>Creates a new instance of the lexer rule for the third alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance3(T3 element, ITextContext context);

        /// <summary>Attempts to read the first alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead1(ITextScanner scanner, out T1 element);

        /// <summary>Attempts to read the second alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead2(ITextScanner scanner, out T2 element);

        /// <summary>Attempts to read the third alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead3(ITextScanner scanner, out T3 element);
    }
}