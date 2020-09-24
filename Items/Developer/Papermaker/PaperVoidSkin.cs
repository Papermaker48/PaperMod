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
    class PaperVoidSkin : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eldritch Voidtouch");
            Tooltip.SetDefault("Great for impersonating random people you probably don't know!\nIt gives off an otherworldly aura...");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 4));
        }

        public override void SetDefaults()
        {
            item.accessory = true;
            item.vanity = true;
            item.rare = ItemRarityID.Cyan;
        }


    }
}
