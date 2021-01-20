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

namespace PaperMod.Necro.Minions
{

    public class BloodZombieMinion : ModProjectile
    {
        public override string Texture => "Terraria/NPC_" + NPCID.BloodZombie;

        public static List<int> minionProjectileIDs;
        public int minionLife = 50;
        private bool minionImmune = false;
        private float minionImmuneTime = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Summoned Blood Zombie");
            Main.projFrames[projectile.type] = 9;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }


        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 44;
            projectile.friendly = true;
            projectile.penetrate = -1;
        }


        public override void Kill(int timeLeft)
        {
            if (PaperPlayer.necroMinions > 0)
                PaperPlayer.necroMinions--;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }


        public override bool MinionContactDamage()
        {
            return true;
        }


        public override void AI()
        {
            minionProjectileIDs.Add(projectile.whoAmI);

            if (++projectile.frameCounter >= 5 && projectile.velocity.X != 0 && projectile.velocity.Y == 0)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 9)
                {
                    projectile.frame = 1;
                }
            }
            else if (projectile.velocity.Y > 0)
            {
                projectile.frame = 0;
            }

            projectile.velocity.Y += 0.4f;

            Player player = Main.player[projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<BloodZombieBuff>());
            }

            if (player.HasBuff(BuffType<BloodZombieBuff>()))
            {
                projectile.timeLeft = 2;
            }

            projectile.ai[0] += 1f;
            projectile.ai[1] += 1f;

            //minion HP, iframes
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                Rectangle enemyHitbox = npc.getRect();
                Rectangle minionHitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                if (enemyHitbox.Intersects(minionHitbox) && !npc.friendly && minionImmune == false)
                {
                    DamageMinion(npc.damage);
                }
            }

            //tick down iframes after minion is hit
            if (projectile.ai[1] >= minionImmuneTime)
            {
                projectile.ai[1] = 0f;
                minionImmune = false;
                minionImmuneTime = 0;
                projectile.netUpdate = true;
            }


            /*Vector2 idlePosition = player.Center;
            idlePosition.X -= (15 + player.width / 2) * player.direction;
            idlePosition.X -= projectile.minionPos * 40 * player.direction;
            NPC target = null;*/




            Vector2 gatherPosition = player.Center;
            //move gatherPos around occasionally
            if (projectile.ai[0] >= Main.rand.NextFloat(60f, 300f))
            {
                projectile.ai[0] = 0f;
                gatherPosition.X += Main.rand.NextFloat(-64f, 64f);
                projectile.netUpdate = true;
            }


            //tp to gatherPos if too far
            Vector2 vectorToGatherPosition = gatherPosition - projectile.Center;
            float distanceToGatherPosition = vectorToGatherPosition.Length();
            if (Main.myPlayer == player.whoAmI && distanceToGatherPosition > 2000f)
            {
                projectile.position = gatherPosition;
                projectile.velocity *= 0f;
                projectile.netUpdate = true;
            }


            float distanceFromTarget = 700f;
            Vector2 targetCenter = projectile.Center;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, projectile.Center);
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, projectile.Center);
                        bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
                    }
                }

            }

            projectile.friendly = foundTarget;


            float speed = 8f;
            float inertia = 20;

            if (foundTarget)
            {
                Vector2 direction = targetCenter - projectile.Center;
                direction.Normalize();
                direction *= speed;
                projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
            }
            else
            {
                if (distanceToGatherPosition > 600f)
                {
                    speed = 10f;
                    inertia = 30f;
                }
                else
                {
                    speed = 6f;
                    inertia = 60f;
                }

                if (distanceToGatherPosition > 10f)
                {
                    vectorToGatherPosition.Normalize();
                    vectorToGatherPosition *= speed;
                    projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToGatherPosition) / inertia;
                }

            }

        }


        public void DamageMinion(int damage)
        {
            Main.NewText(projectile.whoAmI);

            minionLife -= damage;
            minionImmune = true;
            minionImmuneTime = 30;
            if (minionLife > 0)
            {
                Main.PlaySound(SoundID.NPCHit, projectile.position);
            }
            else if (minionLife <= 0)
            {
                Main.PlaySound(SoundID.NPCDeath1, projectile.position);
                projectile.Kill();
            }
            projectile.netUpdate = true;
        }

    }

    
}
