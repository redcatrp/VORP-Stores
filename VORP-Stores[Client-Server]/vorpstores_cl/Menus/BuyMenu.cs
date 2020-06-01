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
    class BuyMenu : BaseScript
    {
        private static Menu buyMenu = new Menu(GetConfig.Langs["BuyButton"], GetConfig.Langs["BuyMenuDesc"]);
        private static Menu buyMenuConfirm = new Menu("", GetConfig.Langs["BuyMenuConfirmDesc"]);

        private static int indexItem;
        private static int quantityItem;

        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(buyMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)0;

            MenuController.AddSubmenu(buyMenu, buyMenuConfirm);

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
                MenuController.BindMenuItem(buyMenu, buyMenuConfirm, _itemToBuy);
            }

            MenuItem subMenuConfirmBuyBtnYes = new MenuItem("", " ")
            {
                RightIcon = MenuItem.Icon.TICK
            };
            MenuItem subMenuConfirmBuyBtnNo = new MenuItem(GetConfig.Langs["BuyConfirmButtonNo"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_LEFT
            };

            buyMenuConfirm.AddMenuItem(subMenuConfirmBuyBtnYes);
            buyMenuConfirm.AddMenuItem(subMenuConfirmBuyBtnNo);

            buyMenu.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                // Code in here would get executed whenever a list item is pressed.
                //indexItem = _itemIndex;
                //quantityItem = _listIndex + 1;
                buyMenuConfirm.MenuTitle = GetConfig.ItemsFromDB[GetConfig.Config["Items"][_itemIndex]["Name"].ToString()]["label"].ToString();
                subMenuConfirmBuyBtnYes.Label = string.Format(GetConfig.Langs["BuyConfirmButtonYes"], (_listIndex + 1).ToString(), GetConfig.ItemsFromDB[GetConfig.Config["Items"][_itemIndex]["Name"].ToString()]["label"].ToString());
            };

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

            //buyMenuConfirm.OnItemSelect += (_menu, _item, _index) =>
            //{
            //    if(_index == 0)
            //    {
            //        TriggerServerEvent("vorpstores:buyItems", GetConfig.Config["Items"][indexItem]["Name"].ToString(), quantityItem, GetConfig.Config["Items"][indexItem]["BuyPrice"]);
            //        buyMenu.OpenMenu();
            //    }
            //    else
            //    {
            //        buyMenu.OpenMenu();
            //    }
            //};

        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return buyMenu;
        }
    }
}
