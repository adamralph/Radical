﻿using Radical.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radical
{
    internal class PuzzleContainerEntry<T> : IPuzzleContainerEntry, IPuzzleContainerEntry<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PuzzleContainerEntry&lt;T&gt;"/> class.
        /// </summary>
        internal PuzzleContainerEntry()
        {
            this.Key = Guid.NewGuid().ToString();
            this.Parameters = new Dictionary<string, Object>();
        }

        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the lifestyle of ths component.
        /// </summary>
        /// <value>The lifestyle.</value>
        public Lifestyle Lifestyle
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the factory used to build up a concrete type.
        /// </summary>
        /// <value>The factory.</value>
        public Delegate Factory
        {
            get;
            private set;
        }

        Dictionary<int, Type> services = new Dictionary<int, Type>();

        internal PuzzleContainerEntry<T> AddService(Type type)
        {
            var hash = type.GetHashCode();
            if (!services.ContainsKey(hash))
            {
                this.services.Add(hash, type);
            }

            return this;
        }

        /// <summary>
        /// Gets the service type.
        /// </summary>
        /// <value>The service type.</value>
        public IEnumerable<Type> Services
        {
            get { return this.services.Values.Cast<Type>(); }
        }

        /// <summary>
        /// Gets the component type.
        /// </summary>
        /// <value>The component type.</value>
        public Type Component
        {
            get;
            internal set;
        }

        public IDictionary<string, Object> Parameters
        {
            get;
            private set;
        }

        void ImplementedBy(Type type)
        {
            //Ensure.That( type ).Named( "type" ).IsFalse( t => t.IsInterface );

            this.Component = type;
        }

        void WithParameters(IDictionary<string, Object> parameters)
        {
            //Ensure.That( parameters ).Named( "parameters" ).IsNotNull();

            this.Parameters = parameters;
        }

        void UsingFactory(Delegate factory)
        {
            //Ensure.That( factory ).Named( "factory" ).IsNotNull();

            this.Factory = factory;
        }

        IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.Forward<TForwarded>()
        {
            this.AddService(typeof(TForwarded));
            return this;
        }

        IPuzzleContainerEntry IPuzzleContainerEntry.Forward(Type forwardedType)
        {
            this.AddService(forwardedType);
            return this;
        }

        /// <summary>
        /// Defines the type that implements the service.
        /// </summary>
        /// <param name="componentType">The type of the component.</param>
        /// <returns>This entry instance.</returns>
        IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.ImplementedBy(Type componentType)
        {
            this.ImplementedBy(componentType);
            return this;
        }

        /// <summary>
        /// Defines the type that implements the service.
        /// </summary>
        /// <typeparam name="TComponent">The type of the component.</typeparam>
        /// <returns>This entry instance.</returns>
        IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.ImplementedBy<TComponent>()
        {
            this.ImplementedBy(typeof(TComponent));
            return this;
        }

        //IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.WithParameters( IDictionary<string, Object> parameters )
        //{
        //    this.WithParameters( parameters );
        //    return this;
        //}

        IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.WithLifestyle(Lifestyle lifestyle)
        {
            this.Lifestyle = lifestyle;
            return this;
        }

        IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.UsingFactory(Func<T> factory)
        {
            this.UsingFactory(factory);
            return this;
        }

        IPuzzleContainerEntry IPuzzleContainerEntry.UsingFactory(Func<Object> factory)
        {
            this.UsingFactory(factory);
            return this;
        }

        IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.UsingInstance<TComponent>(TComponent instance)
        {
            Func<T> factory = () => instance;
            this.UsingFactory(factory);
            return this;
        }

        IPuzzleContainerEntry IPuzzleContainerEntry.UsingInstance(Object instance)
        {
            Func<Object> factory = () => instance;
            this.UsingFactory(factory);
            return this;
        }

        /// <summary>
        /// Defines the type that implements the service.
        /// </summary>
        /// <param name="componentType">The type of the component.</param>
        /// <returns>This entry instance.</returns>
        IPuzzleContainerEntry IPuzzleContainerEntry.ImplementedBy(Type componentType)
        {
            this.ImplementedBy(componentType);
            return this;
        }

        //IPuzzleContainerEntry IPuzzleContainerEntry.WithParameters( IDictionary<string, Object> parameters )
        //{
        //    this.WithParameters( parameters );
        //    return this;
        //}

        /// <summary>
        /// Defines the lifestyle of this entry.
        /// </summary>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <returns>This entry instance.</returns>
        IPuzzleContainerEntry IPuzzleContainerEntry.WithLifestyle(Lifestyle lifestyle)
        {
            this.Lifestyle = lifestyle;
            return this;
        }

        IPuzzleContainerEntry IPuzzleContainerEntry.Overridable()
        {
            this.IsOverridable = true;

            return this;
        }

        IPuzzleContainerEntry<T> IPuzzleContainerEntry<T>.Overridable()
        {
            this.IsOverridable = true;

            return this;
        }

        public bool IsOverridable { get; private set; }
    }
}
