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
    internal class JungleHood : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Hood");
            Tooltip.SetDefault("Increases your max number of minions by 1");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.defense = 3;
            item.rare = ItemRarityID.Orange;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.JungleShirt || body.type == ItemID.AncientCobaltBreastplate && legs.type == ItemID.JunglePants || legs.type == ItemID.AncientCobaltLeggings;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "6% reduced mana usage\n15% increased minion damage";
            player.minionDamage *= 1.1f;
            player.manaCost *= 0.94f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient((ItemID.JungleSpores), 4);
            recipe.AddIngredient((ItemID.Stinger), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
