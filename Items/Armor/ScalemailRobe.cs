using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Papermod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    internal class ScalemailRobe : ModItem
    {

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Enchanted Scalemail Robe");
            Tooltip.SetDefault("'Enchanted emerald lines this robe, increasing your summoning power'\nIncreases minion damage by 10%");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 28;
            item.defense = 5;
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage *= 1.1f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ItemType<ScalemailHood>() && legs.type == ItemType<ScalemailBoots>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "3 defense";
            player.statDefense += 3;
        }

        public override void ArmorSetShadows (Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient((ItemID.Silk), 16);
            recipe.AddIngredient((ItemID.ShadowScale), 16);
            recipe.AddIngredient((ItemID.Emerald), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
