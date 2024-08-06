using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryAPI.Entities.Filters
{
    // This class implements ISchemaFilter to customize the Swagger schema generation process.
    public class SwaggerIgnoreFilter : ISchemaFilter
    {
        // Applies the filter to modify the Swagger schema for types that are processed.
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // If the schema or context type is null, exit early as there's nothing to process.
            if (schema.Properties == null || context.Type == null)
                return;

            // Retrieve properties of the current type that have the SwaggerIgnoreAttribute applied.
            var propertiesToIgnore = context.Type.GetProperties()
                .Where(prop => prop.GetCustomAttributes(typeof(SwaggerIgnoreAttribute), false).Any());

            // Iterate over the properties that need to be ignored.
            foreach (var property in propertiesToIgnore)
            {
                // Find the property in the schema's properties by matching the property name.
                var propertyName = schema.Properties.Keys.FirstOrDefault(key => string.Equals(key, property.Name, StringComparison.OrdinalIgnoreCase));

                // If the property is found in the schema, remove it.
                if (propertyName != null)
                    schema.Properties.Remove(propertyName);
            }
        }
    }
}