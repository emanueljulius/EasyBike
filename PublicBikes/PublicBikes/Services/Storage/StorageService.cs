﻿using Akavache;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Splat;
using PublicBikes.Services.Settings;
using System;

namespace PublicBikes.Models.Storage
{
    /// <summary>
    /// Akavache documentation : https://github.com/akavache/Akavache
    /// </summary>
    public class StorageService : IStorageService
    {
        public StorageService()
        {
            BlobCache.ApplicationName = "PublicBikes";
            var jsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            Locator.CurrentMutable.RegisterConstant(jsonSettings, typeof(JsonSerializerSettings));
        }

        public async Task StoreContractAsync(Contract contract)
        {
            await BlobCache.LocalMachine.InsertObject(contract.StorageName, contract);
        }

        public async Task RemoveContractAsync(Contract contract)
        {
            await BlobCache.LocalMachine.Invalidate(contract.StorageName);
        }

        public async Task<IEnumerable<Contract>> LoadStoredContractsAsync()
        {
            return await BlobCache.LocalMachine.GetAllObjects<Contract>();
        }

        public async Task ClearAsync()
        {
            await BlobCache.LocalMachine.InvalidateAll();
        }

        public async Task<SettingsModel> GetSettingsAsync()
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<SettingsModel>("settings");
            }
            catch
            {
                return new SettingsModel();
            }
        }

        public async Task SetSettingsAsync(SettingsModel settings)
        {
            await BlobCache.LocalMachine.InsertObject("settings", settings);
        }
    }
}
