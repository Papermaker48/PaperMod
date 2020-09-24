using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using PaperMod.Necro;
using static Terraria.ModLoader.ModContent;

namespace PaperMod
{
    class PlayerLayers : ModPlayer
    {

        public static readonly PlayerLayer VoidTouch = new PlayerLayer("PaperMod", "VoidTouch", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
            {
                return;
            }

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("PaperMod");

            if (drawPlayer.head != mod.GetEquipSlot("PaperHat", EquipType.Head))
            {
                return;
            }

            Texture2D texture = mod.GetTexture("Items/Developer/Papermaker/VoidTouchedFace");



            //float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
            //float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;

            //Vector2 origin = drawInfo.bodyOrigin;

            //Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            //float alpha = (255 - drawPlayer.immuneAlpha) / 255f;

            //Color color = Color.White;

            //Rectangle frame = drawPlayer.bodyFrame;

            //float rotation = drawPlayer.headRotation;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(texture, new Vector2((int)(drawInfo.position.X - Main.screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawInfo.position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + drawInfo.headOrigin, drawPlayer.bodyFrame, Color.White, drawPlayer.headRotation, drawInfo.headOrigin, 1f, spriteEffects, 0);

            //DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);

            //drawData.shader = drawInfo.bodyArmorShader;

            //Main.playerDrawData.Add(drawData);

            if (Main.rand.NextFloat() < 0.1f)
            {
                int index = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 14, drawPlayer.velocity.X * 0.5f, drawPlayer.velocity.Y * 0.5f, 160, new Color(255, 255, 255), 1.3f);
                Dust dust = Main.dust[index];
                dust.shader = GameShaders.Armor.GetSecondaryShader(drawInfo.bodyArmorShader, drawPlayer);

                Main.playerDrawDust.Add(index);
            }
        });


        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int bodyLayer = layers.FindIndex(l => l == PlayerLayer.Body);

            if (bodyLayer > -1)
            {
                layers.Insert(bodyLayer + 1, VoidTouch);
            }



        }
    }
}
