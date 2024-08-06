namespace LibraryAPI.Entities.Filters
{
    // Custom attribute used to indicate that a property should be excluded from Swagger documentation
    [AttributeUsage(AttributeTargets.Property)]  // Specifies that this attribute can be applied to properties
    public class SwaggerIgnoreAttribute : Attribute
    {
        // This class does not contain any members or methods. Its purpose is solely to serve as a marker for properties
        // that should be excluded from the generated Swagger documentation.
    }
}