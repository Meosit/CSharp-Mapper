using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mapper
{
    public class FunctionBuilder : IFunctionBuilder
    {
        public Func<TSource, TDestination> Build<TSource, TDestination>(
            List<MappingProperty> mappingProperties) where TDestination : new()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "source");
            List<MemberBinding> memberBindings = new List<MemberBinding>(mappingProperties.Count);

            foreach (MappingProperty property in mappingProperties)
            {
                Expression propertyAccessExpression = Expression.Property(parameterExpression, property.Source);
                Expression convertExpression = Expression.Convert(propertyAccessExpression, property.Destination.PropertyType);
                memberBindings.Add(Expression.Bind(property.Destination, convertExpression));
            }

            Expression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindings);

            var func = Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, parameterExpression).Compile();

            return func;
        }
    }
}