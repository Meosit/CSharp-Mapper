using System;

namespace Mapper
{
    public interface IFunctionsCache
    {
        void Put<TSource, TDestination>(MappingTypeAssociation typeAssociation,
            Func<TSource, TDestination> mappingFunction);

        Func<TSource, TDestination> Get<TSource, TDestination>(MappingTypeAssociation typeAssociation);

        bool Contains(MappingTypeAssociation typeAssociation);
    }
}