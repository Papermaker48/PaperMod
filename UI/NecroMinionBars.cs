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
using PaperMod.Necro;
using PaperMod.Necro.Minions;
using static Terraria.ModLoader.ModContent;

namespace PaperMod.UI
{
    internal class NecroMinionBars : UIState
    {

        private UIElement area;
        List<UIImage> healthBars;
        List<UIText> healthText;

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 560, 1f);
            area.Top.Set(20, 0f);
            area.Width.Set(152, 0f);
            area.Height.Set(600, 0f);

            healthBars = new List<UIImage>();
            Texture2D barFrame;
            for (int i = 0; i < 6; i++)
            {
                barFrame = ModLoader.GetMod("PaperMod").GetTexture("UI/MinionHealthBar");
                healthBars.Add(new UIImage(barFrame));
                area.Append(healthBars[i]);
            }

            healthBars[0].Left.Set(0, 0f);
            healthBars[0].Top.Set(100, 0f);

            Append(area);

        }


        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

            var modPlayer = Main.LocalPlayer.GetModPlayer<NecroDamagePlayer>();

            Rectangle hitbox = healthBars[0].GetInnerDimensions().ToRectangle();
            hitbox.X += 6;
            hitbox.Y += 6;

            Texture2D barTexture = GetTexture("PaperMod/UI/MinionHealthBar");
            Texture2D fillTexture = GetTexture("PaperMod/UI/MinionHealthBarFill");
            Texture2D fillTextureBack = GetTexture("PaperMod/UI/MinionHealthBarFillBack");


            const int fillSize = 152;
            const int barEdge = 6;
            const int fillWidth = fillSize - 2 * barEdge;
            const int fillHeight = 10;



            float barFillPercent = Math.Min(10, 50);
            float fill = barFillPercent / 50;

            Vector2 startPos = new Vector2(hitbox.X, hitbox.Y);
            var barOffset = 60;

            for (int i = 0; i < PaperPlayer.necroMinions; i++)
            {
                var offsetPos = startPos + new Vector2(0, (barOffset * i));
                var barOffsetPos = startPos + new Vector2(-6, (barOffset * i) - 6);
                Main.spriteBatch.Draw(barTexture, barOffsetPos, Color.White);
                Main.spriteBatch.Draw(fillTextureBack, offsetPos, Color.White);
                Main.spriteBatch.Draw(fillTexture, offsetPos, new Rectangle(0, 0, (int)(fill * fillWidth), fillHeight), Color.White);
            }



            /*Vector2 startPos = new Vector2(hitbox.X + 160, hitbox.Y + 20);
            var gemOffset = 22;

            //draw soul gem socket backs
            for (var i = 0; i < 3; i++)
            {
                var offsetPos = startPos + new Vector2((gemOffset * i), 0);
                Main.spriteBatch.Draw(fillMissingSocket, offsetPos, Color.White);
            }

            //draw soul gem sockets
            for (var i = 0; i < modPlayer.soulsMax2; i++)
            {
                var offsetPos = startPos + new Vector2((gemOffset * i), 0);
                Main.spriteBatch.Draw(fillSoulGemBack, offsetPos, Color.White);
            }

            //draw soul gems
            for (var i = 0; i < modPlayer.soulsCurrent; i++)
            {
                var offsetPos = startPos + new Vector2((gemOffset * i), 0);
                Main.spriteBatch.Draw(fillSoulGem, offsetPos, Color.White);
            }*/

        }


    }
}
