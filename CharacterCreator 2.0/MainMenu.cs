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
    class MainMenu
    {
        public delegate void OnClickEventHandler(Object sender);
        public event OnClickEventHandler OnClick;

        Character player;

        Button newGame;
        Button resumeGame;
        Button exit;

        public MainMenu(ContentManager Content, Character Player, float windowWidth, float windowHeight)
        {
            player = Player;

            newGame = new Button(Content, new Rectangle((int)windowWidth / 2 - 100, (int)windowHeight / 4, 200, 60), "Font/Calibri");
            newGame.isToggle = false;
            newGame.text = "Creator";
            newGame.OnClick += newGame_OnClick;

            resumeGame = new Button(Content, new Rectangle((int)windowWidth / 2 - 100, (int)windowHeight / 4 + 80, 200, 60), "Font/Calibri");
            resumeGame.isToggle = false;
            resumeGame.text = "Play";
            resumeGame.OnClick += resumeGame_OnClick;

            exit = new Button(Content, new Rectangle((int)windowWidth / 2 - 100, (int)windowHeight / 4 + 160, 200, 60), "Font/Calibri");
            exit.isToggle = false;
            exit.text = "Exit";
            exit.OnClick += exit_OnClick;
        }

        public bool transitInProg = false;
        public string action = "";
        void resumeGame_OnClick(object sender)
        {
            if (!transitInProg)
            { 
                action = "resumeGame";
                OnClick(this);
            } 
        }
        void exit_OnClick(object sender)
        {
            if (!transitInProg)
            {
                action = "exit";
                OnClick(this);
            }
        }
        void newGame_OnClick(object sender)
        {
            if (!transitInProg)
            {
                action = "newGame";
                OnClick(this);
            }
        }

        public void Update(CustomCursor Cursor)
        {
            newGame.Update(Cursor.hitbox);
            resumeGame.Update(Cursor.hitbox);
            exit.Update(Cursor.hitbox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            newGame.Draw(spriteBatch);
            resumeGame.Draw(spriteBatch);
            exit.Draw(spriteBatch);
        }
    }
}
