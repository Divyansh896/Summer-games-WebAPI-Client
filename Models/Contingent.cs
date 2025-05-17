using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer_games_WebAPI_Client.Models
{
    public class Contingent
    {
        public int ID { get; set; }


        public string Code { get; set; } = "";


        public string Name { get; set; } = "";

        // Navigation Property
        public ICollection<Athlete> Athletes { get; set; }
    }
}
