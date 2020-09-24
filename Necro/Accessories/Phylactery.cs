using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMod.Necro;
using static Terraria.ModLoader.ModContent;

namespace PaperMod.Necro.Accessories
{
    class Phylactery : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.PygmyNecklace;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Phylactery");
            Tooltip.SetDefault("Grants infinite Life Essence and Souls\n100% increased necrotic damage\n100% increased scythe range\nYou shouldn't have this.");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Expert;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = NecroDamagePlayer.ModPlayer(player);
            modPlayer.lifeEssenceCurrent = modPlayer.lifeEssenceMax2;
            modPlayer.soulsCurrent = modPlayer.soulsMax2;
            modPlayer.necroDamageMult *= 2f;
            modPlayer.necroScytheRangeMult *= 2f;

            //Main.NewText(modPlayer.lifeEssenceRegenRate);
        }


    }
}
