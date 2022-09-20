﻿using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using WordBanisher.Utils;

namespace WordBanisher.Hooks
{
    [HarmonyPatch(typeof(HandleCreateCharacterEventSystem), nameof(HandleCreateCharacterEventSystem.TryIsNameValid))]
    public class HandleCreateCharacterEventSystem_Patch
    {
        public static void Postfix(HandleCreateCharacterEventSystem __instance, Entity userEntity, string characterNameString, ref CreateCharacterFailureReason invalidReason, ref bool __result)
        {
            if (__result)
            {
                __result = Helper.ValidateName(characterNameString, out invalidReason);

                var userData = __instance.EntityManager.GetComponentData<User>(userEntity);
                userData.CharacterName = (FixedString64)characterNameString;
                __instance.EntityManager.SetComponentData(userEntity, userData);

                var playerData = new PlayerData(characterNameString, userData.PlatformId, true, userEntity, userData.LocalCharacter._Entity);

                userData = __instance.EntityManager.GetComponentData<User>(userEntity);
                /*Cache.NamePlayerCache[Helper.GetTrueName(characterNameString)] = playerData;
                Cache.SteamPlayerCache[userData.PlatformId] = playerData;*/
            }
        }
    }

}