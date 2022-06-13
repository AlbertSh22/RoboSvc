using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace PublicApi.Filters
{
    using Validation;

    /// <summary>
    ///     Implements the ISchemaFilter interface.
    /// </summary>
    public class AddUniquenessDescriptionFilter : ISchemaFilter
    {
        /// <summary>
        ///     Adds custom ValidationAttribute to Swagger documentation
        /// </summary>
        /// <param name="schema">
        ///     The Schema Object allowing definition of input and 
        ///     output data types.
        /// </param>
        /// <param name="context">
        ///     The current context of the schema.
        /// </param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var attr = context.MemberInfo?.CustomAttributes.Where(x =>
                x.AttributeType.Name == nameof(UniqueAttribute))
                .FirstOrDefault();

            if (attr is not null)
            { 
                schema.Extensions.Add(
                    "isUnique",
                    new OpenApiBoolean(true));
            }
        }
    }
}
