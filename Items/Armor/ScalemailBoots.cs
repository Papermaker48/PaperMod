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
    [AutoloadEquip(EquipType.Legs)]
    internal class ScalemailBoots : ModItem
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Enchanted Scalemail Boots");
            Tooltip.SetDefault("'Tailored to be both stylish and durable'\n10% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.defense = 4;
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient((ItemID.Silk), 10);
            recipe.AddIngredient((ItemID.ShadowScale), 12);
            recipe.AddIngredient((ItemID.Emerald), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
