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
using static Terraria.ModLoader.ModContent;

namespace PaperMod.Necro
{
    public abstract class NecroDamageItem : ModItem
    {
        public override bool CloneNewInstances => true;
        public int lifeEssenceCost = 0;
        public int soulsCost = 0;

        public virtual void SafeSetDefaults()
        {

        }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.summon = false;
            item.thrown = false;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += NecroDamagePlayer.ModPlayer(player).necroDamageAdd;
            add *= NecroDamagePlayer.ModPlayer(player).necroDamageMult;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback += NecroDamagePlayer.ModPlayer(player).necroKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += NecroDamagePlayer.ModPlayer(player).necroCrit;
        }

        public void GetNecroScytheRange(Player player, ref float range)
        {
            range *= NecroDamagePlayer.ModPlayer(player).necroScytheRangeMult;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();

                tt.text = damageValue + " necrotic " + damageWord;
            }

            if(lifeEssenceCost > 0)
            {
                tooltips.Add(new TooltipLine(mod, "Life Essence Cost", $"Uses {lifeEssenceCost} life essence"));
            }

        }

        public override bool CanUseItem(Player player)
        {
            var necroDamagePlayer = player.GetModPlayer<NecroDamagePlayer>();

            if (necroDamagePlayer.lifeEssenceCurrent >= lifeEssenceCost)
            {
                necroDamagePlayer.lifeEssenceCurrent -= lifeEssenceCost;
                return true;
            }
            else
            {
                int lifeEssenceNegative = necroDamagePlayer.lifeEssenceCurrent - lifeEssenceCost;
                necroDamagePlayer.lifeEssenceCurrent -= lifeEssenceCost;
                CombatText.NewText(player.Hitbox, NecroDamagePlayer.HealLifeEssence, lifeEssenceNegative, false, true);
                Main.PlaySound(SoundID.DD2_DarkMageCastHeal, player.Center);
                player.statLife += lifeEssenceNegative;
                if (player.statLife <= 0)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " drained their lifeforce."), 1, 0, false);
                }
                return true;
            }
        }


    }
}
