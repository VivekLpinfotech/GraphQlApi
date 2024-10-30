using GraphQL;
using GraphQlApi.Controllers;
using GraphQlApi.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQlApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        
        // Add services to the container.
        builder.Services.RegisterDependencies();

        builder.Services.AddGraphQL(b => b
            .AddAutoSchema<UserQuery>()
            .AddSystemTextJson().AddGraphTypes());

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        builder.Services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value ?? "";
        builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAllOrigins");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseWebSockets();
        app.UseGraphQL("/graphql");            
        app.UseGraphQLPlayground(
            "/",                               
            new GraphQL.Server.Ui.Playground.PlaygroundOptions
            {
                GraphQLEndPoint = "/graphql",         
                SubscriptionsEndPoint = "/graphql",   
            });

        app.Run();
    }
}