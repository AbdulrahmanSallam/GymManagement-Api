using GymManagement.Application;
using GymManagement.Infrastructure;
using GymManagement.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

{

    builder.Services.AddOpenApi();
    builder.Services.AddControllers();
    builder.Services.AddProblemDetails();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddApplication().AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{

    app.AddInfrastructureMiddlewares();
    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}

