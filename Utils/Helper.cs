using ProjectM;
using ProjectM.Network;
using Unity.Collections;
using System.Text.RegularExpressions;
using ProjectM.Scripting;

namespace WordBanisher.Utils
{
    public static class Helper
    {
        public static ServerGameSettings SGS = default;
        public static ServerGameManager SGM = default;
        public static UserActivityGridSystem UAGS = default;

        public static Regex rxName = new Regex(@"(?<=\])[^\[].*");

        public static bool ValidateName(string name, out CreateCharacterFailureReason invalidReason)
        {
            if (Regex.IsMatch(name, @"[^a-zA-Z0-9]"))
            {
                invalidReason = CreateCharacterFailureReason.InvalidName;
                return false;
            }

            //-- This name is-in our banished words list.
            foreach(var word in Plugin.Database.BanishedWords)
            {
                if(name.ToLower().Equals(word.ToLower()))
                {
                    invalidReason = CreateCharacterFailureReason.InvalidName;
                    return false;
                }
            }

            //-- The game default max byte length is 20.
            //-- The max legth assignable is actually 61 bytes.
            FixedString64 charName = name;
            if (charName.utf8LengthInBytes > 20)
            {
                invalidReason = CreateCharacterFailureReason.InvalidName;
                return false;
            }

            Plugin.Logger.LogWarning($"[Banished Word][{Plugin.Database.BanishedWords[0]}]");

            invalidReason = CreateCharacterFailureReason.None;
            return true;
        }
    }
}