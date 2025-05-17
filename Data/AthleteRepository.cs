using Summer_games_WebAPI_Client.Models;
using Summer_games_WebAPI_Client.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Threading.Tasks;

namespace Summer_games_WebAPI_Client.Data
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly HttpClient client = new HttpClient();

        public AthleteRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Athlete>> GetAthletes()
        {
            HttpResponseMessage response = await client.GetAsync("api/Athletes");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return athletes;
            }
            else
            {
                throw new Exception("Could not access the list of Athletes.");
            }
        }

        public async Task<Athlete> GetAthlete(int AthleteID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Athletes/{AthleteID}");
            if (response.IsSuccessStatusCode)
            {
                Athlete athlete = await response.Content.ReadAsAsync<Athlete>();
                return athlete;
            }
            else
            {
                throw new Exception("Could not access that Athlete.");
            }
        }

        public async Task<List<Athlete>> GetAthletesBySportID(int SportID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Athletes/BySport/{SportID}");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return athletes;
            }
            else
            {
                throw new Exception("Could not access Athletes by Sport.");
            }
        }

        public async Task<List<Athlete>> GetAthletesByContingentID(int ContingentID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Athletes/ByContingent/{ContingentID}");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return athletes;
            }
            else
            {
                throw new Exception("Could not access Athletes by Contingent.");
            }
        }

        public async Task<List<Athlete>> GetAthletesBySportAndContingent(int SportID, int ContingentID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/Athletes/BySportAndContingent/{SportID}/{ContingentID}");
            if (response.IsSuccessStatusCode)
            {
                List<Athlete> athletes = await response.Content.ReadAsAsync<List<Athlete>>();
                return athletes;
            }
            else
            {
                throw new Exception("Could not access Athletes by Sport and Contingent.");
            }
        }

        public async Task AddAthlete(Athlete athleteToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/Athletes", athleteToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateAthlete(Athlete athleteToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/Athletes/{athleteToUpdate.ID}", athleteToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteAthlete(Athlete athleteToDelete)
        {
            var response = await client.DeleteAsync($"/api/Athletes/{athleteToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
