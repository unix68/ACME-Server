﻿using TG_IT.ACME.Protocol.Storage;
using TG_IT.ACME.Protocol.Storage.FileStore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FileStoreServiceCollectionExtensions
    {
        public static IServiceCollection AddACMEFileStore(this IServiceCollection services)
        {
            services.AddScoped<INonceStore, NonceStore>();
            services.AddScoped<IAccountStore, AccountStore>();
            services.AddScoped<IOrderStore, AccountStore>();

            return services;
        }
    }
}