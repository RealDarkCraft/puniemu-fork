using Puniemu.Src.Server.GameServer.DataClasses;
using System.Reflection;
using System.Text;

namespace Puniemu.Src.TableParser.DataClasses
{
    public class YwpMstStageCondition
    {
        public long ConditionId { get; set; }
        public ConditionType ConditionType { get; set; }
        public string? Description { get; set; }
        public long Param1 { get; set; }
        public long Unk1 { get; set; }
        public int Unk2 { get; set; }
        public string? Title { get; set; }
    }
}