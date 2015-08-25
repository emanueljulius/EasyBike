﻿using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using EasyBike.Models;
using EasyBike.Config;
using EasyBike.Design;
using GalaSoft.MvvmLight.Views;
using EasyBike.Models.Storage;
using EasyBike.Notification;
using EasyBike.Services;
using EasyBike.Models.Favorites;

namespace EasyBike.ViewModels
{
    public class ViewModelLocator
    {
        public const string AddCommentPageKey = "AddCommentPage";
        public const string ContractsPageKey = "ContractsPage";
        public const string SettingsPageKey = "SettingsPage";
        public const string FavoritesPageKey = "Favorites";
        public const string AboutPageKey = "AboutPage";

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ContractsViewModel ContractsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ContractsViewModel>();
            }
        }

        public FavoritesViewModel FavoritesViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<FavoritesViewModel>();
            }
        }
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IStorageService, StorageService>();
            SimpleIoc.Default.Register<IRefreshService, RefreshService>(true);
            SimpleIoc.Default.Register<INotificationService, NotificationService>();
            SimpleIoc.Default.Register<IFavoritesService, FavoritesService>();

            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IContractService, DesignContractsService>();
                SimpleIoc.Default.Register<INavigationService, DesignNavigationService>();
            }
            else
            {
                SimpleIoc.Default.Register<IContractService, ContractService>(true);
                // SimpleIoc.Default.Register<ContractsViewModel>();
            }

            SimpleIoc.Default.Register<IConfigService, ConfigService>();

            SimpleIoc.Default.Register<ISettingsService, SettingsService>(true);

            // ViewModels
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<FavoritesViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<ContractsViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }
    }
}