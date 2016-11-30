using System;
using System.Collections.Generic;

namespace Mapper
{
    public class MappingFunctionBuilder
    {
        internal Func<TSource, TDestination> BuildMappingFunction<TSource, TDestination>(
            List<MappingProperty> mappingProperties) where TDestination : new()
        {
            return null;
        }
    }
}