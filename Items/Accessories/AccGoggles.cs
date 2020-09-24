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
    [AutoloadEquip(EquipType.Face)]
    class AccGoggles : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goggles");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.White;
            item.accessory = true;
            item.value = 200;
        }


    }
}

