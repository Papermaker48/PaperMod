using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMod.Items.Accessories
{
    class HellMiningLight : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Lamp");
            Tooltip.SetDefault("'No, not that kind'");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.White;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Lighting.AddLight(player.position, 2.8f, 1.6f, 1f);

            if (Main.rand.NextFloat() < 0.1f)
            {
                Vector2 position = new Vector2(player.position.X, player.position.Y + 10);
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(position, 20, 30, 6, player.velocity.X * 0.5f, Main.LocalPlayer.velocity.Y * 0.5f, 100, new Color(255, 255, 255), 2f)];
                dust.noGravity = true;
            }

        }



    }
}
