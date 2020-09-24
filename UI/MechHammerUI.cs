using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ObjectData;
using PaperMod.Necro;
using static Terraria.ModLoader.ModContent;
using PaperMod.Items.Tools;

namespace PaperMod.UI
{
    public class MechHammerUI : UIState
    {

        //vipixtoolbox used for reference a lot here

        private UIPanel area;
        public float panelWidth;
        public float panelHeight;
        public float buttonDimension;
        public float padding;
        List<UIImageButton> buttonList;
        public static bool visible = true;


        public override void OnInitialize()
        {

            panelWidth = 130f;
            panelHeight = 130f;
            buttonDimension = 38f;
            padding = 36f;

            area = new UIPanel();
            area.SetPadding(0);
            area.Left.Set((float)Main.screenWidth / 2 - panelWidth / 2, 0f);
            area.Top.Set((float)Main.screenHeight / 2 - panelHeight / 2, 0f);
            area.Width.Set(panelWidth, 0f);
            area.Height.Set(panelHeight, 0f);
            area.BackgroundColor = new Color(100, 0, 0, 100);
            area.BorderColor = new Color(100, 100, 0, 100);

            buttonList = new List<UIImageButton>();
            Texture2D buttonTexture;
            for (int i = 0; i < 6; i++)
            {
                buttonTexture = ModLoader.GetMod("PaperMod").GetTexture("UI/HammerButton/" + i.ToString());
                buttonList.Add(new UIImageButton(buttonTexture));
                area.Append(buttonList[i]);
            }

            buttonList[0].Left.Set(panelWidth / 2 + padding - buttonDimension / 2, 0f);
            buttonList[0].Left.Set(panelWidth / 2 + padding - buttonDimension / 2, 0f);

            buttonList[1].Left.Set(panelWidth / 2 + padding - buttonDimension / 2, 0f);
            buttonList[1].Left.Set(panelWidth / 2 - padding - buttonDimension / 2, 0f);

            buttonList[2].Left.Set(panelWidth / 2 - padding - buttonDimension / 2, 0f);
            buttonList[2].Left.Set(panelWidth / 2 + padding - buttonDimension / 2, 0f);

            buttonList[3].Left.Set(panelWidth / 2 - padding - buttonDimension / 2, 0f);
            buttonList[3].Left.Set(panelWidth / 2 - padding - buttonDimension / 2, 0f);

            buttonList[4].Left.Set(panelWidth / 2 + padding - buttonDimension / 2, 0f);
            buttonList[4].Left.Set(panelWidth / 2 - buttonDimension / 2, 0f);

            buttonList[5].Left.Set(panelWidth / 2 - buttonDimension / 2, 0f);
            buttonList[5].Left.Set(panelWidth / 2 - padding - buttonDimension / 2, 0f);

            for (int i = 0; i < buttonList.Count; i++)
            {
                int index = i;
                buttonList[i].OnClick += (evt, element) => ButtonClicked(index);
            }

            base.Append(area);
            Recalculate();    

        }


        public override void Update(GameTime gameTime)
        {
            area.Left.Set((float)Main.screenWidth / 2 - panelWidth / 2, 0f);
            area.Top.Set((float)Main.screenHeight / 2 - panelHeight / 2, 0f);
            //area.Left.Set(Main.mouseX - panelWidth / 2, 0f);
            //area.Top.Set(Main.mouseY - panelHeight / 2, 0f);
            Recalculate();

        }


        public void ButtonClicked(int index)
        {
            Mod mod = ModLoader.GetMod("PaperMod");
            Player player = Main.player[Main.myPlayer];
            Tile tile = Main.tile[Main.mouseX / 16, Main.mouseY / 16];

            switch (index)
            {
                case 0:
                    tile.halfBrick(false);
                    tile.slope(1);
                    break;
                case 1:
                    tile.halfBrick(false);
                    tile.slope(2);
                    break;
                case 2:
                    tile.halfBrick(false);
                    tile.slope(3);
                    break;
                case 3:
                    tile.halfBrick(false);
                    tile.slope(4);
                    break;
                case 4:
                    tile.slope(0);
                    tile.halfBrick(true);
                    break;
                case 5:
                    tile.halfBrick(false);
                    tile.slope(0);
                    break;
            }




        }


        protected override void DrawSelf(SpriteBatch spritebatch)
        {
            if (area.ContainsPoint(Main.MouseWorld))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }


    }

}