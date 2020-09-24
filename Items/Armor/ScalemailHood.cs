using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Papermod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class ScalemailHood : ModItem
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Enchanted Scalemail Hood");
            Tooltip.SetDefault("'A layer of shadow scales woven into the hood protects you from harm'\nIncreases your max number of minions by 1");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.defense = 5;
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient((ItemID.Silk), 8);
            recipe.AddIngredient((ItemID.ShadowScale), 10);
            recipe.AddIngredient((ItemID.Emerald), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
