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
            [BossStrings[Bosses.VENGEFLY_KING]] = ("", ""),
            [BossStrings[Bosses.FALSE_KNIGHT]] = ("", ""),
            [BossStrings[Bosses.HORNET]] = ("", ""),
            [BossStrings[Bosses.MASSIVE_MOSS_CHARGER]] = ("", ""),
            [BossStrings[Bosses.FLUKEMARM]] = ("", ""),
            [BossStrings[Bosses.MANTIS_LORDS]] = ("Fungus2_15_boss", "Mantis Battle/Battle Main/Mantis Lord"),
            [BossStrings[Bosses.OBLOBBLES]] = ("", ""),
            [BossStrings[Bosses.HIVE_KNIGHT]] = ("", ""),
            [BossStrings[Bosses.BROKEN_VESSEL]] = ("", ""),
            [BossStrings[Bosses.NOSK]] = ("", ""),
            [BossStrings[Bosses.WINGED_NOSK]] = ("", ""),
            [BossStrings[Bosses.COLLECTOR]] = ("", ""),
            [BossStrings[Bosses.CRYSTAL_GUARDIAN]] = ("", ""),
            [BossStrings[Bosses.UUMUU]] = ("", ""),
            [BossStrings[Bosses.TRAITOR_LORD]] = ("", ""),
            [BossStrings[Bosses.GREY_PRINCE_ZOTE]] = ("", ""),
            [BossStrings[Bosses.SOUL_WARRIOR]] = ("GG_Mage_Knight", "Mage Knight"),
            [BossStrings[Bosses.SOUL_MASTER]] = ("", ""),
            [BossStrings[Bosses.DUNG_DEFENDER]] = ("", ""),
            [BossStrings[Bosses.WHITE_DEFENDER]] = ("", ""),
            [BossStrings[Bosses.WATCHER_KNIGHT]] = ("", ""),
            [BossStrings[Bosses.NO_EYES]] = ("", ""),
            [BossStrings[Bosses.MARMU]] = ("", ""),
            [BossStrings[Bosses.GALIEN]] = ("", ""),
            [BossStrings[Bosses.MARKOTH]] = ("", ""),
            [BossStrings[Bosses.XERO]] = ("", ""),
            [BossStrings[Bosses.GORB]] = ("", ""),
            [BossStrings[Bosses.ELDER_HU]] = ("", ""),
            [BossStrings[Bosses.ORO_MATO]] = ("", ""),
            [BossStrings[Bosses.PAINTMASTER_SHEO]] = ("", ""),
            [BossStrings[Bosses.GREAT_NAILSAGE_SLY]] = ("", ""),
            [BossStrings[Bosses.PURE_VESSEL]] = ("", ""),
            [BossStrings[Bosses.GRIMM]] = ("", ""),
            [BossStrings[Bosses.NIGHTMARE_KING]] = ("", ""),
            [BossStrings[Bosses.ABSOLUTE_RADIANCE]] = ("", ""),
            [BossStrings[Bosses.HOLLOW_KNIGHT]] = ("", ""),
            [BossStrings[Bosses.RADIANCE]] = ("", ""),
            [BossStrings[Bosses.ZOTE]] = ("", ""),

        };

        public Textures SpriteDict { get; private set; }

        public static Sprite GetSprite(Bosses boss) => Instance.SpriteDict.Get(boss);

        public override string GetVersion() => "0.0.1-0";

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
            FALSE_KNIGHT,
            HORNET,
            MASSIVE_MOSS_CHARGER,
            FLUKEMARM,
            MANTIS_LORDS,
            OBLOBBLES,
            HIVE_KNIGHT,
            BROKEN_VESSEL,
            NOSK,
            WINGED_NOSK,
            COLLECTOR,
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
            GREAT_NAILSAGE_SLY,
            PURE_VESSEL,
            GRIMM,
            NIGHTMARE_KING,
            ABSOLUTE_RADIANCE,
            HOLLOW_KNIGHT,
            RADIANCE,
            ZOTE
        }

        internal static readonly Dictionary<Bosses, string> BossStrings = new Dictionary<Bosses, string>()
        {
            { Bosses.GRUZ_MOTHER, "GruzMother" },
            { Bosses.VENGEFLY_KING, "" },
            { Bosses.FALSE_KNIGHT, "" },
            { Bosses.HORNET, "" },
            { Bosses.MASSIVE_MOSS_CHARGER, "" },
            { Bosses.FLUKEMARM, "" },
            { Bosses.MANTIS_LORDS, "MantisLord" },
            { Bosses.OBLOBBLES, "" },
            { Bosses.HIVE_KNIGHT, "" },
            { Bosses.BROKEN_VESSEL, "" },
            { Bosses.NOSK, "" },
            { Bosses.WINGED_NOSK, "" },
            { Bosses.COLLECTOR, "" },
            { Bosses.CRYSTAL_GUARDIAN, "" },
            { Bosses.UUMUU, "" },
            { Bosses.TRAITOR_LORD, "" },
            { Bosses.GREY_PRINCE_ZOTE, "" },
            { Bosses.SOUL_WARRIOR, "SoulWarrior" },
            { Bosses.SOUL_MASTER, "" },
            { Bosses.DUNG_DEFENDER, "" },
            { Bosses.WHITE_DEFENDER, "" },
            { Bosses.WATCHER_KNIGHT, "" },
            { Bosses.NO_EYES, "" },
            { Bosses.MARMU, "" },
            { Bosses.GALIEN, "" },
            { Bosses.MARKOTH, "" },
            { Bosses.XERO, "" },
            { Bosses.GORB, "" },
            { Bosses.ELDER_HU, "" },
            { Bosses.ORO_MATO, "" },
            { Bosses.PAINTMASTER_SHEO, "" },
            { Bosses.GREAT_NAILSAGE_SLY, "" },
            { Bosses.PURE_VESSEL, "" },
            { Bosses.GRIMM, "" },
            { Bosses.NIGHTMARE_KING, "" },
            { Bosses.ABSOLUTE_RADIANCE, "" },
            { Bosses.HOLLOW_KNIGHT, "" },
            { Bosses.RADIANCE, "" },
            { Bosses.ZOTE, "" },
        };


    }
}