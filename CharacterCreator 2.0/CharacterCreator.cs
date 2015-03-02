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
    class CharacterCreator
    {
        public delegate void OnClickEventHandler(Object sender);
        public event OnClickEventHandler OnClick;

        Character player;

        public string newScreen = "mainMenu";

        TextField nameField;

        Rectangle menu;
        Button[] button;
        Button saveButton;
        Button loadButton;
        Button playButton;
        Button randomButton;
        //OptionsMenu[] optionsMenu;
        SubMenu[] subMenu;
        Rectangle subMenuDimentions;
        int acctiveSubMenu = 0;

        Texture2D deBugg;
        Rectangle portraitDimentions;

        public Dictionary<string, List<Texture2D>> portraitParts;
        public Dictionary<string, List<Texture2D>> modelParts;
        public Dictionary<string, int> partsIndex;

        public CharacterCreator(ContentManager Content, Character Player)
        {
            menu = new Rectangle(10, 50, 100, 400);
            button = new Button[6];

            saveButton = new Button(Content, new Rectangle(110, 390, 60, 40), "Font/Calibri");
            saveButton.isToggle = false;
            saveButton.OnClick += saveButton_OnClick;
            saveButton.text = "Save";
            loadButton = new Button(Content, new Rectangle(50, 390, 60, 40), "Font/Calibri");
            loadButton.isToggle = false;
            loadButton.OnClick += loadButton_OnClick;
            loadButton.text = "Load";
            playButton = new Button(Content, new Rectangle(170, 390, 60, 40), "Font/Calibri");
            playButton.isToggle = false;
            playButton.OnClick += playButton_OnClick;
            playButton.text = "Play";
            randomButton = new Button(Content, new Rectangle(45, 350, 190, 40), "Font/Calibri");
            randomButton.isToggle = false;
            randomButton.OnClick += randomButton_OnClick;
            randomButton.text = "Randomize";
            randomButton.canHoldDown = false;

            //optionsMenu = new OptionsMenu[6];
            subMenu = new SubMenu[6];
            int subMenu_holder = 235;
            subMenuDimentions = new Rectangle(130, 55, subMenu_holder / 2, subMenu_holder);

            player = Player;

            nameField = new TextField(Content, true, new Rectangle(250, 400, 300, 30), "Font/Calibri", 1f, 3);
            nameField.OnClick += nameField_OnClick;
            nameField.preText = player.name;

            for (int i = 0; i < button.Length; i++)
            {
                button[i] = new Button(Content, new Rectangle(menu.X, menu.Y + menu.Height / 10 * i, menu.Width, menu.Height / 10), "Font/Calibri");
                button[i].OnClick += CharacterCreator_OnClick;
                button[i].isToggle = true;
                button[i].canUnpress = false;
                button[i].memory.Add(i);

                //optionsMenu[i] = new OptionsMenu(Content);
                //optionsMenu[i].OnClick += optionsMenu_OnClick;
            }
            button[0].isPressed = true;

            #region Creation
            portraitParts = new Dictionary<string, List<Texture2D>>();
            modelParts = new Dictionary<string, List<Texture2D>>();
            partsIndex = new Dictionary<string, int>();

            List<string> ListPath = List_Path("Character/Model/Sex - Male/Clothes", Content);
            List<Texture2D> textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            modelParts.Add("clothes", textureHolder);

            ListPath = List_Path("Character/Model/Sex - Male/Eyes", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            modelParts.Add("eyes", textureHolder);

            ListPath = List_Path("Character/Model/Sex - Male/Hair", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            modelParts.Add("hair", textureHolder);

            ListPath = List_Path("Character/Portrait/Sex - Male/Body", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("body", textureHolder);
            partsIndex.Add("body", 0);
            button[0].text = "Body";
            button[0].memory.Add("body");
            subMenu[0] = new SubMenu(Content, subMenuDimentions, textureHolder, true, new Vector2());
            subMenu[0].memory.Add("body");

            ListPath = List_Path("Character/Portrait/Sex - Male/Eyes", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("eyes", textureHolder);
            partsIndex.Add("eyes", 0);
            button[1].text = "Eyes";
            button[1].memory.Add("eyes");
            subMenu[1] = new SubMenu(Content, subMenuDimentions, textureHolder, false, new Vector2(35, 25));
            subMenu[1].memory.Add("eyes");

            ListPath = List_Path("Character/Portrait/Sex - Male/Mouth", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("mouth", textureHolder);
            partsIndex.Add("mouth", 0);
            button[2].text = "Mouth";
            button[2].memory.Add("mouth");
            subMenu[2] = new SubMenu(Content, subMenuDimentions, textureHolder, false, new Vector2(35, 55));
            subMenu[2].memory.Add("mouth");

            ListPath = List_Path("Character/Portrait/Sex - Male/Nose", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("nose", textureHolder);
            partsIndex.Add("nose", 0);
            button[3].text = "Nose";
            button[3].memory.Add("nose");
            subMenu[3] = new SubMenu(Content, subMenuDimentions, textureHolder, false, new Vector2(35, 40));
            subMenu[3].memory.Add("nose");

            ListPath = List_Path("Character/Portrait/Sex - Male/Hair/Front", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("hair_front", textureHolder);
            partsIndex.Add("hair_front", 0);
            button[4].text = "Hair";
            button[4].memory.Add("hair_front");
            subMenu[4] = new SubMenu(Content, subMenuDimentions, textureHolder, true, new Vector2());
            subMenu[4].memory.Add("hair_front");

            ListPath = List_Path("Character/Portrait/Sex - Male/Hair/Back", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("hair_back", textureHolder);
            partsIndex.Add("hair_back", 0);

            ListPath = List_Path("Character/Portrait/Sex - Male/Clothes/Front", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("clothes_front", textureHolder);
            partsIndex.Add("clothes_front", 0);
            button[5].text = "Clothes";
            button[5].memory.Add("clothes_front");

            ListPath = List_Path("Character/Portrait/Sex - Male/Clothes/Back", Content);
            textureHolder = new List<Texture2D>(ListPath.Count);
            for (int i = 0; i < ListPath.Count; i++)
            {
                textureHolder.Add(Content.Load<Texture2D>(ListPath[i]));
            }
            portraitParts.Add("clothes_back", textureHolder);
            partsIndex.Add("clothes_back", 0);
            subMenu[5] = new SubMenu(Content, subMenuDimentions, textureHolder, true, new Vector2());
            subMenu[5].memory.Add("clothes_front");
            #endregion

            portraitDimentions = new Rectangle(270, 50, 240, 240);

            player.model.parts = modelParts;
            player.portrait.parts = portraitParts;

            foreach (SubMenu _menu in subMenu)
                _menu.OnClick +=_menu_OnClick;

            deBugg = Content.Load<Texture2D>("WP.png");
        }

        void randomButton_OnClick(object sender)
        {
            RandomizeCharacter();
        }

        void playButton_OnClick(object sender)
        {
            nameField.textEditDone();
            player.Save();
            nameField.text = player.name;
            newScreen = "playDemo";
            OnClick(this);
        }

        void loadButton_OnClick(object sender)
        {
            player.Load();
            nameField.text = player.name;
        }

        void saveButton_OnClick(object sender)
        {
            nameField.textEditDone();
            player.Save();
            nameField.text = player.name;
        }

        //void optionsMenu_OnClick(object sender)
        //{ 
        //}

        void nameField_OnClick(object sender)
        {
            TextField txt = (TextField)sender;
            player.name = txt.text;
        }

        void _menu_OnClick(object sender)
        {
            SubMenu menu = (SubMenu)sender;
            partsIndex[(string)menu.memory[0]] = (int)menu.box.memory[0];
        }

        void CharacterCreator_OnClick(object sender)
        {
            Button but = (Button)sender;
            //Console.WriteLine(but.text);
            acctiveSubMenu = (int)but.memory[0];
            foreach (Button _but in button)
                _but.isPressed = false;
        }

        public List<String> List_Path(string path, ContentManager Content)
        {
            var files = Directory.GetFiles(Content.RootDirectory +  "/" + path, "*.png");
            var fileList = files.Select(file => file.Remove(0, file.IndexOf("Character/"))).ToList();
            return fileList;
        }

        Random rnd = new Random();
        public bool transitInProg = false;
        public void Update(CustomCursor Cursor, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !transitInProg)
            {
                newScreen = "mainMenu";
                OnClick(this);
            }

            if (player.portrait.dimentions != portraitDimentions)
                player.portrait.dimentions = portraitDimentions;

            if (!player.model.deBugg)
                player.model.deBugg = true;

            foreach (Button but in button)
                but.Update(Cursor.hitbox);
            saveButton.Update(Cursor.hitbox);
            loadButton.Update(Cursor.hitbox);
            playButton.Update(Cursor.hitbox);
            randomButton.Update(Cursor.hitbox);
            player.Update(gameTime, partsIndex);

            nameField.Update(Cursor.hitbox);
            subMenu[acctiveSubMenu].Update(Cursor);
            //optionsMenu[acctiveSubMenu].Update(Cursor, gameTime);
            
            //if (Keyboard.GetState().IsKeyDown(Keys.F1))
            //    RandomizeCharacter();
            //if (Keyboard.GetState().IsKeyDown(Keys.F2))
            //    player.Save();
            //if (Keyboard.GetState().IsKeyDown(Keys.F3))
            //{ 
            //    player.Load();
            //    nameField.text = player.name;
            //}
        }

        private void RandomizeCharacter()
        {
            partsIndex["body"] = rnd.Next(portraitParts["body"].Count);
            partsIndex["clothes_front"] = rnd.Next(portraitParts["clothes_front"].Count);
            partsIndex["hair_front"] = rnd.Next(portraitParts["hair_front"].Count); ;
            partsIndex["eyes"] = rnd.Next(portraitParts["eyes"].Count);
            partsIndex["mouth"] = rnd.Next(portraitParts["mouth"].Count);
            partsIndex["nose"] = rnd.Next(portraitParts["nose"].Count);

            foreach (SubMenu sub in subMenu)
                for (int i = 0; i < sub.items.Count; i++)
                    sub.itemBoxes[i].isPressed = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button but in button)
                but.Draw(spriteBatch);
            saveButton.Draw(spriteBatch);
            loadButton.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
            randomButton.Draw(spriteBatch);

            subMenu[acctiveSubMenu].Draw(spriteBatch);
            //optionsMenu[acctiveSubMenu].Draw(spriteBatch);

            nameField.Draw(spriteBatch);
         
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            player.DrawModel(spriteBatch);
            player.DrawPortrait(spriteBatch);
            spriteBatch.End();

        }

    }

    class SubMenu
    {
        public delegate void OnClickEventHandler(Object sender);
        public event OnClickEventHandler OnClick;

        public List<object> memory = new List<object>();

        Rectangle dimentions;
        public List<Texture2D> items;
        List<Rectangle> itemDimentions;
        public List<CheckBox> itemBoxes;

        Texture2D deBugg;

        public SubMenu(ContentManager Content, Rectangle Dimentions, List<Texture2D> Items, bool doCenter, Vector2 itemOffsetFix)
        {
            deBugg = Content.Load<Texture2D>("WP.png");

            dimentions = Dimentions;
            items = Items;

            itemBoxes = new List<CheckBox>(items.Count);
            itemDimentions = new List<Rectangle>(items.Count);
            Rectangle temp = dimentions;
            int counter = 0;
            int trueCount = 0;
            while (true)
            {
                itemDimentions.Add(new Rectangle(temp.X, temp.Y, dimentions.Width / 2, dimentions.Height / 4));
                trueCount++;
                if (trueCount == items.Count)
                    break;
                counter++;
                temp.X += dimentions.Width / 2;
                if (counter == 2)
                {
                    temp.X = dimentions.X;
                    temp.Y += dimentions.Height / 4;
                    counter = 0;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                itemBoxes.Add(new CheckBox(Content, itemDimentions[i]));
                itemBoxes[i].item_texture = items[i];
                itemBoxes[i].OnClick += SubMenu_OnClick;
                itemBoxes[i].memory.Add(i);
                itemBoxes[i].canUnpress = false;
                itemBoxes[i].item_doCenter = doCenter;
                itemBoxes[i].item_offsetFix = itemOffsetFix;
            }
            itemBoxes[0].isPressed = true;
        }

        public CheckBox box;
        void SubMenu_OnClick(object sender)
        {
            box = (CheckBox)sender;
            foreach (CheckBox _box in itemBoxes)
                _box.isPressed = false;

            OnClick(this);
        }

        public void Update(CustomCursor cursor)
        {
            foreach (CheckBox box in itemBoxes)
                box.Update(cursor.hitbox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(deBugg, dimentions, Color.Lerp(Color.Black, Color.Transparent, 0.255f));
            spriteBatch.End();
            
            foreach (CheckBox box in itemBoxes)
                box.Draw(spriteBatch);
        }
    }

    //class OptionsMenu
    //{
    //    public delegate void OnClickEventHandler(Object sender);
    //    public event OnClickEventHandler OnClick;

    //    Slider2D xPosSlider;
    //    Slider2D yPosSlider;

    //    public OptionsMenu(ContentManager Content)
    //    {
    //        xPosSlider = new Slider2D(Content, new Rectangle(500, 100, 200, 20), false);
    //        xPosSlider.OnMove += xPosSlider_OnMove;
    //        yPosSlider = new Slider2D(Content, new Rectangle(400, 500, 300, 30), true);
    //        yPosSlider.OnMove += yPosSlider_OnMove;
    //    }

    //    void yPosSlider_OnMove(object sender)
    //    {
    //        OnClick(this);
    //    }

    //    void xPosSlider_OnMove(object sender)
    //    {
    //        OnClick(this);
    //    }

    //    public void Update(CustomCursor cursor, GameTime gameTime)
    //    {
    //        xPosSlider.Update(cursor.hitbox, gameTime);
    //        yPosSlider.Update(cursor.hitbox, gameTime);
    //    }

    //    public void Draw(SpriteBatch spriteBatch)
    //    {
    //        xPosSlider.Draw(spriteBatch);
    //        yPosSlider.Draw(spriteBatch);
    //    }
    //}
}
