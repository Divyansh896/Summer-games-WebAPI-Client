using Summer_games_WebAPI_Client.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace Summer_games_WebAPI_Client.Data
{
    public interface IAthleteRepository
    {
        Task<List<Athlete>> GetAthletes();
        Task<Athlete> GetAthlete(int ID);
        Task<List<Athlete>> GetAthletesBySportID(int SportID);  
        Task<List<Athlete>> GetAthletesByContingentID(int ContingentID);
        ///Task<List<Athlete>> GetAthletesBySportAndContingent(int SportID, int ContingentID);  // New method for both
        ///
        Task AddAthlete(Athlete athleteToAdd);
        Task UpdateAthlete(Athlete athleteToUpdate);
        Task DeleteAthlete(Athlete athleteToDelete);
    }
}
