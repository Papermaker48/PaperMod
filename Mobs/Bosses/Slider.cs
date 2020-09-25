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

namespace PaperMod.Mobs.Bosses
{

    [AutoloadBossHead]
    class Slider : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temple Slider");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 4000;
            npc.damage = 34;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.width = 98;
            npc.height = 98;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.HitSound = SoundID.DD2_CrystalCartImpact;
            npc.DeathSound = SoundID.DD2_CrystalCartImpact;
            music = MusicID.Boss4;
            npc.boss = true;
        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Main.PlaySound(SoundID.NPCDeath43);
        }

        public override void AI()
        {
            npc.TargetClosest(true);

            float lifeQuotient = (float)npc.lifeMax / (float)npc.life;

            //move cooldown, lowers as boss loses life
            npc.ai[0] += lifeQuotient;

            //tile impact cooldown
            //npc.ai[1]++;

            //trigger phase 2 at 50% life
            if (lifeQuotient >= 1.5f)
            {
                npc.ai[2] = 1;
            }

            //scale impact damage with speed
            if (npc.ai[2] == 0)
            {
                npc.damage = (int)npc.velocity.Length() * 12;
            }
            else
            {
                npc.damage = (int)npc.velocity.Length() * 10;
            }
            Main.NewText(npc.damage);
        
            //attempt to ram player on cooldown
            if (npc.HasValidTarget && npc.ai[0] >= 120)
            {
                //move faster farther away
                float dist = Vector2.Distance(npc.Center, Main.player[npc.target].Center) * 0.02f;

                Vector2 angle = (npc.DirectionTo(Main.player[npc.target].Center));

                Vector2 left = new Vector2(-1, 0);
                Vector2 right = new Vector2(1, 0);
                Vector2 up = new Vector2(0, -1);
                Vector2 down = new Vector2(0, 1);

                //choose direction to move
                if (angle.X > 0 && angle.Y > -0.75 && angle.Y < 0.75)
                {
                    npc.velocity = right * dist * (npc.ai[2] + 0.5f);
                    Main.PlaySound(SoundID.Item100);
                }
                else if (angle.X < 0 && angle.Y > -0.75 && angle.Y < 0.75)
                {
                    npc.velocity = left * dist * (npc.ai[2] + 0.5f);
                    Main.PlaySound(SoundID.Item100);
                }
                else if (angle.Y < 0 && angle.X > -0.75 && angle.X < 0.75)
                {
                    npc.velocity = up * dist;
                    Main.PlaySound(SoundID.Item100);
                }
                else if (angle.Y > 0 && angle.X > -0.75 && angle.X < 0.75)
                {
                    npc.velocity = down * dist;
                    Main.PlaySound(SoundID.Item100);
                }

                //phase through barriers when attempting to move after ramming into them
                npc.noTileCollide = true;

                //reset cooldown after move
                npc.ai[0] = 0;
            }
            else if (npc.ai[0] > 40 * lifeQuotient)
            {
                npc.velocity.X *= 0.9f;
                npc.velocity.Y *= 0.9f;
            }

            //stop phasing after 15 ticks
            if (npc.ai[0] >= 15 * lifeQuotient)
            {
                npc.noTileCollide = false;
            }

            //impact effects
            if (npc.collideX && npc.velocity.X != 0)
            {
                Main.PlaySound(SoundID.Item70);
            }
            if (npc.collideY && npc.velocity.Y != 0)
            {
                Main.PlaySound(SoundID.Item70);
            }



                /*int posX = (int)(npc.position.X / 16f);
                int posY = (int)(npc.position.Y / 16f);
                for (int x = posX; x < 7; x++)
                {
                    for (int y = posY; y < 7; y++)
                    {

                        WorldGen.KillTile(x, y, false, false, true);

                    }
                }*/


        }


        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[2] == 1)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 15)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 30)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else if (npc.frameCounter < 45)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else if (npc.frameCounter < 60)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
        }



    }
}
