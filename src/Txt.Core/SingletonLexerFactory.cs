﻿using System;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class SingletonLexerFactory<T> : ILexerFactory<T>
        where T : Element
    {
        [NotNull]
        [ItemNotNull]
        private readonly Lazy<ILexer<T>> lazy;

        public SingletonLexerFactory([NotNull] ILexerFactory<T> factory)
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            lazy = new Lazy<ILexer<T>>(factory.Create);
        }

        public ILexerFactory<T> Factory { get; }

        public ILexer<T> Create()
        {
            return lazy.Value;
        }

        public ILexerFactory<T> Singleton()
        {
            return this;
        }
    }
}
