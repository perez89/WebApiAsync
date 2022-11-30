namespace WebApiAsync;

public static class RegisterCustomServices 
{
    public static void RegisterCustemServices(this IServiceCollection services) {
        services.AddSingleton<IChannel<Product>, QueueChannel<Product>>();
        services.AddScoped<IProductManager, ProductManager>();
        services.AddHostedService<PrimeBgProcess>();
    }
}

