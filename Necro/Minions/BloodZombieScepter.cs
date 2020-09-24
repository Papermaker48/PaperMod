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

        public class BloodZombieScepter : NecroDamageItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Blood Zombie Scepter");
                Tooltip.SetDefault("Summons blood zombies under your control");
                ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
                ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
            }

            public override void SafeSetDefaults()
            {
                lifeEssenceCost = 20;
                item.damage = 7;
                item.knockBack = 3f;
                //item.width = 10;
                //item.height = 10;
                item.useTime = 24;
                item.useAnimation = 24;
                item.useStyle = 1;
                item.value = 1;
                item.rare = ItemRarityID.Orange;
                item.UseSound = SoundID.Item44;

                item.noMelee = true;
                item.summon = true;
                item.buffType = BuffType<BloodZombieBuff>();
                item.shoot = ProjectileType<BloodZombieMinion>();
            }

            public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
            {
                player.AddBuff(item.buffType, 2);
                PaperPlayer.necroMinions++;
                
                
                position = player.Center;
                return true;
            }

            public override bool AltFunctionUse(Player player)
            {
                return true;
            }

            public override bool CanUseItem(Player player)
            {
                if (player.altFunctionUse == 2)
                {
                    Main.NewText(PaperPlayer.necroMinions);
                    Main.NewText(BloodZombieMinion.minionProjectileIDs);
                    return true;
                }
                else return true;
            }


        }
    }
