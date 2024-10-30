using GraphQL;
using GraphQL.Types;
using GraphQlApi.Controllers;
using GraphQlApi.Repository.Interfaces;
using GraphQlApi.Repository.Services;

namespace GraphQlApi;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services) 
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<UserDetailType>();
        services.AddScoped<UserQuery>();
        services.AddScoped<ISchema, UserDetailsSchema>();
        services.AddScoped<UserMutation>();
        services.AddScoped<UserDetailInputType>();

        //services.AddScoped<UserMutation>();

        return services;
    }
}
