using Newtonsoft.Json;
using Puniemu.Src.Server.GameServer.DataClasses;
using Puniemu.Src.Server.GameServer.Requests.BuyHitodama.DataClasses;
using Puniemu.Src.Server.GameServer.Requests.InitGacha.DataClasses;
using Puniemu.Src.TableParser.DataClasses;
using Puniemu.Src.TableParser.Logic;
using System.Buffers;
using System.IO;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Transactions;

namespace Puniemu.Src.Server.GameServer.Logic
{
    public class ConditionManager
    {
        public static bool ComputeCondition(ConditionType type, object param1Source, long param1Target)
        {
            if (type == ConditionType.MinScore && Convert.ToInt64(param1Source) >= param1Target)
            {
                return true;
            }
            else if (type == ConditionType.MaxClearTime && Convert.ToInt64(param1Source) <= param1Target)
            {
                return true;
            }
            else if (type == ConditionType.MinLinkSize && Convert.ToInt64(param1Source) >= param1Target)
            {
                return true;
            }
            else if (type == ConditionType.UsedYoukai)
            {
                foreach (UserYoukaiResultListReq item in (List<UserYoukaiResultListReq>)param1Source)
                {
                    if (item.YoukaiId == param1Target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
