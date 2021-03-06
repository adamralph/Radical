﻿namespace Topics.Radical.ComponentModel
{
    using System;

    /// <summary>
    /// Provides a standard method to determine if an object instance 
    /// should be, or shoud not, included in the result set of a filter
    /// operation.
    /// </summary>
    /// <typeparam name="T">The type of the object to test.</typeparam>
    public interface IEntityItemViewFilter<T> : IEntityItemViewFilter //where T : class
    {
        /// <summary>
        /// Gets a value that indicates if the given object instance should be included in the result set of the filter operation..
        /// </summary>
        /// <param name="item">The item to test.</param>
        /// <returns><c>True</c> if the item should be included, otherwise <c>false</c>.</returns>
        Boolean ShouldInclude( T item );
    }
}
