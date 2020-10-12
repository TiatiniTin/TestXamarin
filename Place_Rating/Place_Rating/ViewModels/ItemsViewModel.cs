using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using BoxProtocol;
using BoxProtocol.Models;
using Place_Rating.Views;
using Xamarin.Essentials;
using MagicOnion.Client;
using Grpc.Core;
using BoxProtocol.Interfaces;
using MessagePack;

namespace Place_Rating.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }

        public ItemsViewModel()
        {
            Title = "Places";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            var channel = new Channel("10.0.2.2", 12345, ChannelCredentials.Insecure);
            var DataStore = MagicOnionClient.Create<IServerDB>(channel);

            IsBusy = true;
            var location = Geolocation.GetLastKnownLocationAsync().Result;
            Item item_1 = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = "First person",
            };
            Item item_2 = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Second person",
            };
            await DataStore.Add(item_1);
            await DataStore.Add(item_2);

            try
            {
                Items.Clear();
                var items = await DataStore.GetAll();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
          
        }

        
    }
}