﻿namespace SLANG
{
    using System;

    /// <summary>Provides the base class for lexers whose lexer rule has nine alternatives.</summary>
    /// <typeparam name="TAlternative">The type of the lexer rule.</typeparam>
    /// <typeparam name="T1">The type of the first alternative element.</typeparam>
    /// <typeparam name="T2">The type of the second alternative element.</typeparam>
    /// <typeparam name="T3">The type of the third alternative element.</typeparam>
    /// <typeparam name="T4">The type of the fourth alternative element.</typeparam>
    /// <typeparam name="T5">The type of the fifth alternative element.</typeparam>
    /// <typeparam name="T6">The type of the sixth alternative element.</typeparam>
    /// <typeparam name="T7">The type of the seventh alternative element.</typeparam>
    /// <typeparam name="T8">The type of the eighth alternative element.</typeparam>
    /// <typeparam name="T9">The type of the ninth alternative element.</typeparam>
    public abstract class AlternativeLexer<TAlternative, T1, T2, T3, T4, T5, T6, T7, T8, T9> : Lexer<TAlternative>
        where TAlternative : Alternative<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        where T1 : Element
        where T2 : Element
        where T3 : Element
        where T4 : Element
        where T5 : Element
        where T6 : Element
        where T7 : Element
        where T8 : Element
        where T9 : Element
    {
        /// <summary>Initializes a new instance of the <see cref="AlternativeLexer{TAlternative,T1,T2,T3,T4,T5,T6,T7,T8,T9}"/> class for a specified rule.</summary>
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

            T4 alternative4;
            if (this.TryRead4(scanner, out alternative4))
            {
                element = this.CreateInstance4(alternative4, context);
                return true;
            }

            T5 alternative5;
            if (this.TryRead5(scanner, out alternative5))
            {
                element = this.CreateInstance5(alternative5, context);
                return true;
            }

            T6 alternative6;
            if (this.TryRead6(scanner, out alternative6))
            {
                element = this.CreateInstance6(alternative6, context);
                return true;
            }

            T7 alternative7;
            if (this.TryRead7(scanner, out alternative7))
            {
                element = this.CreateInstance7(alternative7, context);
                return true;
            }

            T8 alternative8;
            if (this.TryRead8(scanner, out alternative8))
            {
                element = this.CreateInstance8(alternative8, context);
                return true;
            }

            T9 alternative9;
            if (this.TryRead9(scanner, out alternative9))
            {
                element = this.CreateInstance9(alternative9, context);
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

        /// <summary>Creates a new instance of the lexer rule for the fourth alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance4(T4 element, ITextContext context);

        /// <summary>Creates a new instance of the lexer rule for the fifth alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance5(T5 element, ITextContext context);

        /// <summary>Creates a new instance of the lexer rule for the sixth alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance6(T6 element, ITextContext context);

        /// <summary>Creates a new instance of the lexer rule for the seventh alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance7(T7 element, ITextContext context);

        /// <summary>Creates a new instance of the lexer rule for the eighth alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance8(T8 element, ITextContext context);

        /// <summary>Creates a new instance of the lexer rule for the ninth alternative element.</summary>
        /// <param name="element">The alternative element.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        /// <returns>An instance of the lexer rule.</returns>
        protected abstract TAlternative CreateInstance9(T9 element, ITextContext context);

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

        /// <summary>Attempts to read the fourth alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead4(ITextScanner scanner, out T4 element);

        /// <summary>Attempts to read the fifth alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead5(ITextScanner scanner, out T5 element);

        /// <summary>Attempts to read the sixth alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead6(ITextScanner scanner, out T6 element);

        /// <summary>Attempts to read the seventh alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead7(ITextScanner scanner, out T7 element);

        /// <summary>Attempts to read the eighth alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead8(ITextScanner scanner, out T8 element);

        /// <summary>Attempts to read the ninth alternative element. A return value indicates whether the element was available.</summary>
        /// <param name="scanner">The scanner object that provides text symbols as well as contextual information about the text source.</param>
        /// <param name="element">When this method returns, contains the next available element, or a <c>null</c> reference, depending on whether the return value indicates success.</param>
        /// <exception cref="T:System.InvalidOperationException">The given scanner object is not initialized.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The given text scanner is closed.</exception>
        /// <returns><c>true</c> to indicate success; otherwise, <c>false</c>.</returns>
        protected abstract bool TryRead9(ITextScanner scanner, out T9 element);
    }
}