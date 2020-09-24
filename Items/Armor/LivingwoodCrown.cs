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
    internal class LivingwoodCrown : ModItem
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Living Wood Crown");
            Tooltip.SetDefault("Increases your max number of minions by 1\nCan substitute a wood helmet");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.defense = 2;
            item.rare = ItemRarityID.White;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.WoodBreastplate && legs.type == ItemID.WoodGreaves;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases minion damage by 5%";
            player.minionDamage *= 1.05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient((ItemID.Wood), 25);
            recipe.AddTile(TileID.LivingLoom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
