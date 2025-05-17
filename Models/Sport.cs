using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer_games_WebAPI_Client.Models
{
    public class Sport
    {
        public int ID { get; set; }


        public string Code { get; set; } = "";

        public string Name { get; set; } = "";
        
        public byte[] RowVersion { get; set; }//Added for concurrency 
        public ICollection<Athlete> Athletes { get; set; }
    }
}
