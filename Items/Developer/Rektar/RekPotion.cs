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

namespace PaperMod.Items.Developer.Rektar
{
    class RekPotion : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Only for those who are boney\nGrants you some of the raw power of a boner, but at what cost?");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.RedPotion);
            item.width = 22;
            item.height = 32;
        }

        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.WellFed, 216000);
            player.AddBuff(BuffID.Endurance, 216000);
            player.AddBuff(BuffID.Wrath, 216000);
            player.AddBuff(BuffID.Regeneration, 216000);
            player.AddBuff(BuffID.Swiftness, 216000);
            player.AddBuff(BuffID.Lifeforce, 216000);
            player.AddBuff(BuffID.Ironskin, 216000);
            player.AddBuff(BuffID.PotionSickness, 216000);
            player.AddBuff(BuffID.ChaosState, 216000);
            return true;
        }

    }
}
