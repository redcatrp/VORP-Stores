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

            MenuController.AddSubmenu(mainMenu, BuyMenu.GetMenu());

            MenuItem subMenuBuyBtn = new MenuItem(GetConfig.Langs["BuyButton"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(subMenuBuyBtn);
            MenuController.BindMenuItem(mainMenu, BuyMenu.GetMenu(), subMenuBuyBtn);

            mainMenu.OnMenuClose += (_menu) =>
            {
                StoreActions.ExitBuyStore();
            };

        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return mainMenu;
        }

    }
}
