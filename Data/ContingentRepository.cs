using Summer_games_WebAPI_Client.Models;
using Summer_games_WebAPI_Client.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Summer_games_WebAPI_Client.Data
{
    public class ContingentRepository : IContingentRepository
    {
        private readonly HttpClient client = new HttpClient();

        public ContingentRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Contingent>> GetContingents()
        {
            HttpResponseMessage response = await client.GetAsync("api/Contingents");
            if (response.IsSuccessStatusCode)
            {
                List<Contingent> contingents = await response.Content.ReadAsAsync<List<Contingent>>();
                return contingents;
            }
            else
            {
                throw new Exception("Could not access the list of sports.");
            }
        }

        
    }
}
