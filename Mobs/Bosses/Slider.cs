using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
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
            npc.damage = 0;
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

            //stop moving when hitting player
            float lifeQuotient = (float)npc.lifeMax / (float)npc.life;
            npc.ai[0] = 40 * lifeQuotient;
        }

        public override void AI()
        {
            npc.TargetClosest(true);

            bool spinning = false;
            Dust dust;

            //begin waking up when spawned
            if (npc.ai[2] == 0)
            {
                npc.ai[0]++;
                if (npc.ai[0] >= 75)
                {
                    npc.ai[2] = 1;
                    npc.ai[0] = 0;
                }
            }

            //move cooldown, lowers as boss loses life
            float lifeQuotient = (float)npc.lifeMax / (float)npc.life;
            if (npc.ai[2] == 1 || npc.ai[2] == 2)
            {
                npc.ai[0] += lifeQuotient;
            }

            //trigger phase 2 at 50% life
            if (lifeQuotient > 1.5f && spinning == false)
            {
                npc.ai[2] = 2;
            }

            //scale impact damage with speed, nerf in phase 2 due to higher speeds
            if (npc.ai[2] == 1)
            {
                npc.damage = (int)npc.velocity.Length() * 12;
            }
            else if (npc.ai[2] == 2)
            {
                npc.damage = (int)npc.velocity.Length() * 10;
            }
            //Main.NewText(npc.damage);
        
            //attempt to ram player on cooldown
            if (npc.HasValidTarget && spinning == false && (npc.ai[2] == 1 || npc.ai[2] == 2) && npc.ai[0] >= 120)
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
                    npc.velocity = right * dist;
                    Main.PlaySound(SoundID.Item100);
                }
                else if (angle.X < 0 && angle.Y > -0.75 && angle.Y < 0.75)
                {
                    npc.velocity = left * dist;
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
            else if (npc.ai[2] == 1 || npc.ai[2] == 2)
            {
                if (npc.ai[0] > 40 * lifeQuotient)
                {
                    npc.velocity.X *= 0.9f;
                    npc.velocity.Y *= 0.9f;
                }
            }

            //trigger spin attack in phase 2
            if (npc.ai[2] == 2)
            {
                npc.ai[3] += Main.rand.Next(1, 3);
                if (npc.ai[3] > 300)
                {
                    npc.ai[3] = 0;
                    spinning = true;
                    npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * 5;
                    npc.ai[2] = 3;
                }
            }

            //spin attack
            if (npc.ai[2] == 3)
            {
                npc.rotation++;
                dust = Main.dust[Terraria.Dust.NewDust(npc.position, 10, npc.height, 27, -2f, 0f, 0, new Color(255, 255, 255), 2.5f)];
                /*if (npc.collideX || npc.collideY)
                {
                    npc.ai[2] = 2;
                    npc.rotation = 0;
                    spinning = false;
                }*/
            }
            Main.NewText(npc.ai[2]);

            //stop phasing after 15 ticks
            if (npc.ai[0] >= 15 * lifeQuotient)
            {
                npc.noTileCollide = false;
            }

            //impact effects
            if (npc.collideX && npc.velocity.X != 0)
            {
                Main.PlaySound(SoundID.Item70);
                //left or right?
                if (npc.oldVelocity.X < 0)
                {
                    //purple sparkles
                    for (int i = 0; i < 10; i++)
                    {
                        dust = Main.dust[Terraria.Dust.NewDust(npc.position, 10, npc.height, 143, 1.5f, Main.rand.Next(-1,1), 0, default, 2f)];
                        dust = Main.dust[Terraria.Dust.NewDust(npc.position, 10, npc.height, 27, -2f, 0f, 0, new Color(255, 255, 255), 2.5f)];
                    }
                }
                else if (npc.oldVelocity.X > 0)
                {
                    //purple sparkles
                    for (int i = 0; i < 10; i++)
                    {
                        dust = Main.dust[Terraria.Dust.NewDust(new Vector2(npc.position.X + 88, npc.position.Y), 10, npc.height, 143, -1.5f, Main.rand.Next(-1,1), 0, default, 2f)];
                        dust = Main.dust[Terraria.Dust.NewDust(new Vector2(npc.position.X + 88, npc.position.Y), 10, npc.height, 27, 2f, 0f, 0, new Color(255, 255, 255), 2.5f)];
                    }
                }
            }
            if (npc.collideY && npc.velocity.Y != 0)
            {
                Main.PlaySound(SoundID.Item70);
                //as above,
                if (npc.oldVelocity.Y < 0)
                {
                    //purple sparkles
                    for (int i = 0; i < 10; i++)
                    {
                        dust = Main.dust[Terraria.Dust.NewDust(npc.position, npc.width, 10, 143, Main.rand.Next(-1,1), 1.5f, 0, default, 2f)];
                        dust = Main.dust[Terraria.Dust.NewDust(npc.position, npc.width, 10, 27, 0f, -2f, 0, default, 2.5f)];
                    }
                }
                //so below
                else if (npc.oldVelocity.Y > 0)
                {
                    //purple sparkles
                    for (int i = 0; i < 10; i++)
                    {
                        dust = Main.dust[Terraria.Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 88), npc.width, 10, 143, Main.rand.Next(-1,1), -1.5f, 0, default, 2f)];
                        dust = Main.dust[Terraria.Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 88), npc.width, 10, 27, 0f, 2f, 0, default, 2.5f)];
                    }
                }
            }



        }


        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[2] == 0)
            {
                npc.frameCounter++;
                if (npc.frameCounter < 25)
                {
                    npc.frame.Y = 1 * frameHeight;
                }
                else if (npc.frameCounter < 50)
                {
                    npc.frame.Y = 2 * frameHeight;
                }
                else if (npc.frameCounter < 75)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else
            {
                npc.frame.Y = 0 * frameHeight;
            }

        }



    }
}
