using System.Reflection;
using System.Text;

namespace Puniemu.Src.TableParser.DataClasses
{
    // ywp_user_tutorial_list table definition

    public class YwpUserTutorialList
    {
        public int TutorialType { get; set; }
        public long TutorialId { get; set; }
        public int TutorialValue { get; set; }
    }
}