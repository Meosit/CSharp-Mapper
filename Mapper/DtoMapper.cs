using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mapper
{
    public class DtoMapper : IMapper
    {
        private readonly IFunctionsCache _cache;
        private readonly IFunctionBuilder _builder;
        
        public DtoMapper(IFunctionBuilder builder, IFunctionsCache cache)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("Builder is null");
            }
            if (cache == null)
            {
                throw new ArgumentNullException("Cache is null");
            }
            _builder = builder;
            _cache = cache;
        }

        public DtoMapper()
        {
            _builder = new FunctionBuilder();
            _cache = new FunctionsCache();
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            Func<TSource, TDestination> func;
            MappingTypeAssociation typeAssociation = new MappingTypeAssociation(typeof(TDestination), typeof(TSource));
            if (_cache.Contains(typeAssociation))
            {
                func = _cache.Get<TSource, TDestination>(typeAssociation);
            }
            else
            {
                func = _builder.Build<TSource, TDestination>(GetMappingProperties(typeAssociation));
                _cache.Put(typeAssociation, func);
            }
            return func.Invoke(source);
        }

        private List<MappingProperty> GetMappingProperties(MappingTypeAssociation typeAssociation)
        {
            PropertyInfo[] sourceProperties = typeAssociation.Source.GetProperties();
            PropertyInfo[] destinationProperties = typeAssociation.Destination.GetProperties();

            List<MappingProperty> mappingProperties = new List<MappingProperty>();
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                foreach (PropertyInfo destinationProperty in destinationProperties)
                {
                    if (sourceProperty.Name.Equals(destinationProperty.Name) 
                        && TypesAssignInfo.CanAssign(sourceProperty.PropertyType, destinationProperty.PropertyType))
                    {
                        mappingProperties.Add(new MappingProperty(sourceProperty, destinationProperty));
                    }
                }
            }
            return mappingProperties;
        }
    }
}