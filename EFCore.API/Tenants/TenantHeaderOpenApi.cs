using Microsoft.OpenApi;

namespace EFCore.API.Tenants;

public static class TenantHeaderOpenApi
{
    public static IServiceCollection AddTenantOpenApi(this IServiceCollection services)
    {
        return services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                foreach (var path in document.Paths.Values)
                {
                    if (path.Operations is null) continue;

                    foreach (var operation in path.Operations.Values)
                    {
                        if (operation.Parameters is null)
                            operation.Parameters = new List<IOpenApiParameter>();

                        operation.Parameters.Add(new OpenApiParameter
                        {
                            In = ParameterLocation.Header,
                            Name = "X-Tenant",
                            Required = true,
                            Schema = new OpenApiSchema
                            {
                                Type = JsonSchemaType.String
                            },
                        });
                    }
                }

                return Task.CompletedTask;
            });
        });
    }
}