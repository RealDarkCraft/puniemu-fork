using Newtonsoft.Json;
using Puniemu.Src.Server.GameServer.Requests.BuyHitodama.DataClasses;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace Puniemu.Src.Server.GameServer.DataClasses
{
    public enum ConditionType
    {
        MinScore = 1,
        UsedYoukai = 2,
        MaxClearTime = 3,
        MinLinkSize = 10
    }
}
