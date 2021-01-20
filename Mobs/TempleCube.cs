using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMod.Mobs
{
    class TempleCube : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Temple Block");
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1000;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 64;
            npc.height = 64;
            npc.HitSound = SoundID.DD2_CrystalCartImpact;
            npc.DeathSound = SoundID.DD2_CrystalCartImpact;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void AI()
        {
            npc.life = 1000;

            if (Main.LocalPlayer.Hitbox.Intersects(npc.Hitbox))
            {
                Vector2 direction = Main.LocalPlayer.DirectionTo(npc.Center);
                npc.velocity += direction;
                Main.LocalPlayer.velocity.X *= 0.1f;

            }
            npc.velocity *= 0.9f;

        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            npc.velocity.X = (projectile.velocity.X * knockback) * 0.05;
        }

    }
}
