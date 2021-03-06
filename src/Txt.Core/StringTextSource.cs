﻿using System.Text;
using JetBrains.Annotations;

namespace Txt.Core
{
    public class StringTextSource : TextSource
    {
        public StringTextSource([NotNull] string data)

            // ReSharper disable once ConstantConditionalAccessQualifier
            // ReSharper disable once AssignNullToNotNullAttribute
            : base(data?.ToCharArray())
        {
        }

        public StringTextSource([NotNull] char[] data)
            : base(data)
        {
        }

        public StringTextSource([NotNull] char[] data, int startIndex)
            : base(data, startIndex)
        {
        }

        public StringTextSource([NotNull] char[] data, int startIndex, int length)
            : base(data, startIndex, length)
        {
        }

        public override Encoding Encoding => Encoding.Unicode;

        protected override int ReadImpl(char[] buffer, int startIndex, int maxCount)
        {
            // All characters were buffered upfront so return a value indicating that there are no more characters
            return 0;
        }
    }
}
