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
using static Terraria.ModLoader.ModContent;

namespace Papermod.Necro.Projectiles
{
    public class ScytheDemoniteThrown : ModProjectile
    {

        public int timer;
        public bool hasHitEnemy = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonite Scythe");
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
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
                Texture2D texture = mod.GetTexture("Necro/Projectiles/ScytheDemoniteThrown_Glow");
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

            //demonite dust
            if (Main.rand.NextFloat() < 0.3f)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 14, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 160, new Color(255, 255, 255), 1.3f)];
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

            //sprite rotation, remove angle while returning
            timer++;
            if (timer > 35)
            {
                projectile.rotation = 0;
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation();
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
                else
                {
                    float num43 = 15f;
                    float num44 = 0.6f;

                    Vector2 vector2 = new Vector2(base.projectile.position.X + (float)projectile.width * 0.5f, base.projectile.position.Y + (float)projectile.height * 0.5f);
                    float num45 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector2.X;
                    float num46 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector2.Y;
                    float num47 = (float)Math.Sqrt(num45 * num45 + num46 * num46);
                    if (num47 > 3000f)
                        projectile.Kill();

                    num47 = num43 / num47;
                    num45 *= num47;
                    num46 *= num47;
                    {
                        if (base.projectile.velocity.X < num45)
                        {
                            base.projectile.velocity.X += num44;
                            if (base.projectile.velocity.X < 0f && num45 > 0f)
                                base.projectile.velocity.X += num44;
                        }
                        else if (base.projectile.velocity.X > num45)
                        {
                            base.projectile.velocity.X -= num44;
                            if (base.projectile.velocity.X > 0f && num45 < 0f)
                                base.projectile.velocity.X -= num44;
                        }

                        if (base.projectile.velocity.Y < num46)
                        {
                            base.projectile.velocity.Y += num44;
                            if (base.projectile.velocity.Y < 0f && num46 > 0f)
                                base.projectile.velocity.Y += num44;
                        }
                        else if (base.projectile.velocity.Y > num46)
                        {
                            base.projectile.velocity.Y -= num44;
                            if (base.projectile.velocity.Y > 0f && num46 < 0f)
                                base.projectile.velocity.Y -= num44;
                        }
                    }

                    if (Main.myPlayer == projectile.owner)
                    {
                        Rectangle scytheHitbox = new Rectangle((int)base.projectile.position.X, (int)base.projectile.position.Y, projectile.width / 2, projectile.height);
                        Rectangle playerHitbox = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                        if (scytheHitbox.Intersects(playerHitbox))
                            projectile.Kill();
                    }
                }


                       
            }



        }

    }
}
