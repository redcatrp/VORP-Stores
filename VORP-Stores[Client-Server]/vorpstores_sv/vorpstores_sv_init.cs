using CitizenFX.Core;
using System;

namespace vorpstores_sv
{
    public class vorpstores_sv_init : BaseScript
    {
        public vorpstores_sv_init()
        {
            EventHandlers["vorpstores:buyItems"] += new Action<Player, string, int, double>(buyItems);
            EventHandlers["vorpstores:sellItems"] += new Action<Player, string, int, double>(sellItems);
        }
        private void buyItems([FromSource]Player source, string name, int quantity, double cost)
        {
            int _source = int.Parse(source.Handle);

            string sid = "steam:" + source.Identifiers["steam"];

           
            TriggerEvent("vorpCore:getItemCount", _source, new Action<dynamic>((itemcount) =>
            {
                int count = itemcount;
                int limit = int.Parse(LoadConfig.ItemsFromDB[name]["limit"].ToString());
                int hisLimit = limit - count;
                if (quantity > hisLimit)
                {
                    source.TriggerEvent("vorp:TipRight", string.Format(LoadConfig.Langs["NoMore"], LoadConfig.ItemsFromDB[name]["label"].ToString()), 4000);
                }
                else
                {
                    TriggerEvent("vorpCore:canCarryItems", _source, quantity, new Action<bool>((can) =>
                    {
                        if (!can)
                        {
                            source.TriggerEvent("vorp:TipRight", string.Format(LoadConfig.Langs["NoMore"], LoadConfig.ItemsFromDB[name]["label"].ToString()), 4000);
                        }
                        else
                        {
                            TriggerEvent("vorp:getCharacter", _source, new Action<dynamic>(async (user) =>
                            {
                                await Delay(100);
                                double money = user.money;
                                double totalCost = (double)(cost * quantity);
                                if (totalCost <= money)
                                {
                                    Debug.WriteLine(_source.ToString());
                                    Debug.WriteLine(name);
                                    Debug.WriteLine(quantity.ToString());
                                    Debug.WriteLine(totalCost.ToString());
                                    TriggerEvent("vorp:removeMoney", _source, 0, totalCost);
                                    TriggerEvent("vorpCore:addItem", _source, name, quantity);
                                    //        source.TriggerEvent("vorp:Tip", string.Format(LoadConfig.Langs["Bought"], quantity, LoadConfig.ItemsFromDB[name]["label"].ToString(), totalCost.ToString()), 4000);
                                }
                                else
                                {
                                    source.TriggerEvent("vorp:Tip", LoadConfig.Langs["NoMoney"], 4000);
                                }

                            }));
                        }

                    }));
                }

            }), name);

        }

        private void sellItems([FromSource]Player source, string name, int quantity, double cost)
        {
            int _source = int.Parse(source.Handle);

            string sid = "steam:" + source.Identifiers["steam"];

            double totalCost = (double)(cost * quantity);

            TriggerEvent("vorpCore:getItemCount", _source, new Action<dynamic>((itemcount) =>
            {
                int count = itemcount;
                if (quantity > count)
                {
                    source.TriggerEvent("vorp:Tip", LoadConfig.Langs["NoEnought"], 4000);
                }
                else
                {
                    TriggerEvent("vorp:addMoney", _source, 0, totalCost);
                    TriggerEvent("vorpCore:subItem", _source, name, quantity);
                    source.TriggerEvent("vorp:Tip", string.Format(LoadConfig.Langs["Sold"], quantity, LoadConfig.ItemsFromDB[name]["label"].ToString(), totalCost.ToString()), 4000);
                }

            }), name);

        }

    }
}
