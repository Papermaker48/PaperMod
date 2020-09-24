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
            Main.npcFrameCount[npc.type] = 5;
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

            //move cooldown
            npc.ai[0]++;

            //tile impact cooldown
            npc.ai[1]++;

            //attempt to ram player on cooldown
            if (npc.HasValidTarget && npc.ai[0] == 120)
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
                }
                else if (angle.X < 0 && angle.Y > -0.75 && angle.Y < 0.75)
                {
                    npc.velocity = left * dist;
                }
                else if (angle.Y < 0 && angle.X > -0.75 && angle.X < 0.75)
                {
                    npc.velocity = up * dist;
                }
                else if (angle.Y > 0 && angle.X > -0.75 && angle.X < 0.75)
                {
                    npc.velocity = down * dist;
                }

                //phase through barriers when attempting to move after ramming into them
                npc.noTileCollide = true;

                //reset cooldown after move
                npc.ai[0] = 0;
            }
            else if (npc.ai[0] > 40)
            {
                npc.velocity.X *= 0.9f;
                npc.velocity.Y *= 0.9f;
            }

            //stop phasing after 15 ticks
            if (npc.ai[0] == 15)
            {
                npc.noTileCollide = false;
            }

            //impact effects
            if (npc.collideX && npc.velocity.X != 0)
            {
                Main.PlaySound(SoundID.Item70);
                npc.ai[1] = 0;
            }
            if (npc.collideY && npc.velocity.Y != 0)
            {
                Main.PlaySound(SoundID.Item70);
                npc.ai[1] = 0;
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

    }
}
