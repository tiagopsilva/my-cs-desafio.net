using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace AuthAPI.Infra.Data.EF.Extensions
{
    public static class PrimitivePropertyConfigurationExtensions
    {
        public static PrimitivePropertyConfiguration IsUniqueKey(
            this PrimitivePropertyConfiguration propertyConfiguration, string indexName, int order = 0)
        {
            propertyConfiguration
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute(indexName) { IsUnique = true, Order = order }));

            return propertyConfiguration;
        }
    }
}