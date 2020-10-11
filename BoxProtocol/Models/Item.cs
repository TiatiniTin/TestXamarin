using BoxProtocol.Interfaces;
using System;
using Xamarin.Essentials;
using MessagePack;


namespace BoxProtocol.Models
{
    [MessagePackObject]
    public class Item : IHaveID
    {
        [Key(0)]
        public string Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
      
    }
}