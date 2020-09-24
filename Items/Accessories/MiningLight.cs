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
    class MiningLight : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mining Lamp");
            Tooltip.SetDefault("Provides light when held");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.White;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Lighting.AddLight(player.position, 0.75f, 0.7f, 0.4f);
        }

    }
}
