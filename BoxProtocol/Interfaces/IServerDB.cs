using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxProtocol.Models;
using Xamarin.Essentials;
using MagicOnion;

namespace BoxProtocol.Interfaces
{
    public interface IServerDB : IService<IServerDB>
    {
        UnaryResult<bool> Add(Item item);
       
        UnaryResult<List<Item>>  GetAll() ;

    }
}
