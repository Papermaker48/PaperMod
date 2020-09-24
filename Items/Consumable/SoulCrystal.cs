using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using PaperMod.Necro;
using static Terraria.ModLoader.ModContent;

namespace PaperMod.Items.Consumable
{
    internal class SoulCrystal : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.LifeCrystal;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Permanently increases maximum souls by 1\nCan only be used once");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeCrystal);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<NecroDamagePlayer>().soulsMax == 1 && player.GetModPlayer<PaperPlayer>().soulCrystalsUsed < PaperPlayer.maxSoulCrystalsUsed;
        }

        public override bool UseItem(Player player)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<NecroDamagePlayer>();
            modPlayer.soulsMax2 += 1;
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(2, true);
            }
            player.GetModPlayer<PaperPlayer>().soulCrystalsUsed += 1;
            return true;
        }

    }
}
