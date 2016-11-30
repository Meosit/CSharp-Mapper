using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public interface IFunctionBuilder
    {
        Func<TSource, TDestination> Build<TSource, TDestination>(
            List<MappingProperty> mappingProperties) where TDestination : new();
    }
}
