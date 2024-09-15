using HK.Payments.Core.Services;
using HK.Payments.Core.Services.Interfaces;

namespace HK.Payments.API.Extensions;

public static class ServiceConfigExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICreditCardService, CreditCardService>();
    }
}