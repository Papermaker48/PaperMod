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
using PaperMod.UI;

namespace PaperMod.Items.Tools
{
    class MechHammer : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Hammer");
            Tooltip.SetDefault("bruh");
        }

        public override void SetDefaults()
        {
            //item.width = 10;
            //item.height = 10;
            item.damage = 6;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 16;
            item.useAnimation = 16;
            item.knockBack = 5;
            item.value = 1;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
        }


        public override bool UseItem(Player player)
        {
            MechHammerUI.visible = true;
            return true;
        }


    }
}
