﻿using Newtonsoft.Json;

namespace Puniemu.Src.Server.GameServer.Requests.FriendRequest.DataClasses
{
    public class FriendRequestRequest
    {
        // Client version
        [JsonProperty("appVer")]
        public string AppVer { get; set; }

        // Equivalent to UDKey
        [JsonProperty("deviceID")]
        public string DeviceID { get; set; }

        // ID of the character (current account)
        [JsonProperty("targetUserId")]
        public string TargetUserId { get; set; }

        // Interchangeable with gdkey
        [JsonProperty("level5UserID")]
        public string Level5UserID { get; set; }

        // Version of server data
        [JsonProperty("mstVersionVer")]
        public int MstVersionVer { get; set; }

        // OS type. 2 is Android.
        [JsonProperty("osType")]
        public int OsType { get; set; }

        // Always 0 here
        [JsonProperty("userID")]
        public string UserID { get; set; }

        // Always 0
        [JsonProperty("ywpToken")]
        public string YwpToken { get; set; }
    }
}
