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
    public class PaperPlayer : ModPlayer
    {

        public static int necroMinions;
        public const int maxSoulCrystalsUsed = 2;
        public int soulCrystalsUsed;
        public const int maxLifeEssenceCrystalsUsed = 10;
        public int lifeEssenceCrystalsUsed;

        public override void ResetEffects()
        {
            //Main.LocalPlayer.GetModPlayer<NecroDamagePlayer>().soulsMax2 += soulCrystalsUsed;
            //Main.LocalPlayer.GetModPlayer<NecroDamagePlayer>().lifeEssenceMax2 += lifeEssenceCrystalsUsed;
        }


        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)player.whoAmI);
            packet.Write(soulCrystalsUsed);
            packet.Write(lifeEssenceCrystalsUsed);
            packet.Send(toWho, fromWho);
        }


        public override TagCompound Save()
        {
            return new TagCompound
            {
                {"soulCrystalsUsed", soulCrystalsUsed},
                {"lifeEssenceCrystalsUsed", lifeEssenceCrystalsUsed}
            };
        }


        public override void Load(TagCompound tag)
        {
            soulCrystalsUsed = tag.GetInt("soulCrystalsUsed");
            soulCrystalsUsed = tag.GetInt("lifeEssenceCrystalsUsed");
        }


        public static readonly PlayerLayer WornGoggles = new PlayerLayer("PaperMod", "WornGoggles", PlayerLayer.FaceAcc, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
            {
                return;
            }

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("PaperMod");

            /*if (drawPlayer.face != mod.GetAccessorySlot("AccGoggles", EquipType.Face))
            {
                return;
            }*/

            Texture2D texture = mod.GetTexture("Items/Accessories/AccGoggles");

            float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
            float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.headFrame.Height / 2 + 4f;

            Vector2 origin = drawInfo.headOrigin;

            Vector2 position = new Vector2(drawX, drawY) + drawPlayer.headPosition - Main.screenPosition;

            float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
            Color color = Color.White;

            Rectangle frame = drawPlayer.headFrame;

            float rotation = drawPlayer.headRotation;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, SpriteEffects.FlipHorizontally, 0);

            drawData.shader = drawInfo.faceShader;

            Main.playerDrawData.Add(drawData);

        });

        /*public static readonly PlayerLayer RobeGlow = new PlayerLayer("PaperMod", "RobeGlow", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {

            if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
            {
                return;
            }

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("PaperMod");

            if (drawPlayer.body != mod.GetEquipSlot("ScalemailRobe", EquipType.Body))
            {
                return;
            }

            Texture2D texture = mod.GetTexture("Items/Armor/ScalemailRobe_Body");

            float drawX = (int)drawInfo.position.X + drawPlayer.width / 2;
            float drawY = (int)drawInfo.position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;

            Vector2 origin = drawInfo.bodyOrigin;

            Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;

            float alpha = (255 - drawPlayer.immuneAlpha) / 255f;

            Color color = Color.White;

            Rectangle frame = drawPlayer.bodyFrame;

            float rotation = drawPlayer.bodyRotation;

            SpriteEffects spriteEffects = drawInfo.spriteEffects;

            DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);

            drawData.shader = drawInfo.bodyArmorShader;

            Main.playerDrawData.Add(drawData);

            if (Main.rand.NextFloat() < 0.1f)
            {
                int index = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 14, drawPlayer.velocity.X * 0.5f, drawPlayer.velocity.Y * 0.5f, 160, new Color(255, 255, 255), 1.3f);
                Dust dust = Main.dust[index];
                dust.shader = GameShaders.Armor.GetSecondaryShader(drawInfo.bodyArmorShader, drawPlayer);

                Main.playerDrawDust.Add(index);
            }
        });*/


        public Vector3[,] soul = new Vector3[10, Main.player.Length];


        public static readonly PlayerLayer necroSoul = new PlayerLayer("PaperMod", "necroSoul", PlayerLayer.Body, delegate (PlayerDrawInfo drawInfo)
        {
        if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
        {
            return;
        }
        Player drawPlayer = drawInfo.drawPlayer;
        PaperPlayer modPlayer = drawInfo.drawPlayer.GetModPlayer<PaperPlayer>();
        Mod mod = ModLoader.GetMod("Papermod");
        Texture2D texture = mod.GetTexture("Necro/NecroSoul");
        

            int drawX = (int)(drawPlayer.position.X - Main.screenPosition.X);
            int drawY = (int)(drawPlayer.position.Y - Main.screenPosition.Y);
            Vector2 position = drawPlayer.Center;
            position.X += (float)Math.Sin(modPlayer.soul[1, drawPlayer.whoAmI].Y) * 50;
            position.Y += (float)Math.Sin(modPlayer.soul[1, drawPlayer.whoAmI].Y) * 50;
            Vector2 origin = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.5f);
            //pos.Y -= drawPlayer.mount.PlayerOffset;
            DrawData data = new DrawData(texture, position, new Rectangle(0, (int)modPlayer.soul[1, drawPlayer.whoAmI].Z * texture.Height / 4, texture.Width, texture.Height / 4), Color.White, 0, origin, 1f, drawPlayer.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            data.shader = drawInfo.bodyArmorShader;
            Main.playerDrawData.Add(data);

        });





        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            //int bodyLayer = layers.FindIndex(l => l == PlayerLayer.Body);

            /*if (bodyLayer > -1)
            {
                layers.Insert(bodyLayer + 1, RobeGlow);
            }*/

            int effectFrontLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("MiscEffectsFront"));
            if (effectFrontLayer != -1)
            {
                necroSoul.visible = true;
                layers.Insert(effectFrontLayer + 1, WornGoggles);
                layers.Insert(effectFrontLayer + 1, necroSoul);

            }

        }

    }
}
