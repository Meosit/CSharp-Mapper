using System;
using System.Collections.Generic;

namespace Mapper
{
    public class FunctionsCache : IFunctionsCache
    {
        private readonly Dictionary<MappingTypeAssociation, Delegate> _cache = new Dictionary<MappingTypeAssociation, Delegate>();

        public void Put<TSource, TDestination>(MappingTypeAssociation typeAssociation,
            Func<TSource, TDestination> mappingFunction)
        {
            _cache.Add(typeAssociation, mappingFunction);
        }

        public Func<TSource, TDestination> Get<TSource, TDestination>(MappingTypeAssociation typeAssociation) => (Func<TSource, TDestination>)_cache[typeAssociation];

        public bool Contains(MappingTypeAssociation typeAssociation) => _cache.ContainsKey(typeAssociation);


    }
}