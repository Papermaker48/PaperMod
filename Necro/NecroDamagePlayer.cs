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
using static Terraria.ModLoader.ModContent;

namespace PaperMod.Necro
{
    public class NecroDamagePlayer : ModPlayer
    {

        public static NecroDamagePlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<NecroDamagePlayer>();
        }

        public float necroDamageAdd;
        public float necroDamageMult = 1f;
        public float necroKnockback;
        public int necroCrit;
        public float necroScytheRangeMult = 1f;

        public int lifeEssenceCurrent;
        public const int DefaultLifeEssenceMax = 100;
        public int lifeEssenceMax;
        public int lifeEssenceMax2;
        public float lifeEssenceRegenRate;
        public float lifeEssenceRegenTimer = 0;
        public static readonly Color HealLifeEssence = new Color(180, 20, 35);

        public int soulsCurrent;
        public const int DefaultSoulsMax = 1;
        public int soulsMax;
        public int soulsMax2;
        public static readonly Color HealSoul = new Color(80, 200, 240);

        public override void Initialize()
        {
            lifeEssenceMax = DefaultLifeEssenceMax;
            soulsMax = DefaultSoulsMax;
        }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            float lifeQuotient = (float)player.statLife / player.statLifeMax2;
            float lifeEssenceMaxScaled = lifeEssenceMax2 / 100;

            necroDamageAdd = 0f;
            necroDamageMult = 1f;
            necroKnockback = 0f;
            necroCrit = 0;
            necroScytheRangeMult = 1f;

            lifeEssenceRegenRate = 1f * lifeEssenceMaxScaled / lifeQuotient;

            lifeEssenceMax2 = lifeEssenceMax;

            soulsMax2 = soulsMax;
        }

        public override void PostUpdateMiscEffects()
        {
            UpdateResource();
        }

        private void UpdateResource()
        {
            lifeEssenceRegenTimer++;

            if (lifeEssenceRegenTimer > 10 * lifeEssenceRegenRate)
            {
                lifeEssenceCurrent += 1;
                lifeEssenceRegenTimer = 0;
            }

            lifeEssenceCurrent = Utils.Clamp(lifeEssenceCurrent, 0, lifeEssenceMax2);
        }

    }
}
