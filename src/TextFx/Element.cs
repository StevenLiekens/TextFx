﻿namespace TextFx
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>Provides the base class for all elements.</summary>
    public abstract class Element : ITextContext, IReadOnlyList<Element>
    {
        [NotNull]
        [ItemNotNull]
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly Element[] elements;

        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private static readonly Element[] EmptyElements = new Element[0];

        [NotNull]
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly ITextContext context;

        [NotNull]
        [DebuggerBrowsable(SwitchOnBuild.DebuggerBrowsableState)]
        private readonly string text;

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given element to copy.</summary>
        /// <param name="element">The element to copy.</param>
        /// <exception cref="ArgumentNullException">The value of <paramref name="element" /> is a null reference.</exception>
        protected Element([NotNull] Element element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
            text = element.text;
            elements = element.elements;
            context = element.context;
        }

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given terminal and context.</summary>
        /// <param name="terminal">The terminal value.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of  <paramref name="terminal" /> or <paramref name="context" /> is a
        ///     null reference.
        /// </exception>
        protected Element([NotNull] string terminal, [NotNull] ITextContext context)
        {
            if (terminal == null)
            {
                throw new ArgumentNullException(nameof(terminal));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            text = terminal;
            elements = new Element[0];
            this.context = context;
        }

        /// <summary>Initializes a new instance of the <see cref="Element" /> class with a given sequence and context.</summary>
        /// <param name="sequence">The text in the sequence.</param>
        /// <param name="elements">The collection of elements that represent the sequence.</param>
        /// <param name="context">An object that describes the current element's context.</param>
        /// <exception cref="ArgumentNullException">
        ///     The value of  <paramref name="sequence" /> or  <paramref name="elements" /> or
        ///     <paramref name="context" /> is a null reference.
        /// </exception>
        protected Element([NotNull] string sequence, [NotNull] IList<Element> elements, [NotNull] ITextContext context)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            text = sequence;
            this.elements = elements.Count == 0 ? EmptyElements : elements.ToArray();
            this.elements = elements.ToArray();
            this.context = context;
        }

        /// <inheritdoc />
        public int Count => elements.Length;

        [NotNull]
        public IList<Element> Elements
        {
            get
            {
                Debug.Assert(elements != null, "this.elements != null");
                return elements;
            }
        }

        [CanBeNull]
        public Element NextElement { get; set; }

        /// <summary>Gets the current position, relative to the beginning of the data source.</summary>
        public int Offset => context.Offset;

        [CanBeNull]
        public Element PreviousElement { get; set; }

        /// <summary>Gets one or more terminal values that represent the current element.</summary>
        [NotNull]
        public string Text
        {
            get
            {
                Debug.Assert(text != null);
                return text;
            }
        }

        /// <inheritdoc />
        public Element this[int index] => elements[index];

        /// <inheritdoc />
        public IEnumerator<Element> GetEnumerator()
        {
            return ((IEnumerable<Element>)elements).GetEnumerator();
        }

        /// <summary>
        ///     Gets a collection of terminal elements by recursively evaluating <see cref="GetTerminals" /> for every
        ///     <see cref="Element" /> in <see cref="Elements" />.
        /// </summary>
        /// <returns>A collection of terminal elements.</returns>
        [NotNull]
        [ItemNotNull]
        public IEnumerable<Element> GetTerminals()
        {
            if (this is Terminal)
            {
                yield return this;
            }
            else
            {
                foreach (var terminal in Elements.SelectMany(t => t.GetTerminals()))
                {
                    yield return terminal;
                }
            }
        }

        /// <summary>
        ///     Gets a well-formed string that represents the current element. This is useful for elements that are
        ///     technically valid, but contain formatting errors or other inpurities. For example: mixed upper and lower case
        ///     characters where only lower case is well-formed. Unless overridden, the default return value is the value of
        ///     <see cref="Text" />.
        /// </summary>
        /// <returns>A well-formed string that represents the current element.</returns>
        public virtual string GetWellFormedText()
        {
            return string.Concat(Elements.Select(element => element.GetWellFormedText()));
        }

        /// <inheritdoc />
        public sealed override string ToString()
        {
            return Text;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)elements).GetEnumerator();
        }
    }
}
