﻿using EasyBike.Models.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace EasyBike.Models
{
    public class ContractService : IContractService
    {
        private readonly IStorageService _storageService;
        private readonly IRefreshService _refreshService;

        private List<Contract> contracts = new List<Contract>();
        private List<Station> stations = new List<Station>();

        public ContractService(IStorageService storageService, IRefreshService refreshService)
        {
            _storageService = storageService;
            _refreshService = refreshService;
            GetContractsAsync().ConfigureAwait(false);
        }

        public async Task AddContractAsync(Contract contract)
        {
            await _storageService.StoreContractAsync(contract).ConfigureAwait(false);
            contracts.Add(contract);
            AggregateStations(contract);
        }

        public async Task RemoveContractAsync(Contract contract)
        {
            await _storageService.RemoveContractAsync(contract).ConfigureAwait(false);
            foreach(var station in stations.Where(s=>s.ContractStorageName == contract.StorageName).ToList())
            {
                stations.Remove(station);
            }
            contracts.Remove(contract);
            _refreshService.RemoveContract(contract);
        }

        public List<Station> GetStations()
        {
            return stations;
        }

        public List<Contract> GetStaticContracts()
        {
            return new ContractList().Contracts;
        }

        public async Task<List<Contract>> GetContractsAsync()
        {
            if(contracts.Count == 0)
            {
                var storedContracts = await _storageService.LoadStoredContractsAsync().ConfigureAwait(false);
                contracts = storedContracts.ToList();
                foreach(var contract in contracts)
                {
                    AggregateStations(contract);
                }
            }
            return contracts;
        }

        private void AggregateStations(Contract contract)
        {
            foreach (var station in contract.Stations)
            {
                stations.Add(station);
            }
            _refreshService.AddContract(contract);
        }
    }
}