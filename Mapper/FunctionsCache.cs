using System;
using System.Collections.Generic;

namespace Mapper
{
    internal class FunctionsCache
    {
        private readonly Dictionary<MappingTypeAssociation, Delegate> _cache = new Dictionary<MappingTypeAssociation, Delegate>();

        internal void Add<TSource, TDestination>(MappingTypeAssociation typeAssociation,
            Func<TSource, TDestination> mappingFunction)
        {
            _cache.Add(typeAssociation, mappingFunction);
        }

        internal Func<TSource, TDestination> Get<TSource, TDestination>(MappingTypeAssociation typeAssociation) => (Func<TSource, TDestination>)_cache[typeAssociation];

        internal bool Contains(MappingTypeAssociation typeAssociation) => _cache.ContainsKey(typeAssociation);


    }
}