using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace tutorial_backend_dotnet.Presentation.Filters
{
    public class DefaultUserIdParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name == "userId" && parameter.Schema.Type == "integer")
            {
                parameter.Schema.Default = new OpenApiInteger(123);
            }
        }
    }
}
