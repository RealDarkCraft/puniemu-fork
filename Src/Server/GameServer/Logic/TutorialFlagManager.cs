using Newtonsoft.Json;
using Puniemu.Src.Server.GameServer.Requests.InitGacha.DataClasses;
using System.Buffers;
using System.Text;
using Puniemu.Src.TableParser.DataClasses;
using Puniemu.Src.TableParser.Logic;

namespace Puniemu.Src.Server.GameServer.Logic
{
    public class TutorialFlagManager
    {
        public static void EditTutorialFlg(ref TableParser<YwpUserTutorialList> parser, int TutorialType, int TutorialId, int TutorialValue)
        {
            int index = GetTutorialFlgIndex(ref parser, TutorialId, TutorialType);
            if (index == -1)
            {
                parser.AddItem(new YwpUserTutorialList { TutorialId = TutorialId, TutorialType = TutorialType, TutorialValue = TutorialValue });
                return;
            }
            parser.Items[index].TutorialValue = TutorialValue;
            parser.Items[index].TutorialType = TutorialType;
        }
        public static int GetTutorialFlgIndex(ref TableParser<YwpUserTutorialList> parser, int TutorialId, int TutorialType)
        {
            uint count = 0;
            foreach (YwpUserTutorialList i in parser.Items)
            {
                if (i.TutorialId == TutorialId && i.TutorialType == TutorialType)
                {
                    return (int)count;
                }
                count += 1;
            }
            return -1;
        }
        public static int GetStatus(ref TableParser<YwpUserTutorialList> parser, int TutorialId, int TutorialType)
        {
            int index = GetTutorialFlgIndex(ref parser, TutorialId, TutorialType);
            if (index == -1)
            {
                return -1;
            }
            return parser.Items[index].TutorialValue;
        }
    }
}
