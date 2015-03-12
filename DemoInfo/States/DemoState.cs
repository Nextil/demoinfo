using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.States
{
    public class DemoState
    {
        const int MAXTEAMS = 4;

        private readonly DemoParser Parser;

        public CSTeam CounterTerrorists { get; internal set; }
        public CSTeam Terrorists { get; internal set; }

        public CSTeam[] Teams { get; private set; }

        public List<CSPlayer> Players { get; set; }

        public DemoState(DemoParser parser)
        {
            this.Parser = parser;
            Teams = new CSTeam[MAXTEAMS];
            Players = new List<CSPlayer>();
        }

        public CSPlayer GetPlayerByUserID(int id)
        {
            // The idea here is to use the faster variant "FirstOrDefault" when 
            // in production - but it should always contain only one matching user
            // so let's check that here!
            #if DEBUG_SLOW_MONO
            return Players.SingleOrDefault(a => a.UserID == id);
            #else
            return Players.FirstOrDefault(a => a.UserID == id);
            #endif
        }

        public CSPlayer GetPlayerByEntityIndex(int index)
        {
            return (CSPlayer)Parser.RawData.Entities[index].Instance;
        }
    }
}
