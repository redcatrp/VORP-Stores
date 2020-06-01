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
    class BuyMenu
    {
        private static Menu buyMenu = new Menu("", GetConfig.Langs["DescMainMenu"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(buyMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;

            buyMenu.AddMenuItem(new MenuItem(GetConfig.Langs["BuyButton"], " ")
            {
                Enabled = true,
            });
            buyMenu.AddMenuItem(new MenuItem(GetConfig.Langs["SellButton"], " ")
            {
                Enabled = false,
            });
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return buyMenu;
        }
    }
}
