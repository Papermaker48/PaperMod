using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace PaperMod.Items.Weapons
{
    class VoidArcanum : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Arcanum");
            Tooltip.SetDefault("Conjure an astral rift to annihilate your foes\nAnnihilated enemies leave nothing behind\nDoes not work on bosses");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            item.damage = 0;
            item.width = 56;
            item.height = 54;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 3f;
            item.value = 27000;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.noUseGraphic = false;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("AstralRift");
            item.shootSpeed = 24f;
        }

    }
}
