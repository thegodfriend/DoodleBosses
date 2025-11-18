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

            [BossStrings[Bosses.SOUL_WARRIOR]] = ("GG_Mage_Knight", "Mage Knight"),
            [BossStrings[Bosses.MANTIS_LORDS]] = ("Fungus2_15_boss", "Mantis Battle/Battle Main/Mantis Lord"),
            [BossStrings[Bosses.GRUZ_MOTHER]] = ("GG_Gruz_Mother", "_Enemies/Giant Fly"),
            
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
            
            MANTIS_LORDS,

            SOUL_WARRIOR
        }

        internal static readonly Dictionary<Bosses, string> BossStrings = new Dictionary<Bosses, string>()
        {
            { Bosses.GRUZ_MOTHER, "GruzMother" },
            { Bosses.MANTIS_LORDS, "MantisLord" },
            { Bosses.SOUL_WARRIOR, "SoulWarrior" },
        };


    }
}