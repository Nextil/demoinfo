using System;

namespace EHVAG.DemoInfo.ValveStructs
{
    public enum RoundEndReason
    {
        /// <summary>
        /// Target Successfully Bombed!
        /// </summary>
        TargetBombed = 0,        
        /// <summary>
        /// The VIP has escaped.
        /// </summary>
        VIPEscaped, 
        /// <summary>
        /// VIP has been assassinated
        /// </summary>
        VIPKilled,
        /// <summary>
        /// The terrorists have escaped
        /// </summary>
        TerroristsEscaped,
        /// <summary>
        /// The CTs have prevented most of the terrorists from escaping!
        /// </summary>
        CTStoppedEscape,
        /// <summary>
        /// Escaping terrorists have all been neutralized
        /// </summary>
        TerroristsStopped,
        /// <summary>
        /// The bomb has been defused!
        /// </summary>
        BombDefused,
        /// <summary>
        /// Counter-Terrorists Win!
        /// </summary>
        CTWin,
        /// <summary>
        /// Terrorists Win! 
        /// </summary>
        TerroristWin,
        /// <summary>
        /// Round Draw!
        /// </summary>
        Draw,
        /// <summary>
        /// All Hostages have been rescued
        /// </summary>
        HostagesRescued,
        /// <summary>
        /// Target has been saved! 
        /// </summary>
        TargetSaved,
        /// <summary>
        /// Hostages have not been rescued!
        /// </summary>
        HostagesNotRescued,
        /// <summary>
        /// Terrorists have not escaped!
        /// </summary>
        TerroristsNotEscaped,
        /// <summary>
        /// VIP has not escaped! 
        /// </summary>
        VIPNotEscaped,
        /// <summary>
        /// Game Commencing! 
        /// </summary>
        GameStart,
        /// <summary>
        /// Terrorists Surrender
        /// </summary>
        TerroristsSurrender,
        /// <summary>
        /// CTs Surrender
        /// </summary>
        CTSurrender
    };
}

