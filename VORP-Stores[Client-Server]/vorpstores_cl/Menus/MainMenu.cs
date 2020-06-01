using MenuAPI;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpstores_cl.Menus
{
    class MainMenu
    {
        private static Menu mainMenu = new Menu("", GetConfig.Langs["DescMainMenu"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(mainMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;
            ;
            mainMenu.AddMenuItem(new MenuItem(GetConfig.Langs["BuyButton"], " ")
            {
                Enabled = true,
            });
            mainMenu.AddMenuItem(new MenuItem(GetConfig.Langs["SellButton"], " ")
            {
                Enabled = false,
            });
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return mainMenu;
        }

    }
}
