﻿using System;
using System.Collections;
using System.Collections.Generic;


namespace MapperExpression.Core
{
    /// <summary>
    /// Singleton storage mappers
    /// </summary>
    /// <remarks>Don't need a lazy singleton because this is for all thread</remarks>
    internal class MapperConfigurationContainer :
        IEnumerable<MapperConfigurationBase>
    {

        private List<MapperConfigurationBase> items;
        private static MapperConfigurationContainer currentInstance;

        internal static MapperConfigurationContainer Instance
        {
            get
            {
                if (currentInstance == null)
                {
                    currentInstance = new MapperConfigurationContainer();
                }
                return currentInstance;
            }
        }

        /// <summary>
        /// Gets the number of item.
        /// </summary>
        internal int Count
        {
            get
            {
                return items.Count;
            }
        }

        private MapperConfigurationContainer()
        {
            items = new List<MapperConfigurationBase>();
        }


        /// <summary>
        /// Finds the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="name">The name.</param>
        internal MapperConfigurationBase Find(Type source, Type target, string name = null)
        {

            MapperConfigurationBase result = null;

            if (string.IsNullOrEmpty(name))
            {
                result = items.Find(x => x.Name == x.paramClassSource.Name && x.SourceType == source && x.TargetType == target);
            }
            else
            {
                result = items.Find(x => x.Name == name && x.SourceType == source && x.TargetType == target);
            }

            return result;
        }

        /// <summary>
        /// Existses the specified predigate.
        /// </summary>
        /// <param name="match">The predigate.</param>
        internal bool Exists(Predicate<MapperConfigurationBase> match)
        {
            return items.Exists(match);
        }

        /// <summary>
        /// Returns an enumerator that jaundice within the collection.
        /// </summary>
        /// <returns>
        ///   <see cref="T:System.Collections.Generic.IEnumerator`1" /> can be used to iterate through the collection.
        /// </returns>

        public IEnumerator<MapperConfigurationBase> GetEnumerator()
        {
            return items.GetEnumerator() as IEnumerator<MapperConfigurationBase>;
        }

        /// <summary>
        /// Returns an enumerator that jaundice within the collection.
        /// </summary>
        /// <returns>
        /// Objet <see cref="T:System.Collections.IEnumerator" /> can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        internal void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        internal void Clear()
        {
            items.Clear();
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        internal void Add(MapperConfigurationBase item)
        {
            items.Add(item);
        }
       
    }
}