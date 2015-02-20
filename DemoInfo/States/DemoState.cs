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

        internal CSPlayer GetPlayerByUserID(int id)
        {
            return Players.SingleOrDefault(a => a.UserID == id);
        }

    }
}
