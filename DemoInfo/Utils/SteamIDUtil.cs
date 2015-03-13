using System;

namespace EHVAG.DemoInfo.Utils
{
    public static partial class Util
    {
        public static long GetCommunityID(string steamID)
        {
            long authServer = Convert.ToInt64(steamID.Substring(8, 1));
            long authID = Convert.ToInt64(steamID.Substring(10));
            return (76561197960265728 + (authID * 2) + authServer);
        }
    }
}

