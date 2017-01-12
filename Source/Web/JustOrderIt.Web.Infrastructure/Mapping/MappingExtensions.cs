namespace JustOrderIt.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using AutoMapper;

    public enum WithBaseFor
    {
        Source,
        Destination,
        Both
    }

    public static class MappingExtensions
    {
        // Original's source:
        //    http://matthewmanela.com/blog/update-on-inheriting-base-type-mappings-with-automapper/
        //    https://github.com/mmanela/InheritedAutoMapper/blob/master/MappingExtensions/MapperExtensions.cs
        public static void InheritMappingFromBaseType<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            WithBaseFor baseFor = WithBaseFor.Both)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var sourceParentType = baseFor == WithBaseFor.Both ||
                                   baseFor == WithBaseFor.Source ? sourceType.BaseType : sourceType;

            var destinationParentType = baseFor == WithBaseFor.Both ||
                                        baseFor == WithBaseFor.Destination ? destinationType.BaseType : destinationType;

            mappingExpression
                .BeforeMap((x, y) => Mapper.Map(x, y, sourceParentType, destinationParentType))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => NotAlreadyMapped(sourceParentType, destinationParentType) && srcMember != null));
        }

        private static bool NotAlreadyMapped(Type sourceParentType, Type desitnationParentType)
        {
            var result = Mapper
                .Configuration
                .FindTypeMapFor(sourceParentType, desitnationParentType)
                .GetPropertyMaps()
                .Where(m => m.DestinationProperty.Name.Equals(m.SourceMember.Name))
                .Select(y => !y.IsMapped())
                .All(b => b);

            return result;
        }
    }
}
