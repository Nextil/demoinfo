using System;
using EHVAG.DemoInfo.Utils.Reflection;
using EHVAG.DemoInfo.Edicts;

namespace EHVAG.DemoInfo.DemoPackets.GameEvents.Events
{
    [ValveEvent("cs_win_panel_round")]
    public class CSWinPanelRound : BaseEvent
    {
        /// <summary>
        /// Gets the "show_timer_defend"-property of this event. 
        /// (If you know what this exactly is, create a pull-request)
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("show_timer_defend")]
        public NetworkedVar<bool> ShowTimerDefend { get; private set; }


        /// <summary>
        /// Gets the "show_timer_attack"-property of this event. 
        /// (If you know what this exactly is, create a pull-request)
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("show_timer_attack")]
        public NetworkedVar<bool> ShowTimerAttack { get; private set; }

        /// <summary>
        /// Gets the "timer_time"-property of this event. 
        /// (If you know what this exactly is, create a pull-request)
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("timer_time")]
        public NetworkedVar<int> TimerTime { get; private set; }

        /// <summary>
        /// Gets the "final_event"-property of this event. It's a byte. 
        /// (If you know what this exactly is, create a pull-request)
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("final_event")]
        public NetworkedVar<int> FinalEvent { get; private set; }

        /// <summary>
        /// Gets a string describing which FunFact to display. 
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("funfact_token")]
        public NetworkedVar<string> FunfactToken { get; private set; }

        /// <summary>
        /// Gets a string describing which FunFact to display. 
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("funfact_player")]
        NetworkedVar<int> FunfactPlayerID { get; set; }

        /// <summary>
        /// Gets the funfact-data. 
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("funfact_data1")]
        public NetworkedVar<int> FunfactData1 { get; private set; }

        /// <summary>
        /// Gets the funfact-data. 
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("funfact_data2")]
        public NetworkedVar<int> FunfactData2 { get; private set; }

        /// <summary>
        /// Gets the funfact-data. 
        /// </summary>
        /// <value>The show timer defend.</value>
        [NetworkedProperty("funfact_data3")]
        public NetworkedVar<int> FunfactData3 { get; private set; }

        public CSPlayer FunfactPlayer {
            get { return EventInfo.Parser.GameState.GetPlayerByUserID(FunfactPlayerID); }
        }

        public CSWinPanelRound()
        {
        }

        internal override void HandleYourself()
        {

        }
    }
}

