using Newtonsoft.Json;

namespace Puniemu.Src.Server.GameServer.DataClasses
{
    public class SkillResult
    {
        // Max level flg
        [JsonProperty("isMaxLevel")]
        public bool isMaxLevel = false;
        // old yokai info
        [JsonProperty("before")]
        public ExpInfo Before = new();
        // new yokai info
        [JsonProperty("after")]
        public ExpInfo After = new();
    }
}
