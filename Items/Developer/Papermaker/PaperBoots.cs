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

namespace PaperMod.Items.Developer.Papermaker
{
    [AutoloadEquip(EquipType.Legs)]
    class PaperBoots : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Boots");
            Tooltip.SetDefault("Great for impersonating random people you probably don't know!\nIt gives off an otherworldly aura...");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.vanity = true;
            item.rare = ItemRarityID.Cyan;
        }

    }
}
