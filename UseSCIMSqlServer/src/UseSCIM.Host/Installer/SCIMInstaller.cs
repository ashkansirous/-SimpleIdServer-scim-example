using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleIdServer.Scim;
using SimpleIdServer.Scim.Api;
using SimpleIdServer.Scim.Commands.Handlers;
using SimpleIdServer.Scim.Domain;
using SimpleIdServer.Scim.Helpers;
using SimpleIdServer.Scim.Infrastructure.Lock;
using SimpleIdServer.Scim.Persistence;
using SimpleIdServer.Scim.Persistence.InMemory;
using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using UseSCIM.Host.Repository;

namespace UseSCIM.Host.Installer
{
    public static class SCIMInstaller
    {
        public static IServiceCollection AddSCIM(this IServiceCollection services, Action<SCIMHostOptions> options, List<SCIMSchema> schemas, Action<IServiceCollectionBusConfigurator> massTransitOptions = null)
        {
            services.Configure(options);
            services.AddMassTransit(massTransitOptions ?? (o =>
            {
                o.UsingInMemory();
            }));
            services.AddCommandHandlers()
                .AddSCIMRepository(schemas)
                .AddHelpers()
                .AddInfrastructure();
            return services;
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddTransient<IAddRepresentationCommandHandler, AddRepresentationCommandHandler>();
            services.AddTransient<IDeleteRepresentationCommandHandler, DeleteRepresentationCommandHandler>();
            services.AddTransient<IReplaceRepresentationCommandHandler, ReplaceRepresentationCommandHandler>();
            services.AddTransient<IPatchRepresentationCommandHandler, PatchRepresentationCommandHandler>();
            services.AddTransient<ISCIMRepresentationHelper, SCIMRepresentationHelper>();
            return services;
        }

        private static IServiceCollection AddSCIMRepository(this IServiceCollection services, List<SCIMSchema> schemas)
        {
            var provisioningConfigurations = new List<ProvisioningConfiguration>();
            services.AddTransient<ISCIMRepresentationCommandRepository, RepresentationCommandRepository>();
            services.AddTransient<ISCIMRepresentationQueryRepository, RepresentationQueryRepository>();
            services.TryAddSingleton<ISCIMSchemaCommandRepository>(new DefaultSchemaCommandRepository(schemas));
            services.TryAddSingleton<ISCIMSchemaQueryRepository>(new DefaultSchemaQueryRepository(schemas));
            services.TryAddSingleton<ISCIMAttributeMappingQueryRepository>(new DefaultAttributeMappingQueryRepository(new EditableList<SCIMAttributeMapping>()));
            services.TryAddSingleton<IProvisioningConfigurationRepository>(new DefaultProvisioningConfigurationRepository(provisioningConfigurations));
            return services;
        }

        private static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<IAttributeReferenceEnricher, AttributeReferenceEnricher>();
            return services;
        }

        private static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.TryAddSingleton<IDistributedLock, InMemoryDistributedLock>();
            foreach (var assm in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.RegisterAllAssignableType(typeof(BaseApiController), assm, true);
            }


            return services;
        }
    }
}
