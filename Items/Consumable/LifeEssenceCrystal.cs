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
    internal class LifeEssenceCrystal : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.LifeCrystal;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Permanently increases maximum life essence by 10\nCan be used 10 times");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeCrystal);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<PaperPlayer>().lifeEssenceCrystalsUsed < PaperPlayer.maxLifeEssenceCrystalsUsed;
        }

        public override bool UseItem(Player player)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<NecroDamagePlayer>();
            modPlayer.lifeEssenceMax2 += 1;
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(2, true);
            }
            player.GetModPlayer<PaperPlayer>().lifeEssenceCrystalsUsed += 1;
            return true;
        }

    }
}
