using Newtonsoft.Json;

namespace Puniemu.Src.Server.GameServer.DataClasses
{
    public class UserYoukaiResultListRes
    {
        // IDK but maybe has level up flg
        [JsonProperty("haveFlg")]
        public bool HaveFlg = false;
        // Max level flg
        [JsonProperty("isMaxLevel")]
        public bool isMaxLevel = false;
        // level lock (paid) flg
        [JsonProperty("isLockLevel")]
        public bool IsLockLevel = false;
        // old yokai info
        [JsonProperty("before")]
        public ExpInfo Before = new();
        // new yokai info
        [JsonProperty("after")]
        public ExpInfo After = new();
        // yokai id
        [JsonProperty("youkaiId")]
        public long youkaiId = 0L;
        // can evolve flg
        [JsonProperty("canEvolve")]
        public bool CanEvolve = false;

    }
}
