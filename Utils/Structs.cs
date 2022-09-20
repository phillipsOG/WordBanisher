using ProjectM.Network;
using Unity.Entities;

namespace WordBanisher.Utils
{
    public struct PlayerData
    {
        public string PlayerName { get; set; }
        public ulong PlatformID { get; set; }
        public bool IsTrue { get; set; }
        public Entity UserEntity { get; set; }
        public Entity NetworkUser { get; set; }
        public PlayerData(string playerName = "default", ulong platformID = 0, bool isTrue = false, Entity userEntity = default, Entity networkUser = default)
        {
            PlayerName = playerName;
            PlatformID = platformID;
            IsTrue = isTrue;
            UserEntity = userEntity;
            NetworkUser = networkUser;
        }
    }
}
