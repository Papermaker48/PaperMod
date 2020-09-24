using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace PaperMod.Projectiles
{
    class AstralRift : ModProjectile
    {

        List<Dust> dustList;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Rift");
            //Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 450;
            projectile.height = 450;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.knockBack = 0f;
            projectile.timeLeft = 500;
        }


        //projectile trail
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle firstFrame = new Rectangle();
            firstFrame.Width = 450;
            firstFrame.Height = 450;
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, firstFrame, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {

            projectile.velocity *= 0.95f;
            projectile.rotation += 0.1f;

            if (projectile.timeLeft < 50)
            {
                projectile.scale *= 0.9f;
            }
            else
            {
                projectile.scale *= 1.0015f;
            }

            var rotation0 = new Vector2();
            var rotation1 = new Vector2();
            var rotation2 = new Vector2();
            var rotation3 = new Vector2();
            var rotation4 = new Vector2();
            var rotation5 = new Vector2();
            Random rnd = new Random();
            dustList = new List<Dust>();
            for (int i = 0; i < 6; i++)
            {
                rotation0 = projectile.rotation.ToRotationVector2();
                rotation1 = (projectile.rotation + MathHelper.ToRadians(51)).ToRotationVector2();
                rotation2 = (projectile.rotation + MathHelper.ToRadians(102)).ToRotationVector2();
                rotation3 = (projectile.rotation + MathHelper.ToRadians(153)).ToRotationVector2();
                rotation4 = (projectile.rotation + MathHelper.ToRadians(255)).ToRotationVector2();
                rotation5 = (projectile.rotation + MathHelper.ToRadians(306)).ToRotationVector2();
                dustList.Add(new Dust());
                dustList[i] = Dust.NewDustPerfect((rotation0 * 230f * projectile.scale + projectile.Center), 170, projectile.velocity, 0, Color.White, 2);
            }


            dustList[0].position = (rotation0 * 230f * projectile.scale + projectile.Center);
            dustList[0].noGravity = true;
            dustList[1].position = (rotation1 * 230f * projectile.scale + projectile.Center);
            dustList[1].noGravity = true;
            dustList[2].position = (rotation2 * 230f * projectile.scale + projectile.Center);
            dustList[2].noGravity = true;
            dustList[3].position = (rotation3 * 230f * projectile.scale + projectile.Center);
            dustList[3].noGravity = true;
            dustList[4].position = (rotation4 * 230f * projectile.scale + projectile.Center);
            dustList[4].noGravity = true;
            dustList[5].position = (rotation5 * 230f * projectile.scale + projectile.Center);
            dustList[5].noGravity = true;

        }


    }
}
