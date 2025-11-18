using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DoodleBosses
{
    public class Textures
    {

        private readonly Dictionary<DoodleBosses.Bosses, Sprite> _dict;

        public Textures()
        {

            Assembly asm = Assembly.GetExecutingAssembly();
            _dict = new Dictionary<DoodleBosses.Bosses, Sprite>();
            /*var tmpTextures = new Dictionary<DoodleBosses.Bosses, string>
            {
                { DoodleBosses.Bosses.GRUZ_MOTHER, "DoodleBosses.Resources.Sheet_GruzMother.png" },
                { DoodleBosses.Bosses.MANTIS_LORDS, "DoodleBosses.Resources.Sheet_MantisLord.png" },
                { DoodleBosses.Bosses.SOUL_WARRIOR, "DoodleBosses.Resources.Sheet_SoulWarrior.png" },
            };*/

            foreach (DoodleBosses.Bosses boss in DoodleBosses.BossStrings.Keys)//(var pair in tmpTextures)
            {
                using (Stream s = asm.GetManifestResourceStream("DoodleBosses.Resources.Sheet_" + DoodleBosses.BossStrings[boss] + ".png")) 
                {
                    if (s != null)
                    {
                        byte[] buffer = new byte[s.Length];
                        s.Read(buffer, 0, buffer.Length);
                        s.Dispose();

                        //Create texture from bytes
                        var tex = new Texture2D(2, 2);

                        tex.LoadImage(buffer, true);

                        // Create sprite from texture
                        _dict.Add(boss, Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)));
                    }
                }
            }
        }

        public Sprite Get(DoodleBosses.Bosses key)
        {
            return _dict[key];
        }
    }
}
