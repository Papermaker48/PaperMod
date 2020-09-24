using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using PaperMod.Necro;
using PaperMod.UI;
using static Terraria.ModLoader.ModContent;


namespace PaperMod
{
	public class PaperMod : Mod
	{

        private UserInterface _necroStatBarsUserInterface;
        internal NecroStatBars NecroStatBars;
        private UserInterface _necroMinionBarsUserInterface;
        internal NecroMinionBars NecroMinionBars;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                NecroStatBars = new NecroStatBars();
                _necroStatBarsUserInterface = new UserInterface();
                _necroStatBarsUserInterface.SetState(NecroStatBars);

                NecroMinionBars = new NecroMinionBars();
                _necroMinionBarsUserInterface = new UserInterface();
                _necroMinionBarsUserInterface.SetState(NecroMinionBars);


            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _necroStatBarsUserInterface?.Update(gameTime);
            _necroMinionBarsUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int statBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (statBarsIndex != -1)
            {
                layers.Insert(statBarsIndex, new LegacyGameInterfaceLayer(
                    "PaperMod: Necro Stat Bars",
                    delegate
                    {
                        _necroStatBarsUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                    );
            }

            int minionBarsIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (minionBarsIndex != -1)
            {
                layers.Insert(minionBarsIndex, new LegacyGameInterfaceLayer(
                    "PaperMod: Necro Stat Bars",
                    delegate
                    {
                        _necroMinionBarsUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                    );
            }
        }


        /*internal NecroStatBars necroStatBars;
        public UserInterface necroStatBarsInterface;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                necroStatBars = new NecroStatBars();
                necroStatBars.Initialize();
                necroStatBarsInterface = new UserInterface();
                necroStatBarsInterface.SetState(necroStatBars);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if(!Main.gameMenu && NecroStatBars.visible)
            {
                necroStatBarsInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Add(new LegacyGameInterfaceLayer("PaperMod: Necro Stat Bars", DrawNecroStatBars, InterfaceScaleType.UI));
        }

        private bool DrawNecroStatBars()
        {
            if (!Main.gameMenu && NecroStatBars.visible)
            {
                necroStatBarsInterface.Draw(Main.spriteBatch, new GameTime());
            }
            return true;
        }*/


    }
}