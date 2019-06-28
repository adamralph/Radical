﻿namespace Radical.Conversions
{
    using Radical.ComponentModel;
    using Radical.Validation;
    using System;

    public static class KeyConversionExtensions
    {
        public static IKey<T> AsKey<T>(this T value) where T : IComparable, IComparable<T>
        {
            return new Key<T>(value);
        }

        public static T AsValue<T>(this IKey value) where T : IComparable, IComparable<T>
        {
            Ensure.That(value).Named("value").IsTrue(obj => obj is IKey<T>);

            return ((IKey<T>)value).Value;
        }
    }
}
