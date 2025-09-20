﻿using Newtonsoft.Json;
using Puniemu.Src.Server.GameServer.DataClasses;
using Puniemu.Src.UserDataManager.DataClasses;

namespace Puniemu.Src.Server.GameServer.Requests.GetGdkeyAccounts.DataClasses
{
    public struct UdkeyPlayerItem
    {
        [JsonProperty("iconId")]
        public PlayerIcon IconID { get; set; } // ID of the player's icon.

        [JsonProperty("playerName")]
        public string PlayerName { get; set; } // Name of the player

        [JsonProperty("youkaiId")]
        public long PartnerYokaiID { get; set; } // The center yokai in the party

        [JsonProperty("lastUpdateDate")]
        public string? LastUpdateDate { get; set; } // Last time the game was played

        [JsonProperty("titleId")]
        public PlayerTitle TitleID { get; set; } // Title of the character (kun, chan, etc...)

        [JsonProperty("gdkey")]
        public string GDKey { get; set; } // GDKey

        [JsonProperty("userId")]
        public string UserID { get; set; } // UserID

        [JsonProperty("playStartDate")]
        public string StartDate { get; set; } // First time the game was played

        //because constructors cant be async
        public static async Task<UdkeyPlayerItem?> ConstructAsync(string gdkey)
        {
            YwpUserData? userData;
            try
            {
                userData = await UserDataManager.Logic.UserDataManager.GetYwpUserAsync<YwpUserData>(gdkey, "ywp_user_data");
            }
            catch (UserDataManager.Logic.UserDataManager.TableNotFoundException)
            {
                return null;
            }

            var res = await UserDataManager.Logic.UserDataManager.SupabaseClient.From<Account>().Where(x => x.Gdkey == gdkey).Get();
            var account = res.Model;
            UdkeyPlayerItem playerItem = new();

            playerItem.IconID = (PlayerIcon)userData!.IconID;
            playerItem.PlayerName = userData.PlayerName;
            playerItem.PartnerYokaiID = userData.YoukaiId;
            var startTimestamp = account.StartDate;
            var startTimestampString = DateTimeOffset.FromUnixTimeMilliseconds(startTimestamp).DateTime.ToString("yyyy-MM-dd HH:mm:ss");
            playerItem.StartDate = startTimestampString;
            playerItem.LastUpdateDate = account.LastLoginTime; 
            playerItem.TitleID = (PlayerTitle)userData.CharacterTitleID;
            playerItem.GDKey = gdkey;
            playerItem.UserID = userData.UserID;
            return playerItem;
        }
    }
}
