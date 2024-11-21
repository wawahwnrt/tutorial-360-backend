using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace tutorial_backend_dotnet.Presentation.Filters
{
    public class DefaultRoleIdParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name == "roleId" && parameter.Schema.Type == "integer")
            {
                parameter.Schema.Default = new OpenApiInteger(1);
            }
        }
    }
}
