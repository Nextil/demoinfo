using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("player_death")]
    public class PlayerDeath : BaseEvent
    {
        /// <summary>
        /// Gets the weapon used to kill the enemy.
        /// </summary>
        [NetworkedProperty("weapon")]
        public NetworkedVar<string> Weapon { get; private set; }

        /// <summary>
        /// Gets the weapons item ID.
        /// </summary>
        [NetworkedProperty("weapon_itemid")]
        public NetworkedVar<string> WeaponItemID { get; private set; }

        /// <summary>
        /// Gets the weapons "faux" item ID.
        /// (I'm not sure what this is - if you know it, create a pullrequest on GitHub)
        /// </summary>
        [NetworkedProperty("weapon_fauxitemid")]
        public NetworkedVar<string> WeaponFauxItemID { get; private set; }

        /// <summary>
        /// Gets the weapons's original owner. 
        /// </summary>
        [NetworkedProperty("weapon_originalowner_xuid")]
        public NetworkedVar<string> WeaponOriginalownerSteamID { get; private set; }

        /// <summary>
        /// Gets whether this kill was a headshot
        /// </summary>
        [NetworkedProperty("headshot")]
        public NetworkedVar<bool> Headshot { get; private set; }

        /// <summary>
        /// Gets whether this kill was a domination
        /// (I don't know what the integer value is - if you know it, create a pullrequest on GitHub)
        /// </summary>
        [NetworkedProperty("dominated")]
        public NetworkedVar<int> Domination { get; private set; }

        /// <summary>
        /// Gets whether this kill was a revenge.
        /// (I don't know what the integer value is - if you know it, create a pullrequest on GitHub)
        /// </summary>
        [NetworkedProperty("revenge")]
        public NetworkedVar<int> Revenge { get; private set; }

        /// <summary>
        /// ( ͡° ͜ʖ ͡°)
        /// (The number of walls that were penetrated with the shot)
        /// </summary>
        [NetworkedProperty("penetrated")]
        public NetworkedVar<int> Penetrated { get; private set; }


        [NetworkedProperty("userid")]
        NetworkedVar<int> UserID { get; set; }

        [NetworkedProperty("attacker")]
        NetworkedVar<int> AttackerUID { get; set; }

        [NetworkedProperty("assister")]
        NetworkedVar<int> AssisterUID { get; set; }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public CSPlayer Player {
            get {
                return EventInfo.Parser.GameState.GetPlayerByUserID(UserID);
            }
        }

        /// <summary>
        /// Gets the attacker. Keep in mind that this can be null, e.g. when
        /// the world killed. 
        /// </summary>
        /// <value>The attacker.</value>
        public CSPlayer Attacker {
            get {
                return EventInfo.Parser.GameState.GetPlayerByUserID(AttackerUID);
            }
        }

        /// <summary>
        /// Gets the assister. Returns null if there was no assister. 
        /// </summary>
        /// <value>The assister.</value>
        public CSPlayer Assister {
            get {
                return EventInfo.Parser.GameState.GetPlayerByUserID(AssisterUID);
            }
        }


        public PlayerDeath()
        {
        }

        internal override void HandleYourself()
        {

        }
    }
}

