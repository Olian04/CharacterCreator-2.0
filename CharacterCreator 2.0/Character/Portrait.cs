using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace CharacterCreator_2._0
{
    class Portrait
    {
        public Rectangle dimentions;

        public Dictionary<string, List<Texture2D>> parts;
        public Dictionary<string, int> partsIndex;

        public Portrait(ContentManager Content, Rectangle Dimentions)
        {
            dimentions = Dimentions;
            parts = new Dictionary<string, List<Texture2D>>();
        }

        public void Update(Dictionary<string, int> PartsIndex)
        {
            partsIndex = PartsIndex;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (parts.Count != 0)
            {
                partsIndex["clothes_back"] = partsIndex["clothes_front"];
                partsIndex["hair_back"] = partsIndex["hair_front"];

                spriteBatch.Draw(parts["clothes_back"][partsIndex["clothes_back"]], dimentions, Color.White);
                spriteBatch.Draw(parts["hair_back"][partsIndex["hair_back"]], dimentions, Color.White);
                spriteBatch.Draw(parts["body"][partsIndex["body"]], dimentions, Color.White);
                spriteBatch.Draw(parts["eyes"][partsIndex["eyes"]], dimentions, Color.White);
                spriteBatch.Draw(parts["mouth"][partsIndex["mouth"]], dimentions, Color.White);
                spriteBatch.Draw(parts["nose"][partsIndex["nose"]], dimentions, Color.White);
                spriteBatch.Draw(parts["clothes_front"][partsIndex["clothes_front"]], dimentions, Color.White);
                spriteBatch.Draw(parts["hair_front"][partsIndex["hair_front"]], dimentions, Color.White);
            }
        }

    }
}
