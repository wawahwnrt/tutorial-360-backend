using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace tutorial_backend_dotnet.Presentation.Filters
{
    public class DefaultGroupIdParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name != "groupId")
            {
                return;
            }

            parameter.Description = "The ID of the tutorial group";
            parameter.Required = true;
            parameter.Schema.Default = new OpenApiInteger(1);
        }
    }
}
