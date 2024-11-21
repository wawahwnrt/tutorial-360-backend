using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace tutorial_backend_dotnet.Filters
{
    public class DefaultUserIdParameterFilter: IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name == "userId" && parameter.Schema.Type == "integer")
            {
                parameter.Schema.Default = new Microsoft.OpenApi.Any.OpenApiInteger(123);
            }
        }
    }
}
