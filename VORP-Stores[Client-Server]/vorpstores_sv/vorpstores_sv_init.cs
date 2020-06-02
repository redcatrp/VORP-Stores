using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpstores_sv
{
    public class vorpstores_sv_init : BaseScript
    {
        public vorpstores_sv_init()
        {
            EventHandlers["vorpstores:buyItems"] += new Action<Player, string, int, double>(buyItems);
            EventHandlers["vorpstores:sellItems"] += new Action<Player, double, string>(sellItems);
        }
        private void buyItems([FromSource]Player source, string name, int quantity, double cost)
        {
            int _source = int.Parse(source.Handle);

            string sid = "steam:" + source.Identifiers["steam"];

            TriggerEvent("vorp:getCharacter", _source, new Action<dynamic>((user) =>
            {
                double money = user.money;

                if ((cost * quantity) <= money)
                {
                    Debug.WriteLine("Entra " + (cost * quantity).ToString()); 
                    TriggerEvent("vorp:removeMoney", _source, 0, (double)(cost * quantity));
                }
                else
                {
                    source.TriggerEvent("vorp:Tip", LoadConfig.Langs["NoMoney"], 4000);
                }

            }));
        }

        private void sellItems([FromSource]Player source, double totalCost, string jsonCloths)
        {

        }

    }
}
