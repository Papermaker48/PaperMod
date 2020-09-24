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
using static Terraria.ModLoader.ModContent;

namespace Papermod.Necro.Projectiles
{
    public class ScytheDiscordThrown : ModProjectile
    {

        public int timer;
        public bool hasHitEnemy = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordant Scythe");
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 110;
            projectile.height = 46;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.knockBack = 3f;           
        }



        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (hasHitEnemy == false)
            {
                projectile.damage /= 2;
            }
            hasHitEnemy = true;
        }

        //projectile trail
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle firstFrame = new Rectangle();
            firstFrame.Width = 110;
            firstFrame.Height = 46;
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, firstFrame, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }


        //fullbright before hitting enemy
        public override Color? GetAlpha(Color lightColor)
        {
            if (hasHitEnemy == false)
            {
                return Color.White;
            }
            else
            {
                return null;
            }

        }


        //projectile glowmask
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (hasHitEnemy == false)
            {
                Texture2D texture = mod.GetTexture("Necro/Projectiles/ScytheDiscordThrown_Glow");
                spriteBatch.Draw
                    (
                    texture,
                    new Vector2
                    (
                        projectile.position.X - Main.screenPosition.X + projectile.width * 0.5f,
                        projectile.position.Y - Main.screenPosition.Y + projectile.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    projectile.rotation,
                    texture.Size() * 0.5f,
                    projectile.scale,
                    SpriteEffects.None,
                    0f
                    );
            }
        }


        public override void AI()
        {

            var player = Main.player[projectile.owner];

            if (player.dead)
            {
                projectile.Kill();
                return;
            }

            player.itemAnimation = 15;
            player.itemTime = 15;

            //pink dust
            if (Main.rand.NextFloat() < 0.3f)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 164, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 160, new Color(255, 255, 255), 1.2f)];
            }

            //disable light particles and return to normal damage after first enemy hit
            if (hasHitEnemy == true)
            {
                projectile.light = 0f;
            }
            else
            {
                projectile.light = 0.5f;
                if (Main.rand.NextFloat() < 0.4f)
                {
                    Dust dust;
                    dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 43, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, new Color(255, 255, 255), 1f)];
                }
            }

            //animation
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 3)
                {
                    projectile.frame = 0;
                }
            }


            timer++;
            //slow to stop for teleport
            if (timer > 30)
            {
                projectile.velocity *= 0.95f;
            }

            //sprite rotation, remove angle while returning
            if (timer > 35)
            {
                projectile.rotation = 0;
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation();
            }

            if (timer > 45)
            {
                //only spawn imminent teleport dust if teleport location is valid
                int blockX = (int)(projectile.Center.X / 16f);
                int blockY = (int)(projectile.Center.Y / 16f);
                if ((Main.tile[blockX, blockY].wall != 87) || NPC.downedPlantBoss && !Collision.SolidCollision(projectile.Center, player.width, player.height))
                {
                    Dust dust;
                    dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 164, Main.rand.Next(-16, 16) + projectile.velocity.X, Main.rand.Next(-3, 3) + projectile.velocity.Y, 0, new Color(255, 255, 255), 1.6f)];
                }

            }
            
            if (timer > 77)
            {
                //ditto
                int blockX = (int)(projectile.Center.X / 16f);
                int blockY = (int)(projectile.Center.Y / 16f);
                if ((Main.tile[blockX, blockY].wall != 87) || NPC.downedPlantBoss)
                {
                    if (!Collision.SolidCollision(projectile.Center, player.width, player.height) || player.name == "Rektar")
                    {
                        Dust dust;
                        for (int i = 0; i < 3; i++)
                        {
                            //vertical scythe particles
                            dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 164, Main.rand.Next(-1, 1) + projectile.velocity.X, Main.rand.Next(-19, 19) + projectile.velocity.Y, 0, new Color(255, 255, 255), 1.6f)];
                            //player particles
                            dust = Main.dust[Terraria.Dust.NewDust(player.position, player.width, player.height, 164, Main.rand.Next(-1, 1), Main.rand.Next(-10, 10), 0, new Color(255, 255, 255), 1.4f)];
                        }
                    }
                }


            }

            //teleport code
            if (timer > 80)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    player = Main.player[projectile.owner];

                    //no pre plantera temple cheese, or teleporting into blocks
                    int blockX = (int)(projectile.Center.X / 16f);
                    int blockY = (int)(projectile.Center.Y / 16f);
                    if ((Main.tile[blockX, blockY].wall != 87) || NPC.downedPlantBoss)
                    {
                        //rektar gets free sv_cheats
                        if (!Collision.SolidCollision(projectile.Center, player.width, player.height) || player.name == "Rektar")
                        {
                            //teleport player to scythe
                            Vector2 telePos = new Vector2(projectile.Center.X, projectile.Center.Y - 8);
                            player.Teleport(telePos, 1);
                            //damage if player has chaos state
                            if (player.chaosState)
                            {
                                player.statLife -= player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.Next(2) == 0)
                                    damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);

                                if (player.statLife <= 0)
                                    player.KillMe(damageSource, 1.0, 0);

                                player.lifeRegenCount = 0;
                                player.lifeRegenTime = 0;
                            }

                            //inflict chaos state for 4 seconds
                            player.AddBuff(88, 240);

                            //telefrag nearby npcs
                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                NPC npc = Main.npc[i];
                                float distance = Vector2.Distance(projectile.Center, npc.Center);
                                if (npc.CanBeChasedBy() && distance < 86)
                                {
                                    npc.StrikeNPC(projectile.damage * 3, 14, (npc.Center.X >= projectile.Center.X).ToDirectionInt());
                                    player.immune = true;
                                    player.immuneTime = 40;
                                    Main.PlaySound(SoundID.Item68, projectile.Center);
                                }
                            }
                        }

                        

                    }
                    
                }

                projectile.Kill();

            }


            {
                if (projectile.soundDelay == 0)
                {
                    projectile.soundDelay = 12;
                    Main.PlaySound(SoundID.Item71, base.projectile.position);
                }

                if (this.projectile.ai[0] == 0f)
                {
                    this.projectile.ai[1] += 1f;
                    if (projectile.type == 106 && this.projectile.ai[1] >= 45f)
                    {
                        this.projectile.ai[0] = 1f;
                        this.projectile.ai[1] = 0f;
                        projectile.netUpdate = true;
                    }

                    else if (this.projectile.ai[1] >= 30f)
                    {
                        this.projectile.ai[0] = 1f;
                        this.projectile.ai[1] = 0f;
                        projectile.netUpdate = true;
                    }
                }


                       
            }



        }


    }
}
