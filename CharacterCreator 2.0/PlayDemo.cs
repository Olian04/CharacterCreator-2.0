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

using GUI.elements;

namespace CharacterCreator_2._0
{
    class PlayDemo
    {
        public delegate void OnClickEventHandler(Object sender);
        public event OnClickEventHandler OnClick;

        Character player;

        Rectangle eventBox;
        Texture2D empty;

        Rectangle portraitDimentions;
        Rectangle oldPortraitDimentions;

        FontRenderer fontRender;
        Font font;

        public PlayDemo(ContentManager Content, Character Player)
        {
            player = Player;
            player.model.deBugg = false;

            oldPortraitDimentions = player.model.dimentions;
            portraitDimentions = new Rectangle(0, 240, (int)(player.portrait.dimentions.Width * 0.8f), (int)(player.portrait.dimentions.Height * 0.8f));

            empty = Content.Load<Texture2D>("WP.png");
            eventBox = new Rectangle(300, 50, 50, 50);

            font = new Font("GUI-Elements/Font/Calibri", Content);
            fontRender = new FontRenderer();
        }

        public bool transitInProg = false;
        public void Update(GameTime gameTime, Dictionary<string, int> partsIndex)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !transitInProg)
                OnClick(this);

            if (player.model.deBugg)
                player.model.deBugg = false;

            if (player.portrait.dimentions != portraitDimentions)
            { 
                player.portrait.dimentions = portraitDimentions;
                player.model.dimentions.X = 100;
                player.model.dimentions.Y = 100;
            }

            player.Update(gameTime, partsIndex);
            Move();
        }

        int i = 2;
        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.model.dimentions.Y += i;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.model.dimentions.Y -= i;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.model.dimentions.X += i;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.model.dimentions.X -= i;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            player.DrawModel(spriteBatch);
            spriteBatch.Draw(empty, eventBox, Color.Lerp(Color.Yellow, Color.Transparent, 0.2f));
            if (eventBox.Intersects(player.model.dimentions))
            {
                player.portrait.Draw(spriteBatch);
                fontRender.DrawText(spriteBatch, new Vector2(portraitDimentions.Y / 4, 250 + portraitDimentions.Height), player.name.ToString(), font, Color.Black);
            }
            spriteBatch.End();
        }
    }
}
