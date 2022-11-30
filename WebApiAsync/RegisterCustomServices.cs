namespace WebApiAsync;

public static class RegisterCustomServices 
{
    public static void RegisterCustemServices(this IServiceCollection services) {
        services.AddSingleton<IChannel<Product>, ChannelX<Product>>();
        services.AddScoped<IProductManager, ProductManager>();
        services.AddHostedService<PrimeBgProcess>();
    }
}

