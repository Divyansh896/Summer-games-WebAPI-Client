using Summer_games_WebAPI_Client.Models;
using Summer_games_WebAPI_Client.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Summer_games_WebAPI_Client.Data
{
    public class SportRepository : ISportRepository
    {
        private readonly HttpClient client = new HttpClient();

        public SportRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Sport>> GetSports()
        {
            HttpResponseMessage response = await client.GetAsync("api/Sports");
            if (response.IsSuccessStatusCode)
            {
                List<Sport> sports = await response.Content.ReadAsAsync<List<Sport>>();
                return sports;
            }
            else
            {
                throw new Exception("Could not access the list of sports.");
            }
        }

        
    }
}
