using Modding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace DoodleBosses
{
    public class DoodleBosses : Mod
    {
        internal static DoodleBosses Instance;

        public static readonly Dictionary<string, GameObject> _gameObjects = new();

        private Dictionary<string, ValueTuple<string, string>> _preloads = new()
        {

            [BossStrings[Bosses.GRUZ_MOTHER]] = ("GG_Gruz_Mother", "_Enemies/Giant Fly"),
            [BossStrings[Bosses.VENGEFLY_KING]] = ("GG_Vengefly", "Giant Buzzer Col"),
            [BossStrings[Bosses.BROODING_MAWLEK]] = ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
            [BossStrings[Bosses.FALSE_KNIGHT]] = ("GG_False_Knight", "Battle Scene/False Knight New"),
            //[BossStrings[Bosses.HORNET]] = ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
            [BossStrings[Bosses.MASSIVE_MOSS_CHARGER]] = ("GG_Mega_Moss_Charger", "Mega Moss Charger"),
            [BossStrings[Bosses.FLUKEMARM]] = ("GG_Flukemarm", "Fluke Mother"),
            [BossStrings[Bosses.MANTIS_LORDS]] = ("Fungus2_15_boss", "Mantis Battle/Battle Main/Mantis Lord"),
            [BossStrings[Bosses.OBLOBBLES]] = ("GG_Oblobbles", "Mega Fat Bee"),
            [BossStrings[Bosses.HIVE_KNIGHT]] = ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
            [BossStrings[Bosses.BROKEN_VESSEL]] = ("GG_Broken_Vessel", "Infected Knight"),
            [BossStrings[Bosses.NOSK]] = ("GG_Nosk", "Mimic Spider"),
            [BossStrings[Bosses.WINGED_NOSK]] = ("GG_Nosk_Hornet", "Battle Scene/Hornet Nosk"),
            [BossStrings[Bosses.COLLECTOR]] = ("GG_Collector", "Battle Scene/Jar Collector"),
            [BossStrings[Bosses.GOD_TAMER]] = ("GG_God_Tamer", "Entry Object/Lancer"),
            [BossStrings[Bosses.CRYSTAL_GUARDIAN]] = ("GG_Crystal_Guardian", "Mega Zombie Beam Miner (1)"),
            [BossStrings[Bosses.UUMUU]] = ("GG_Uumuu", "Mega Jellyfish GG"),
            [BossStrings[Bosses.TRAITOR_LORD]] = ("GG_Traitor_Lord", "Battle Scene/Wave 3/Mantis Traitor Lord"),
            [BossStrings[Bosses.GREY_PRINCE_ZOTE]] = ("GG_Grey_Prince_Zote", "Grey Prince"),
            [BossStrings[Bosses.SOUL_WARRIOR]] = ("GG_Mage_Knight", "Mage Knight"),
            [BossStrings[Bosses.SOUL_MASTER]] = ("GG_Soul_Master", "Mage Lord"),
            [BossStrings[Bosses.DUNG_DEFENDER]] = ("GG_Dung_Defender", "Dung Defender"),
            [BossStrings[Bosses.WHITE_DEFENDER]] = ("GG_White_Defender", "White Defender"),
            [BossStrings[Bosses.WATCHER_KNIGHT]] = ("GG_Watcher_Knights", "Battle Control/Black Knight 1"),
            [BossStrings[Bosses.NO_EYES]] = ("GG_Ghost_No_Eyes", "Warrior/Ghost Warrior No Eyes"),
            [BossStrings[Bosses.MARMU]] = ("GG_Ghost_Marmu", "Warrior/Ghost Warrior Marmu"),
            [BossStrings[Bosses.GALIEN]] = ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
            [BossStrings[Bosses.MARKOTH]] = ("GG_Ghost_Markoth", "Warrior/Ghost Warrior Markoth"),
            [BossStrings[Bosses.XERO]] = ("GG_Ghost_Xero", "Warrior/Ghost Warrior Xero"),
            [BossStrings[Bosses.GORB]] = ("GG_Ghost_Gorb", "Warrior/Ghost Warrior Slug"),
            [BossStrings[Bosses.ELDER_HU]] = ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
            [BossStrings[Bosses.ORO_MATO]] = ("GG_Nailmasters", "Brothers/Oro"),
            [BossStrings[Bosses.PAINTMASTER_SHEO]] = ("GG_Painter", "Battle Scene/Sheo Boss"),
            //[BossStrings[Bosses.GREAT_NAILSAGE_SLY]] = ("GG_Sly", "Battle Scene/Sly Boss"),
            [BossStrings[Bosses.PURE_VESSEL]] = ("GG_Hollow_Knight", "Battle Scene/HK Prime"),
            [SpecialStrings[Specials.GRIMM1].Item1] = ("GG_Grimm", "Grimm Scene/Grimm Boss"),
            [BossStrings[Bosses.NIGHTMARE_KING]] = ("GG_Grimm_Nightmare", "Grimm Control/Nightmare Grimm Boss"),
            //[BossStrings[Bosses.HOLLOW_KNIGHT]] = ("", ""),
            [SpecialStrings[Specials.RADIANCE1].Item1] = ("GG_Radiance", "Boss Control/Radiance Roar"),//"Boss Control/Absolute Radiance"),
            //[BossStrings[Bosses.ZOTE]] = ("", ""),

        };

        public Textures SpriteDict { get; private set; }

        public static Sprite GetSprite(int key, bool special = false) => Instance.SpriteDict.Get(key, special);

        public override string GetVersion() => "0.9.1-0";

        public override List<ValueTuple<string, string>> GetPreloadNames()
        {
            return _preloads.Values.ToList();
        }

        public DoodleBosses() : base("Doodle Boss project")
        {
            Instance = this;

            SpriteDict = new Textures();
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            foreach (var (name, (scene, path)) in _preloads)
            {
                _gameObjects[name] = preloadedObjects[scene][path];
            }

            Instance = this;

            foreach (Bosses boss in BossStrings.Keys)
            {
                _gameObjects[BossStrings[boss]].GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = GetSprite((int)boss).texture;

            }

            Log("Finished loading regular bosses");

            foreach (Specials sheet in SpecialStrings.Keys)
            {
                Log("Doing special boss " + SpecialStrings[sheet].Item1);
                _gameObjects[SpecialStrings[sheet].Item1].GetComponent<tk2dSprite>().Collection.materials[SpecialStrings[sheet].Item2].mainTexture = GetSprite((int)sheet, true).texture;
            }


            //_gameObjects[BossStrings[Bosses.GRUZ_MOTHER]].GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = GetSprite(Bosses.GRUZ_MOTHER).texture;

            ModHooks.LanguageGetHook += LanguageGet;

            Log("Initialized");
        }

        private static string LanguageGet(string key, string sheetTitle, string orig)
        {
            if (true)
                return orig;

            switch (key)
            {
                case "BIGFLY_SUPER":
                    return "";
                case "BIGFLY_MAIN":
                    return "";
                case "BIGFLY_SUB":
                    return "";
                case "VENGEFLY_SUPER":
                    return "";
                case "VENGEFLY_MAIN":
                    return "";
                case "VENGEFLY_SUB":
                    return "";
                case "FALSE_KNIGHT_SUPER":
                    return "";
                case "FALSE_KNIGHT_MAIN":
                    return "";
                case "FALSE_KNIGHT_SUB":
                    return "";
                case "FALSE_KNIGHT_DREAM_SUPER":
                    return "";
                case "FALSE_KNIGHT_DREAM_MAIN":
                    return "";
                case "FALSE_KNIGHT_DREAM_SUB":
                    return "";
                case "HORNET_SUPER":
                    return "";
                case "HORNET_MAIN":
                    return "";
                case "HORNET_SUB":
                    return "";
                case "MEGA_MOSS_SUPER":
                    return "";
                case "MEGA_MOSS_MAIN":
                    return "";
                case "MEGA_MOSS_SUB":
                    return "";
                case "FLUKE_MOTHER_SUPER":
                    return "";
                case "FLUKE_MOTHER_MAIN":
                    return "";
                case "FLUKE_MOTHER_SUB":
                    return "";
                case "MANTIS_LORDS_SUPER":
                    return "";
                case "MANTIS_LORDS_MAIN":
                    return "";
                case "MANTIS_LORDS_SUB":
                    return "";
                case "SISTERS_SUPER":
                    return "";
                case "SISTERS_MAIN":
                    return "";
                case "SISTERS_SUB":
                    return "";
                case "OBLOBBLES_SUPER":
                    return "";
                case "OBLOBBLES_MAIN":
                    return "";
                case "OBLOBBLES_SUB":
                    return "";
                case "HIVE_KNIGHT_SUPER":
                    return "";
                case "HIVE_KNIGHT_MAIN":
                    return "";
                case "HIVE_KNIGHT_SUB":
                    return "";
                case "INFECTED_KNIGHT_SUPER":
                    return "";
                case "INFECTED_KNIGHT_MAIN":
                    return "";
                case "INFECTED_KNIGHT_SUB":
                    return "";
                case "INFECTED_KNIGHT_DREAM_SUPER":
                    return "";
                case "INFECTED_KNIGHT_DREAM_MAIN":
                    return "";
                case "INFECTED_KNIGHT_DREAM_SUB":
                    return "";
                case "MIMIC_SPIDER_SUPER":
                    return "";
                case "MIMIC_SPIDER_MAIN":
                    return "";
                case "MIMIC_SPIDER_SUB":
                    return "";
                case "COLLECTOR_SUPER":
                    return "";
                case "COLLECTOR_MAIN":
                    return "";
                case "COLLECTOR_SUB":
                    return "";
                case "LOBSTER_LANCER_NC_SUPER":
                case "LOBSTER_LANCER_C_SUPER":
                    return "";
                case "LOBSTER_LANCER_NC_MAIN":
                case "LOBSTER_LANCER_C_MAIN":
                    return "";
                case "LOBSTER_LANCER_NC_SUB":
                case "LOBSTER_LANCER_C_SUB":
                    return "";
                case "CRYSTAL_GUARDIAN_SUPER":
                    return "";
                case "CRYSTAL_GUARDIAN_MAIN":
                    return "";
                case "CRYSTAL_GUARDIAN_SUB":
                    return "";
                case "ENRAGED_GUARDIAN_SUPER":
                    return "";
                case "ENRAGED_GUARDIAN_MAIN":
                    return "";
                case "ENRAGED_GUARDIAN_SUB":
                    return "";
                case "MEGA_JELLY_SUPER":
                    return "";
                case "MEGA_JELLY_MAIN":
                    return "";
                case "MEGA_JELLY_SUB":
                    return "";
                case "TRAITOR_LORD_SUPER":
                    return "";
                case "TRAITOR_LORD_MAIN":
                    return "";
                case "TRAITOR_LORD_SUB":
                    return "";
                // GPZ
                case "MAGE_KNIGHT_SUPER":
                    return "";
                case "MAGE_KNIGHT_MAIN":
                    return "";
                case "MAGE_KNIGHT_SUB":
                    return "";
                case "MAGE_LORD_SUPER":
                    return "";
                case "MAGE_LORD_MAIN":
                    return "";
                case "MAGE_LORD_SUB":
                    return "";
                case "MAGE_LORD_DREAM_SUPER":
                    return "";
                case "MAGE_LORD_DREAM_MAIN":
                    return "";
                case "MAGE_LORD_DREAM_SUB":
                    return "";
                case "DUNG_DEFENDER_SUPER":
                    return "";
                case "DUNG_DEFENDER_MAIN":
                    return "";
                case "DUNG_DEFENDER_SUB":
                    return "";
                case "WHITE_DEFENDER_SUPER":
                    return "";
                case "WHITE_DEFENDER_MAIN":
                    return "";
                case "WHITE_DEFENDER_SUB":
                    return "";
                case "BLACK_KNIGHT_SUPER":
                    return "";
                case "BLACK_KNIGHT_MAIN":
                    return "";
                case "BLACK_KNIGHT_SUB":
                    return "";
                case "GH_NOEYES_NC_SUPER":
                case "GH_NOEYES_C_SUPER":
                    return "";
                case "GH_NOEYES_NC_MAIN":
                case "GH_NOEYES_C_MAIN":
                    return "";
                case "GH_NOEYES_NC_SUB":
                case "GH_NOEYES_C_SUB":
                    return "";
                case "GH_MUMCAT_NC_SUPER":
                case "GH_MUMCAT_C_SUPER":
                    return "";
                case "GH_MUMCAT_NC_MAIN":
                case "GH_MUMCAT_C_MAIN":
                    return "";
                case "GH_MUMCAT_NC_SUB":
                case "GH_MUMCAT_C_SUB":
                    return "";
                case "GH_GALIEN_NC_SUPER":
                case "GH_GALIEN_C_SUPER":
                    return "";
                case "GH_GALIEN_NC_MAIN":
                case "GH_GALIEN_C_MAIN":
                    return "";
                case "GH_GALIEN_NC_SUB":
                case "GH_GALIEN_C_SUB":
                    return "";
                case "GH_MARKOTH_NC_SUPER":
                case "GH_MARKOTH_C_SUPER":
                    return "";
                case "GH_MARKOTH_NC_MAIN":
                case "GH_MARKOTH_C_MAIN":
                    return "";
                case "GH_MARKOTH_NC_SUB":
                case "GH_MARKOTH_C_SUB":
                    return "";
                case "GH_XERO_NC_SUPER":
                case "GH_XERO_C_SUPER":
                    return "";
                case "GH_XERO_NC_MAIN":
                case "GH_XERO_C_MAIN":
                    return "";
                case "GH_XERO_NC_SUB":
                case "GH_XERO_C_SUB":
                    return "";
                case "GH_ALADAR_NC_SUPER":
                case "GH_ALADAR_C_SUPER":
                    return "";
                case "GH_ALADAR_NC_MAIN":
                case "GH_ALADAR_C_MAIN":
                    return "";
                case "GH_ALADAR_NC_SUB":
                case "GH_ALADAR_C_SUB":
                    return "";
                case "GH_HU_NC_SUPER":
                case "GH_HU_C_SUPER":
                    return "";
                case "GH_HU_NC_MAIN":
                case "GH_HU_C_MAIN":
                    return "";
                case "GH_HU_NC_SUB":
                case "GH_HU_C_SUB":
                    return "";
                // Oro/Mato
                case "PAINTMASTER_SUPER":
                    return "";
                case "PAINTMASTER_MAIN":
                    return "";
                case "PAINTMASTER_SUB":
                    return "";
                case "SLY_BOSS_SUPER":
                    return "";
                case "SLY_BOSS_MAIN":
                    return "";
                case "SLY_BOSS_SUB":
                    return "";
                // PV
                case "GRIMM_SUPER":
                    return "";
                case "GRIMM_MAIN":
                    return "";
                case "GRIMM_SUB":
                    return "";
                case "NIGHTMARE_GRIMM_SUPER":
                    return "";
                case "NIGHTMARE_GRIMM_MAIN":
                    return "";
                case "NIGHTMARE_GRIMM_SUB":
                    return "";
                // AbsRad
                case "ZOTE_SUPER":
                    return "";
                case "ZOTE_MAIN":
                    return "";
                case "ZOTE_SUB":
                    return "";
                // Rad
                // THK
            }


            return key;//orig;//
        }

        public enum Bosses
        {
            GRUZ_MOTHER,
            VENGEFLY_KING,
            BROODING_MAWLEK,
            FALSE_KNIGHT,
            //HORNET, // No sheet
            MASSIVE_MOSS_CHARGER,
            FLUKEMARM,
            MANTIS_LORDS,
            OBLOBBLES,
            HIVE_KNIGHT,
            BROKEN_VESSEL,
            NOSK,
            WINGED_NOSK,
            COLLECTOR,
            GOD_TAMER,
            CRYSTAL_GUARDIAN,
            UUMUU,
            TRAITOR_LORD,
            GREY_PRINCE_ZOTE,
            SOUL_WARRIOR,
            SOUL_MASTER,
            DUNG_DEFENDER,
            WHITE_DEFENDER,
            WATCHER_KNIGHT,
            NO_EYES,
            MARMU,
            GALIEN,
            MARKOTH,
            XERO,
            GORB,
            ELDER_HU,
            ORO_MATO,
            PAINTMASTER_SHEO,
            //GREAT_NAILSAGE_SLY, // No Sheet
            PURE_VESSEL,
            //GRIMM, // Exceptional
            NIGHTMARE_KING,
            //HOLLOW_KNIGHT, // No Sheet
            //RADIANCE, // Exceptional
            //ZOTE
        }
        public enum Specials
        {
            GRIMM1,
            GRIMM2,
            RADIANCE1,
            RADIANCE2,
        }

        internal static readonly Dictionary<Bosses, string> BossStrings = new Dictionary<Bosses, string>()
        {
            { Bosses.GRUZ_MOTHER, "GruzMother" },
            { Bosses.VENGEFLY_KING, "VengeflyKing" },
            { Bosses.BROODING_MAWLEK, "BroodingMawlek" },
            { Bosses.FALSE_KNIGHT, "FalseKnight" },
            //{ Bosses.HORNET, "Hornet" },
            { Bosses.MASSIVE_MOSS_CHARGER, "MassiveMossCharger" },
            { Bosses.FLUKEMARM, "Flukemarm" },
            { Bosses.MANTIS_LORDS, "MantisLord" },
            { Bosses.OBLOBBLES, "Oblobble" },
            { Bosses.HIVE_KNIGHT, "HiveKnight" },
            { Bosses.BROKEN_VESSEL, "BrokenVessel" },
            { Bosses.NOSK, "Nosk" },
            { Bosses.WINGED_NOSK, "WingedNosk" },
            { Bosses.COLLECTOR, "Collector" },
            { Bosses.GOD_TAMER, "GodTamer" },
            { Bosses.CRYSTAL_GUARDIAN, "CrystalGuardian" },
            { Bosses.UUMUU, "Uumuu" },
            { Bosses.TRAITOR_LORD, "TraitorLord" },
            { Bosses.GREY_PRINCE_ZOTE, "GreyPrinceZote" },
            { Bosses.SOUL_WARRIOR, "SoulWarrior" },
            { Bosses.SOUL_MASTER, "SoulMaster" },
            { Bosses.DUNG_DEFENDER, "DungDefender" },
            { Bosses.WHITE_DEFENDER, "WhiteDefender" },
            { Bosses.WATCHER_KNIGHT, "WatcherKnight" },
            { Bosses.NO_EYES, "NoEyes" },
            { Bosses.MARMU, "Marmu" },
            { Bosses.GALIEN, "Galien" },
            { Bosses.MARKOTH, "Markoth" },
            { Bosses.XERO, "Xero" },
            { Bosses.GORB, "Gorb" },
            { Bosses.ELDER_HU, "ElderHu" },
            { Bosses.ORO_MATO, "OroMato" },
            { Bosses.PAINTMASTER_SHEO, "PaintmasterSheo" },
            //{ Bosses.GREAT_NAILSAGE_SLY, "GreatNailsageSly" },
            { Bosses.PURE_VESSEL, "PureVessel" },
            //{ Bosses.GRIMM, "Grimm" },
            { Bosses.NIGHTMARE_KING, "NightmareKing" },
            //{ Bosses.HOLLOW_KNIGHT, "TheHollowKnight" },
            //{ Bosses.RADIANCE, "Radiance" },
            //{ Bosses.ZOTE, "Zote" },
        };

        internal static readonly Dictionary<Specials, ValueTuple<string, int>> SpecialStrings = new Dictionary<Specials, ValueTuple<string, int>>()
        {
            { Specials.GRIMM1, ("Grimm", 0) },
            { Specials.GRIMM2, ("Grimm", 1) },
            { Specials.RADIANCE1, ("Radiance", 0) },
            { Specials.RADIANCE2, ("Radiance", 1) },
        };
    }
}