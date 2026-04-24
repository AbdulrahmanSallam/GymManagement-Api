using GymManagement.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace GymManagement.Infrastructure;



public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();

        return builder;
    }
}