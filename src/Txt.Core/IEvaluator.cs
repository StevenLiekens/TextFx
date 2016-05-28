﻿namespace Txt.Core
{
    /// <summary>
    ///     Providers the interface for element tree walkers, such as parsers, compilers, interpreters or syntax highlighters.
    /// </summary>
    /// <remarks>
    ///     Implementers must declare at least one method named
    ///     <c>Evaluate</c> that takes an <see cref="Element" /> and returns a <see cref="bool" />. Implementers may optionally
    ///     declare an unlimited number of additional <c>Evaluate</c> methods that take any argument that derives from
    ///     <see cref="Element" />. When walking an element tree, the <c>Evaluate</c> method that gets called is bound at runtime
    ///     by using dynamic language runtime features.
    /// <example>
    ///    public class ExampleWalker : IEvaluator
    ///    {
    ///        // Implementers MUST implement the following method
    ///        public bool Evaluate(Element element)
    ///        {
    ///            // Do something with the current node (e.g. logging)
    ///            Console.WriteLine(
    ///                "Found element of type {0} with text {1} at offset {2}",
    ///                element.GetType(),
    ///                element.Text,
    ///                element.Offset);
    ///    
    ///            // return true to indicate that the element's descendant nodes (if any) should be evaluated
    ///            return true;
    ///        }
    ///    
    ///        // Implementers MAY implement overloads that take a more derived element type
    ///        // The most specific overload will be automatically selected at runtime
    ///        public bool Evaluate(Alpha alpha)
    ///        {
    ///            // Log the letter of the alphabet
    ///            Console.WriteLine(
    ///                "Found a letter of the alphabet: {0} at offset {1}",
    ///                alpha.Text,
    ///                alpha.Offset);
    ///    
    ///            // Return false to indicate that descendant nodes should not be evaluated
    ///            return false;
    ///        }
    ///    }
    /// </example>
    /// </remarks>
    public interface IEvaluator
    {
        /// <summary>
        /// Evaluates a given element. A return value indicates whether its descendant nodes should be evaluated as well.
        /// </summary>
        /// <param name="element">The element to evaluate.</param>
        /// <returns><c>true</c> if descendant nodes should be evaluated; otherwise, <c>false</c>.</returns>
        bool Evaluate(Element element);
    }
}