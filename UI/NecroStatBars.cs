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
using static Terraria.ModLoader.ModContent;

namespace PaperMod.UI
{
    internal class NecroStatBars : UIState
    {

        private UIElement area;
        private UIText lifeEssenceText;
        private UIImage barFrame;
        private UIText soulsText;

        public override void OnInitialize()
        {

            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 620, 1f);
            area.Top.Set(20, 0f);
            area.Width.Set(280, 0f);
            area.Height.Set(110, 0f);

            barFrame = new UIImage(GetTexture("PaperMod/UI/LifeEssenceBar"));
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(280, 0f);
            barFrame.Height.Set(80, 0f);

            lifeEssenceText = new UIText("0/0", 1f);
            lifeEssenceText.Left.Set(0, 0f);
            lifeEssenceText.Top.Set(8, 0f);
            lifeEssenceText.Width.Set(280, 0f);
            lifeEssenceText.Height.Set(110, 0f);

            soulsText = new UIText("0/0", 1f);
            soulsText.Left.Set(-10, 0f);
            soulsText.Top.Set(56, 0f);
            soulsText.Width.Set(280, 0f);
            soulsText.Height.Set(110, 0f);

            area.Append(lifeEssenceText);
            area.Append(barFrame);
            area.Append(soulsText);
            Append(area);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }


        protected override void DrawSelf(SpriteBatch spriteBatch)
        {

            var modPlayer = Main.LocalPlayer.GetModPlayer<NecroDamagePlayer>();

            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 28;
            hitbox.Width -= 56;
            hitbox.Y += 32;
            hitbox.Height -= 64;

            Texture2D fillTexture = GetTexture("PaperMod/UI/LifeEssenceBarFill");
            Texture2D fillTextureBack = GetTexture("PaperMod/UI/LifeEssenceBarFillBack");
            Texture2D fillMissingSocket = GetTexture("PaperMod/UI/LifeEssenceBarMissingGem");
            Texture2D fillSoulGem = GetTexture("PaperMod/UI/SoulGemFill");
            Texture2D fillSoulGemBack = GetTexture("PaperMod/UI/SoulGemFillBack");


            const int fillSize = 236;
            const int barEdge = 6;
            const int fillWidth = fillSize - 2 * barEdge;
            const int fillHeight = 16;

            float barFillPercent = Math.Min(modPlayer.lifeEssenceCurrent, modPlayer.lifeEssenceMax2);
            float fill = barFillPercent / modPlayer.lifeEssenceMax2;

            //draw life essence
            Main.spriteBatch.Draw(fillTextureBack, new Vector2(hitbox.X, hitbox.Y), Color.White);
            Main.spriteBatch.Draw(fillTexture, new Vector2(hitbox.X, hitbox.Y), new Rectangle(0, 0, (int)(fill * fillWidth), fillHeight), Color.White);


            Vector2 startPos = new Vector2(hitbox.X + 160, hitbox.Y + 20);
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
            }

        }


        public override void Update(GameTime gameTime)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<NecroDamagePlayer>();
            lifeEssenceText.SetText($"Life Essence: {modPlayer.lifeEssenceCurrent} / {modPlayer.lifeEssenceMax2}");
            soulsText.SetText($"{modPlayer.soulsCurrent} / {modPlayer.soulsMax2} Souls");
            base.Update(gameTime);
        }


    }
}
