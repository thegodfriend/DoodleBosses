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
            //[BossStrings[Bosses.GRIMM]] = ("GG_Grimm", "Grimm Scene/Grimm Boss"),
            [BossStrings[Bosses.NIGHTMARE_KING]] = ("GG_Grimm_Nightmare", "Grimm Control/Nightmare Grimm Boss"),
            //[BossStrings[Bosses.HOLLOW_KNIGHT]] = ("", ""),
            //[BossStrings[Bosses.RADIANCE]] = ("GG_Radiance", "Boss Control/Absolute Radiance"),
            //[BossStrings[Bosses.ZOTE]] = ("", ""),

        };

        public Textures SpriteDict { get; private set; }

        public static Sprite GetSprite(Bosses boss) => Instance.SpriteDict.Get(boss);

        public override string GetVersion() => "0.9.0-0";

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
                _gameObjects[BossStrings[boss]].GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = GetSprite(boss).texture;

            }
            //_gameObjects[BossStrings[Bosses.GRUZ_MOTHER]].GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = GetSprite(Bosses.GRUZ_MOTHER).texture;

            Log("Initialized");
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


    }
}