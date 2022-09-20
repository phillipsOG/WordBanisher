using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using Unity.Entities;
using UnityEngine;
using WordBanisher;
using WordBanisher.Systems;

[assembly: AssemblyVersion(BuildConfig.Version)]
[assembly: AssemblyTitle(BuildConfig.Name)]

namespace WordBanisher
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        private Harmony harmony;
        public static ManualLogSource Logger;
        public static Database Database;
        public static string ModFolder = "BepInEx/config/" + BuildConfig.PackageID + "";
        public static bool isInitialized = false;

        private static ConfigEntry<string> BannedWordOne;
        private static ConfigEntry<string> BannedWordTwo;
        private static ConfigEntry<string> BannedWordThree;
        private static ConfigEntry<string> BannedWordFour;
        private static ConfigEntry<string> BannedWordFive;
        private static ConfigEntry<string> BannedWordSix;
        private static ConfigEntry<string> BannedWordSeven;
        private static ConfigEntry<string> BannedWordEight;
        private static ConfigEntry<string> BannedWordNine;
        private static ConfigEntry<string> BannedWordTen;


        public void InitConfig()
        {
            BannedWordOne = Config.Bind("Word Banisher", "Banished Word One", "", "Enter a word you want banished");
            BannedWordTwo = Config.Bind("Word Banisher", "Banished Word Two", "", "Enter a word you want banished");
            BannedWordThree = Config.Bind("Word Banisher", "Banished Word Three", "", "Enter a word you want banished");
            BannedWordFour = Config.Bind("Word Banisher", "Banished Word Four", "", "Enter a word you want banished");
            BannedWordFive = Config.Bind("Word Banisher", "Banished Word Five", "", "Enter a word you want banished");
            BannedWordSix = Config.Bind("Word Banisher", "Banished Word Six", "", "Enter a word you want banished");
            BannedWordSeven = Config.Bind("Word Banisher", "Banished Word Seven", "", "Enter a word you want banished");
            BannedWordEight = Config.Bind("Word Banisher", "Banished Word Eight", "", "Enter a word you want banished");
            BannedWordNine = Config.Bind("Word Banisher", "Banished Word Nine", "", "Enter a word you want banished");
            BannedWordTen = Config.Bind("Word Banisher", "Banished Word Ten", "", "Enter a word you want banished");
        }

        public override void Load()
        {
            InitConfig();

            Logger = Log;
            harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Database = new Database();
            Database.BanishedWords.Add(BannedWordOne.Value);
            Database.BanishedWords.Add(BannedWordTwo.Value);
            Database.BanishedWords.Add(BannedWordThree.Value);
            Database.BanishedWords.Add(BannedWordFour.Value);
            Database.BanishedWords.Add(BannedWordFive.Value);
            Database.BanishedWords.Add(BannedWordSix.Value);
            Database.BanishedWords.Add(BannedWordSeven.Value);
            Database.BanishedWords.Add(BannedWordEight.Value);
            Database.BanishedWords.Add(BannedWordNine.Value);
            Database.BanishedWords.Add(BannedWordTen.Value);
            Log.LogWarning("Loaded database of banished words.");
            Log.LogInfo($"Plugin {BuildConfig.Name}-v{BuildConfig.Version} is loaded!");
        }

        public override bool Unload()
        {
            Config.Clear();
            harmony.UnpatchSelf();
            return true;
        }

        public void OnGameInitialized()
        {
            if (isInitialized) return;

            isInitialized = true;
        }

        private static World _serverWorld;
        public static World Server
        {
            get
            {
                if (_serverWorld != null) return _serverWorld;

                _serverWorld = GetWorld("Server")
                    ?? throw new System.Exception("There is no Server world (yet). Did you install a server mod on the client?");
                return _serverWorld;
            }
        }

        public static bool IsServer => Application.productName == "VRisingServer";
        private static World GetWorld(string name)
        {
            foreach (var world in World.s_AllWorlds)
            {
                if (world.Name == name)
                {
                    return world;
                }
            }
            return null;
        }
    }
}
