using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMod.Necro;
using static Terraria.ModLoader.ModContent;

namespace PaperMod.Necro.Scythes
{
	public class ScytheDemonite : NecroDamageItem
	{
		public override void SetStaticDefaults() 
		{
            DisplayName.SetDefault("Demonite Scythe");
            Tooltip.SetDefault("Right click to throw the scythe forwards\nThrown scythe does half damage after the first hit");
		}

		public override void SafeSetDefaults() 
		{
            lifeEssenceCost = 10;
			item.damage = 18;
			item.width = 56;
			item.height = 54;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 1;
			item.knockBack = 3f;
			item.value = 27000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("ScytheDemoniteSlash");
            item.autoReuse = false;
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;

            item.shootSpeed = 5f;
        }
        

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }


        public override bool CanUseItem(Player player)
        {
            float mouseDistance = Vector2.Distance(player.Center, Main.MouseWorld);

            if (player.altFunctionUse == 2)
            {
                if (mouseDistance >= 150)
                {
                    lifeEssenceCost = 20;
                    item.useTime = 35;
                    item.useAnimation = 35;
                    item.UseSound = (SoundID.Item1);
                    item.useStyle = 1;
                    item.shoot = mod.ProjectileType("ScytheDemoniteThrown");
                }
                else
                {
                    lifeEssenceCost = 0;
                    item.useTime = 5;
                    item.useAnimation = 5;
                    item.UseSound = (null);
                    item.useStyle = 5;
                    item.shoot = ProjectileID.None;
                }
            }
            else
            {
                lifeEssenceCost = 0;
                item.useTime = 35;
                item.useAnimation = 35;
                item.UseSound = (SoundID.Item71);
                item.useStyle = 5;
                item.shoot = mod.ProjectileType("ScytheDemoniteSlash");
            }
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 angle = player.DirectionTo(Main.MouseWorld) * 10;
                float vertAngle = angle.Y;
                float mouseDistance = Vector2.Distance(player.Center, Main.MouseWorld);
                
                //clamp verticle throwing angle
                if (vertAngle >= 4)
                {
                    vertAngle = 3.9f;
                }
                else if (vertAngle <= -4)
                {
                    vertAngle = -3.9f;
                }

                //cap maximum throwing range
                float maxScytheRange = 350 * NecroDamagePlayer.ModPlayer(player).necroScytheRangeMult;

                if (mouseDistance >= maxScytheRange)
                {
                    mouseDistance = maxScytheRange;
                }

                //lower speed based on distance
                if (mouseDistance >= 400)
                {
                    mouseDistance /= 35;
                }
                else if (mouseDistance >= 200)
                {
                    mouseDistance /= 30;
                }
                else
                {

                    mouseDistance /= 25;
                }

                //final throw values
                if (player.direction == 1)
                {
                    speedX = mouseDistance;
                    speedY = vertAngle;
                    return true;
                }
                else
                {
                    speedX = -mouseDistance;
                    speedY = vertAngle;
                    return true;
                }

            }
            else
            {
                position = player.Center;
                speedY = 0;
                speedX = 0;
                return true;               
            }

        }

        public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


	}
}