using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelix.Data
{
    public class GameData
    {
        public GameData(String hostId, int version)
        {
            UserId1 = hostId;
            Version = version;
            CreatedAt = DateTime.Now;
            NbPlayers = 8;
            
            UserId2 = null;
            UserId3 = null;
            UserId4 = null;
            UserId5 = null;
            UserId6 = null;
            UserId7 = null;
            UserId8 = null;
        }

        public String Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Version { get; set; }
        public int NbPlayers { get; set; }
        public String UserId1 { get; set; }
        public String UserId2 { get; set; }
        public String UserId3 { get; set; }
        public String UserId4 { get; set; }
        public String UserId5 { get; set; }
        public String UserId6 { get; set; }
        public String UserId7 { get; set; }
        public String UserId8 { get; set; }

        public bool Started { get; set; }

        public bool Ended { get; set; }

    }
}
