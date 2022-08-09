﻿using Microsoft.Extensions.DependencyInjection;
using Shared;
using Volo.Abp.Autofac;
using Volo.Abp.Dapr;
using Volo.Abp.Http.Client.ClientProxying;
using Volo.Abp.Http.Client.Dapr;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace DaprClient;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpHttpClientDaprModule),
    typeof(AbpValidationModule),
    typeof(SharedModule)
)]
public class DaprClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";
    
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpDaprOptions>(options =>
        {
            options.HttpEndpoint = "http://localhost:3500";
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(typeof(SharedModule).Assembly, RemoteServiceName);
    }
}