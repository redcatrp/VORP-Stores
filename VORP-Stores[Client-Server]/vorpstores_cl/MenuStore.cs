using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using MenuAPI;

namespace vorpstores_cl
{
    class MenuStore : BaseScript
    {
        public static Menu MainMenu = new Menu("ASD", GetConfig.Langs["DescMainMenu"]);
        private static void SetupMenu()
        {
            //MenuController.EnableMenuToggleKeyOnController = false;
            //MenuController.MenuToggleKey = (Control)0;
;
            MainMenu.AddMenuItem(new MenuItem(GetConfig.Langs["BuyButton"], " ")
            {
                Enabled = true,
            });
            MainMenu.AddMenuItem(new MenuItem(GetConfig.Langs["SellButton"], " ")
            {
                Enabled = false,
            });
        }
        public static Menu LoadMenu()
        {
            SetupMenu();
            return MainMenu;
        }
    }
}
