﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlCharacter.cs" company="Steven Liekens">
//   The MIT License (MIT)
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TextFx.ABNF.Core
{
    public class ControlCharacter : Alternative
    {
        public ControlCharacter(Alternative element)
            : base(element)
        {
        }
    }
}