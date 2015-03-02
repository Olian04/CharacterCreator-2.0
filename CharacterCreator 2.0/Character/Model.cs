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
    class Model
    {
        public Rectangle dimentions;
        private List<Rectangle> sourceRect;
        private int sourceRectPick = 0;

        public Dictionary<string, List<Texture2D>> parts;
        public Dictionary<string, int> partsIndex;
        private Texture2D bodyTexture;

        public bool deBugg = true;

        public Model(ContentManager Content, Rectangle Dimentions)
        {
            dimentions = Dimentions;
            debuggRectangle = dimentions;
            parts = new Dictionary<string, List<Texture2D>>();

            bodyTexture = Content.Load<Texture2D>("Character/Model/Sex - Male/Body/0.png");

            sourceRect = new List<Rectangle>(16);
            for (int Y = 0; Y < 4; Y++)
                for (int X = 0; X < 4; X++)
                    sourceRect.Add(new Rectangle(X * 32, Y * 48, 32, 48));
        }

        #region Update
        private float timePerSprite = 1.7f;
        private float time = 0;
        private int increase = 0;
        private Rectangle debuggRectangle;
        public void Update(GameTime gameTime, Dictionary<string, int> PartsIndex)
        {
            partsIndex = PartsIndex;

            if (deBugg && time >= timePerSprite)
            {
                if (dimentions != debuggRectangle)
                    dimentions = debuggRectangle;

                if (sourceRectPick >= 3)
                    sourceRectPick = 0;
                else
                    sourceRectPick++;
                time = 0;
            }
            else if (!deBugg && time >= timePerSprite)
            {
                #region controle
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    int spriteStart = 0;
                    if (time > timePerSprite)
                    {
                        sourceRectPick = spriteStart + increase;
                        time = 0;
                        if (increase >= 3)
                            increase = 0;
                        increase++;
                    }
                }
                else if (sourceRectPick >= 1 && sourceRectPick <= 3)
                { 
                    sourceRectPick = 0;
                    increase = 0;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    int spriteStart = 4;
                    if (time > timePerSprite)
                    {
                        sourceRectPick = spriteStart + increase;
                        time = 0;
                        if (increase >= 3)
                            increase = 0;
                        increase++;
                    }
                }
                else if (sourceRectPick >= 5 && sourceRectPick <= 7)
                { 
                    sourceRectPick = 4;
                    increase = 0;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    int spriteStart = 8;
                    if (time > timePerSprite)
                    {
                        sourceRectPick = spriteStart + increase;
                        time = 0;
                        if (increase >= 3)
                            increase = 0;
                        increase++;
                    }
                }
                else if (sourceRectPick >= 9 && sourceRectPick <= 11)
                {
                    sourceRectPick = 8;
                    increase = 0;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    int spriteStart = 12;
                    if (time > timePerSprite)
                    {
                        sourceRectPick = spriteStart + increase;
                        time = 0;
                        if (increase >= 3)
                            increase = 0;
                        increase++;
                    }
                }
                else if (sourceRectPick >= 13 && sourceRectPick <= 15)
                {
                    sourceRectPick = 12;
                    increase = 0;
                }
                #endregion
            }
            time += 0.01f * gameTime.ElapsedGameTime.Milliseconds;
        }
        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawChar(spriteBatch, dimentions, 0);

            #region ifDebugg
            if (deBugg)
            {
                DrawChar(spriteBatch, new Rectangle(dimentions.X + dimentions.Width, dimentions.Y, dimentions.Width, dimentions.Height), 4);
                DrawChar(spriteBatch, new Rectangle(dimentions.X + dimentions.Width * 2, dimentions.Y, dimentions.Width, dimentions.Height), 8);
                DrawChar(spriteBatch, new Rectangle(dimentions.X + dimentions.Width * 3, dimentions.Y, dimentions.Width, dimentions.Height), 12);
            }
            #endregion
        }

        private void DrawChar(SpriteBatch spriteBatch, Rectangle Dimentions, int sourcePlus)
        {
            spriteBatch.Draw(bodyTexture, Dimentions, sourceRect[sourceRectPick + sourcePlus], Color.White);
            spriteBatch.Draw(parts["clothes"][partsIndex["clothes_front"]], Dimentions, sourceRect[sourceRectPick + sourcePlus], Color.White);
            spriteBatch.Draw(parts["eyes"][partsIndex["eyes"]], Dimentions, sourceRect[sourceRectPick + sourcePlus], Color.White);
            spriteBatch.Draw(parts["hair"][partsIndex["hair_front"]], Dimentions, sourceRect[sourceRectPick + sourcePlus], Color.White);
        }

    }
}
