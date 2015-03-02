#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using GUI.elements;
#endregion

namespace CharacterCreator_2._0
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        CustomCursor Cursor;
        CharacterCreator characterCreator;
        Character player;
        MainMenu mainMenu;
        PlayDemo playDemo;

        Texture2D blank;
        Texture2D water;
        Texture2D ripples;
        Texture2D beach;
        Rectangle fullScreen;

        public bool doTrasit = false;
        public bool doIntroTransit = true;
        private bool doDraw = true;

        public string activeScreen = null;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Cursor = new CustomCursor(Content, Content.Load<Texture2D>("GUI-Elements/Cursor/Cursor_Pointer_Placeholder.png"), new Vector2(0.15f, 0.2f), Color.White);

            player = new Character(Content, new Rectangle(270, 50, 240, 240), new Rectangle(295, 290, (int)(32 * 1.5f), (int)(48 * 1.5f)));

            characterCreator = new CharacterCreator(Content, player);
            characterCreator.OnClick += characterCreator_OnClick;

            mainMenu = new MainMenu(Content, player, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            mainMenu.OnClick += mainMenu_OnClick;

            playDemo = new PlayDemo(Content, player);
            playDemo.OnClick += playDemo_OnClick;

            blank = Content.Load<Texture2D>("WP.png");
            water = Content.Load<Texture2D>("water.jpg");
            ripples = Content.Load<Texture2D>("ripples.jpg");
            beach = Content.Load<Texture2D>("beach.jpg");
            fullScreen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            activeScreen = "mainMenu";
        }

        void playDemo_OnClick(object sender)
        {
            activeScreen = "mainMenu";
        }
        void characterCreator_OnClick(object sender)
        {
            CharacterCreator creator = (CharacterCreator)sender;
            activeScreen = creator.newScreen;
        }

        void mainMenu_OnClick(object sender)
        {
            MainMenu main = (MainMenu)sender;
            if (main.action == "newGame")
                activeScreen = "characterCreation";
            else if (main.action == "resumeGame")
            {
                player.Load();
                activeScreen = "playDemo";
            }
            else if (main.action == "exit")
                doOutroTransit = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        bool firstUpdateDone = false;
        static string privActiveScreen = null;
        float lerpConst = 0;
        bool lerpOut = false;
        float time = 0;
        float holdTime = 0;
        bool lerpDone = false;
        bool doOutroTransit = false;
        protected override void Update(GameTime gameTime)
        {
            #region doTransit?
            if (doTrasit || doIntroTransit)
            {
                characterCreator.transitInProg = true;
                playDemo.transitInProg = true;
                mainMenu.transitInProg = true;
            }
            else if (characterCreator.transitInProg)
            {
                characterCreator.transitInProg = false;
                playDemo.transitInProg = false;
                mainMenu.transitInProg = false;
            }
            #endregion

            #region Transit
            if (doIntroTransit)
            {
                if (time >= 0.5f)
                {
                    if (!lerpOut)
                    {
                        lerpConst = 0;
                        lerpOut = true;
                        bgColor = Color.CornflowerBlue;
                        holdTime = 2;
                    }
                    else if (lerpOut && holdTime > 0)
                    {
                        holdTime -= 0.1f;
                        characterCreator.Update(Cursor, gameTime);
                        firstUpdateDone = true;
                    }
                    else if (lerpOut && lerpConst <= 1 && holdTime <= 0)
                    {
                        lerpConst += 0.1f;
                    }
                    else if (lerpOut && lerpConst >= 1 && holdTime <= 0)
                    {
                        privActiveScreen = activeScreen;
                        doIntroTransit = false;
                        lerpOut = false;
                        lerpDone = true;
                    }
                    time = 0;
                }
            }
            else if (doOutroTransit)
            {
                if (time >= 0.5f)
                {
                    if (!lerpOut && lerpDone)
                    { 
                        lerpDone = false;
                        lerpConst = 1;
                        doDraw = true;
                    }
                    else if (!lerpOut && !lerpDone && lerpConst > 0)
                    {
                        lerpConst -= 0.1f;
                        holdTime = 1;
                    }
                    else if (!lerpOut && !lerpDone && lerpConst < 0.1f && holdTime > 0.1f)
                    {
                        holdTime -= 0.2f;
                    }
                    else if (!lerpOut && !lerpDone && lerpConst < 0.1f)
                        Exit();
                    time = 0;
                }
            }
            else if (doTrasit)
            {
                if (time >= 0.5f)
                {
                    if (!lerpOut && lerpDone)
                    { 
                        lerpDone = false;
                        lerpConst = 1;
                    }
                    else if (!lerpOut && !lerpDone && lerpConst > 0)
                    {
                        lerpConst -= 0.3f;
                    }
                    else if (!lerpOut && !lerpDone && lerpConst < 0.1f)
                    {
                        lerpOut = true;
                        doDraw = true;
                        holdTime = 0.5f;
                    }
                    else if (lerpOut && !lerpDone && lerpConst < 1 && holdTime > 0.1f)
                    {
                        holdTime -= 0.1f;
                    }
                    else if (lerpOut && !lerpDone && lerpConst < 1 && holdTime < 0.1f)
                    {
                        lerpConst += 0.3f;
                    }
                    else if (lerpOut && !lerpDone && lerpConst >= 1 && holdTime < 0.1f)
                    {
                        lerpDone = true;
                        lerpOut = false;
                        privActiveScreen = activeScreen;
                        doTrasit = false;
                    }

                    time = 0;
                }
            }
            #endregion
            #region ScreenPick
            if (activeScreen == "characterCreation")
            {
                if (lerpDone && activeScreen != privActiveScreen)
                {
                    doTrasit = true;
                    doDraw = false;
                }
                else if (!lerpDone && doTrasit)
                {
                    characterCreator.Update(Cursor, gameTime);
                }
                else if (lerpDone && activeScreen == privActiveScreen)
                    characterCreator.Update(Cursor, gameTime);
            }
            else if (activeScreen == "mainMenu")
            {
                if (lerpDone && activeScreen != privActiveScreen)
                {
                    doTrasit = true;
                    doDraw = false;
                }
                else if ((!lerpDone && doTrasit) || (!lerpDone && doIntroTransit))
                {
                    mainMenu.Update(Cursor);
                }
                else if (lerpDone && activeScreen == privActiveScreen)
                    mainMenu.Update(Cursor);   
            } 
            else if (activeScreen == "playDemo")
            {
                if (lerpDone && activeScreen != privActiveScreen)
                {
                    doTrasit = true;
                    doDraw = false;
                }
                else if (!lerpDone && doTrasit)
                {
                    playDemo.Update(gameTime, characterCreator.partsIndex);
                }
                else if (lerpDone && activeScreen == privActiveScreen)
                    playDemo.Update(gameTime, characterCreator.partsIndex);
            }

            if (activeScreen != privActiveScreen && lerpDone)
                doDraw = false;
            #endregion

            time += 0.01f * gameTime.ElapsedGameTime.Milliseconds;

            Cursor.Update(gameTime);
            base.Update(gameTime);
        }

        Color bgColor = Color.Black;
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (doDraw)
                spriteBatch.Draw(blank, fullScreen, bgColor);
            spriteBatch.End();
            if (activeScreen == "characterCreation" && firstUpdateDone && doDraw)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(water, fullScreen, bgColor);
                spriteBatch.End();
                characterCreator.Draw(spriteBatch);
            }
            else if (activeScreen == "mainMenu" && doDraw)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(ripples, fullScreen, bgColor);
                spriteBatch.End();
                mainMenu.Draw(spriteBatch);
            }
            else if (activeScreen == "playDemo" && firstUpdateDone && doDraw)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(beach, fullScreen, bgColor);
                spriteBatch.End();
                playDemo.Draw(spriteBatch);
            }


            if (activeScreen != privActiveScreen || !firstUpdateDone || doOutroTransit)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(blank, fullScreen, Color.Lerp(Color.Black, Color.Transparent, lerpConst));
                spriteBatch.End();
            }

            Cursor.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
