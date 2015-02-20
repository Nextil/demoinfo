using System;
using EHVAG.DemoInfo.Utils.Reflection;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents
{
    [ValveEvent("player_footstep")]
    public class PlayerFootstep : BaseEvent
    {
        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        public override void HandleYourself()
        {
            var player = EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            Console.WriteLine(player.Name + " stepped");
        }
    }
}

