using Summer_games_WebAPI_Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Summer_games_WebAPI_Client.Data
{
    public interface IContingentRepository
    {
        Task<List<Contingent>> GetContingents();
       
    }
}
