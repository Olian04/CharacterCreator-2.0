using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace CharacterCreator_2._0
{
    class Character
    {
        public Portrait portrait;
        public Model model;

        public string name = "Name?";

        public Character(ContentManager Content, Rectangle PortraitDimentions, Rectangle ModelDimentions)
        {
            portrait = new Portrait(Content, PortraitDimentions);
            model = new Model(Content, ModelDimentions);
        }

        StreamWriter sw;
        public void Save()
        {
            using (sw = new StreamWriter("save.txt"))
            { 
                sw.WriteLine(name);
                sw.WriteLine(portrait.partsIndex["body"]);
                sw.WriteLine(portrait.partsIndex["clothes_front"]);
                sw.WriteLine(portrait.partsIndex["eyes"]);
                sw.WriteLine(portrait.partsIndex["hair_front"]);
                sw.WriteLine(portrait.partsIndex["mouth"]);
                sw.WriteLine(portrait.partsIndex["nose"]);
                sw.Close();
            }
        }

        StreamReader sr;
        public void Load()
        {
            
            using (sr = new StreamReader("save.txt"))
            {
                name = sr.ReadLine();
                try
                {
                    portrait.partsIndex["body"] = int.Parse(sr.ReadLine());
                    portrait.partsIndex["clothes_front"] = int.Parse(sr.ReadLine());
                    portrait.partsIndex["eyes"] = int.Parse(sr.ReadLine());
                    portrait.partsIndex["hair_front"] = int.Parse(sr.ReadLine());
                    portrait.partsIndex["mouth"] = int.Parse(sr.ReadLine());
                    portrait.partsIndex["nose"] = int.Parse(sr.ReadLine());
                }
                catch
                { 
                }
                sr.Close();
            }
        }

        public void Update(GameTime gameTime, Dictionary<string, int> partsIndex)
        {
            model.Update(gameTime, partsIndex);
            portrait.Update(partsIndex); 
        }

        public void DrawModel(SpriteBatch spriteBatch)
        {
            model.Draw(spriteBatch);
        }

        public void DrawPortrait(SpriteBatch spriteBatch)
        { 
            portrait.Draw(spriteBatch);
        }
    }
}
