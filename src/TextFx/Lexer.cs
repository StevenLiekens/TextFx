﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to convert the input text to a parse tree.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     Provides the base class for lexers. A lexer is a class that matches symbols from a data source against a
    ///     grammar rule to produce grammar elements. Each class that extends the <see cref="Lexer{TElement}" /> class
    ///     corresponds to a singe grammar rule. For complex grammars with many grammar rules, multiple lexers work together to
    ///     convert the input text to a parse tree.
    /// </summary>
    /// <typeparam name="TElement">The type of the element that represents the lexer rule.</typeparam>
    /// <remarks>
    ///     <para>The terms "lexer rule" and "grammar rule" are used interchangeably.</para>
    ///     <para>
    ///         Notes to inheritors.
    ///         The name of grammar rules are case insensitive.
    ///         At minimum, you must provide an implementation for the <see cref="TryRead" /> method. You can optionally
    ///         provide a custom implementation for the <see cref="Read" /> method. The default behavior of the
    ///         <see cref="Read" /> method is essentially a virtual call to <see cref="TryRead" />, but contains additional
    ///         logic to initialize and throw a <see cref="FormatException" />.
    ///         There are a number of conventions that you should follow.
    ///         If the value of <see cref="ITextScanner.EndOfInput" /> is <c>true</c> and the grammar rule is not optional, you
    ///         should immediately return <c>false</c>.
    ///         Do not throw any exceptions in TryRead().
    ///         Lexer classes should be sealed.
    ///         Re-use lexer classes for lexer rules that reference other lexer rules.
    ///     </para>
    /// </remarks>
    [RuleName("undefined")]
    public abstract class Lexer<TElement> : ILexer<TElement>
        where TElement : Element
    {
        /// <inheritdoc />
        public virtual TElement Read(ITextScanner scanner)
        {
            TElement element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new FormatException(
                string.Format(
                    "Syntax error. Expected '{0}' at position '{1}'.",
                    this.GetType().GetTypeInfo().GetCustomAttribute<RuleNameAttribute>().RuleName,
                    scanner.GetContext().Offset));
        }

        /// <inheritdoc />
        public Element ReadElement(ITextScanner scanner)
        {
            return this.Read(scanner);
        }

        /// <inheritdoc />
        public abstract bool TryRead(ITextScanner scanner, out TElement element);

        /// <inheritdoc />
        public bool TryReadElement(ITextScanner scanner, out Element element)
        {
            // This intermediary variable is required to match the type of the output parameter
            TElement t;
            if (this.TryRead(scanner, out t))
            {
                element = t;
                return true;
            }

            element = default(Element);
            return false;
        }
    }
}