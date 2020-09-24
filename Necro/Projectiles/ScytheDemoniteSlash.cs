using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace PaperMod.Necro.Projectiles
{
    public class ScytheDemoniteSlash : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonite Scythe Slash");
            Main.projFrames[ProjectileType<ScytheDemoniteSlash>()] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 84;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.knockBack = 6f;
        }

        public override void AI()
        {

            Player player = Main.player[projectile.owner];

            if (player.direction == 1)
            {
                projectile.position.X = player.position.X + 20;
                projectile.position.Y = player.position.Y + 6;
            }
            else
            {
                projectile.position.X = player.position.X - 84;
                projectile.position.Y = player.position.Y + 6;
            }

            projectile.direction = player.direction;
            projectile.spriteDirection = player.direction;


            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            //despawn timer SHOULD PROBABLY OPTIMIZE LATER WITH TIMELEFT INSTEAD
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 24f)
            {
                projectile.ai[0] = 20;
                projectile.Kill();
            }

            //demonite dust
            if (player.direction == 1)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Dust dust;
                    dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 14, Main.rand.Next(1,2), Main.rand.Next(-1, 1) * 0.5f, 160, new Color(255, 255, 255), 1.3f)];
                }
            }
            else if (Main.rand.NextFloat() < 0.1f)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(projectile.position, projectile.width, projectile.height, 14, Main.rand.Next(-2,-1), Main.rand.Next(-1, 1) * 0.5f, 160, new Color(255, 255, 255), 1.3f)];
            }


        }


    }
}
