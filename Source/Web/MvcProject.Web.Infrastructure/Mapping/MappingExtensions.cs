namespace MvcProject.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;

    public enum WithBaseFor
    {
        Source,
        Destination,
        Both
    }

    public static class MappingExtensions
    {
        public static void InheritMappingFromBaseType<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            WithBaseFor baseFor = WithBaseFor.Both)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var sourceParentType = baseFor == WithBaseFor.Both || baseFor == WithBaseFor.Source
                                       ? sourceType.BaseType
                                       : sourceType;

            var destinationParentType = baseFor == WithBaseFor.Both || baseFor == WithBaseFor.Destination
                                            ? destinationType.BaseType
                                            : destinationType;

            mappingExpression
                .BeforeMap((x, y) => Mapper.Map(x, y, sourceParentType, destinationParentType))
                .ForAllMembers(x => x.Condition(r => NotAlreadyMapped(sourceParentType, destinationParentType, r)));
        }

        private static bool NotAlreadyMapped(Type sourceType, Type desitnationType, ResolutionContext r)
        {
            var result = !r.IsSourceValueNull &&
                   Mapper.FindTypeMapFor(sourceType, desitnationType)
                        .GetPropertyMaps()
                        .Where(m => m.DestinationProperty.Name.Equals(r.MemberName))
                        .Select(y => !y.IsMapped())
                        .All(b => b);

            return result;
        }
    }
}
