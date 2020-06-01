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
        private static Menu buyMenu = new Menu(GetConfig.Langs["BuyButton"], GetConfig.Langs["BuyMenuDesc"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(buyMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;

            List<string> quantityList = new List<string>();
            for (var i = 1; i < 101; i++)
            {
                quantityList.Add($"Cantidad #{i}");
            }

            foreach (var item in GetConfig.Config["Items"])
            {
                MenuListItem _itemToBuy = new MenuListItem(GetConfig.ItemsFromDB[item["Name"].ToString()]["label"].ToString() + $" ${item["BuyPrice"]}" , quantityList, 0, "")
                {

                };

                buyMenu.AddMenuItem(_itemToBuy);
            }

            buyMenu.OnIndexChange += (_menu, _oldItem, _newItem, _oldIndex, _newIndex) =>
            {
                // Code in here would get executed whenever the up or down key is pressed and the index of the menu is changed.
                Debug.WriteLine($"OnIndexChange: [{_menu}, {_oldItem}, {_newItem}, {_oldIndex}, {_newIndex}]");
                StoreActions.CreateObjectOnTable(_newIndex);
            };

            buyMenu.OnMenuOpen += (_menu) =>
            {
                StoreActions.CreateObjectOnTable(_menu.CurrentIndex);
            };

        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return buyMenu;
        }
    }
}
