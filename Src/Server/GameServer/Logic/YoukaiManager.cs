using Newtonsoft.Json;
using Puniemu.Src.Server.GameServer.DataClasses;
using Puniemu.Src.Server.GameServer.Requests.InitGacha.DataClasses;
using System.Buffers;
using System.IO;
using System.Text;

namespace Puniemu.Src.Server.GameServer.Logic
{
    public class YoukaiManager
    {
        public static TableParser.Logic.TableParser AddYoukai(TableParser.Logic.TableParser youkaiList, long YoukaiId)
        {
            var YoukaiMstTable = new TableParser.Logic.TableParser(JsonConvert.DeserializeObject<Dictionary<string, string>>(DataManager.Logic.DataManager.GameDataManager!.GamedataCache["ywp_mst_youkai"]!)!["tableData"]);
            var YoukaiLevelMstTable = new TableParser.Logic.TableParser(JsonConvert.DeserializeObject<Dictionary<string, string>>(DataManager.Logic.DataManager.GameDataManager.GamedataCache["ywp_mst_youkai_level"]!)!["tableData"]);
            var UserYoukaiIndex = youkaiList.FindIndex([YoukaiId.ToString()]);
            var MstYoukaiIndex = YoukaiMstTable.FindIndex([YoukaiId.ToString()]);
            if (UserYoukaiIndex == -1)
            {
                // add new youkai
                var tmpIdx = 0;
                var levelType = int.Parse(YoukaiMstTable.Table[MstYoukaiIndex][5]);
                foreach (string[] str in YoukaiLevelMstTable.Table)
                {
                    if (str[0] == levelType.ToString() && str[1] == "1")
                        break;
                    tmpIdx++;
                }
                // set Youkai id, level, total exp, hp, attack power, exp limit (for this level), exp (for this level), percentage of exp limit and exp (for this level), actual date, unk (maybe paid level)
                youkaiList.AddRow([YoukaiId.ToString(), "1", "0", YoukaiMstTable.Table[MstYoukaiIndex][8], YoukaiMstTable.Table[MstYoukaiIndex][10], YoukaiLevelMstTable.Table[tmpIdx][3], "0", "0", "0", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), "0"]);
            }
            return new TableParser.Logic.TableParser(youkaiList.ToString());
        }
        static int SoulLevelFormula(int n)
        {
            if (n == 0)
                return 0;
            return Math.Max(1, (n * (n + 1)) / 3) * 1000;
        }
        static int SoulLevel(int n)
        {
            int points = 0;
            for (int i = 1; i < 7; i++)
            {
                points += SoulLevelFormula(i);
                if (n < points)
                    return i;
            }
            return 7;
        }
        static int SoulPoints(int n)
        {
            int points = 0;
            for (int i = 1; i <= n; i++)
            {
                points += SoulLevelFormula(i);
            }
            return points;
        }
        public static TableParser.Logic.TableParser AddYoukaiSkill(TableParser.Logic.TableParser youkaiList, long YoukaiId)
        {
            var UserYoukaiIndex = youkaiList.FindIndex([YoukaiId.ToString()]);
            if (UserYoukaiIndex == -1)
            {
                youkaiList.AddRow([YoukaiId.ToString(), "1", "0", "1000", "0", "0"]);
            }
            else
            {
                int amuLevel = int.Parse(youkaiList.Table[UserYoukaiIndex][1]);
                if (amuLevel >= 7)
                {
                    return new TableParser.Logic.TableParser(youkaiList.ToString());
                }
                int current_points = int.Parse(youkaiList.Table[UserYoukaiIndex][2]);
                int new_points = 1000 + current_points;
                int new_level = SoulLevel(new_points);

                int denominator = SoulLevelFormula(new_level);
                int numerator = new_points - SoulPoints(new_level - 1);
                int percentage = (int)(((double)numerator / (double)denominator) * 100);
                youkaiList.Table[UserYoukaiIndex][1] = new_level.ToString();
                youkaiList.Table[UserYoukaiIndex][2] = new_points.ToString();
                youkaiList.Table[UserYoukaiIndex][3] = denominator.ToString();
                youkaiList.Table[UserYoukaiIndex][4] = numerator.ToString();
                youkaiList.Table[UserYoukaiIndex][5] = percentage.ToString();
            }
            return new TableParser.Logic.TableParser(youkaiList.ToString());
        }
    }
}
