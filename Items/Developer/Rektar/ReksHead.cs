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
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;

namespace PaperMod.Items.Developer.Rektar
{
    [AutoloadEquip(EquipType.Head)]
    class ReksHead : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rek's Head");
            Tooltip.SetDefault("Great for impersonating random people you probably don't know!\nFor some reason, wearing this reminds you of lasagna");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 22;
            item.vanity = true;
            item.rare = ItemRarityID.Cyan;
        }

    }
}
