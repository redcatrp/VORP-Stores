using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace vorpstores_cl
{
    public class StoreActions : BaseScript
    {
        private static int CamStore;
        private static int ObjectStore;
        public static async Task EnterBuyStore(int storeId)
        {
            float Camerax = float.Parse(GetConfig.Config["Stores"][storeId]["CameraMain"][0].ToString());
            float Cameray = float.Parse(GetConfig.Config["Stores"][storeId]["CameraMain"][1].ToString());
            float Cameraz = float.Parse(GetConfig.Config["Stores"][storeId]["CameraMain"][2].ToString());
            float CameraRotx = float.Parse(GetConfig.Config["Stores"][storeId]["CameraMain"][3].ToString());
            float CameraRoty = float.Parse(GetConfig.Config["Stores"][storeId]["CameraMain"][4].ToString());
            float CameraRotz = float.Parse(GetConfig.Config["Stores"][storeId]["CameraMain"][5].ToString());

            TriggerEvent("vorp:setInstancePlayer", true);
            NetworkSetInSpectatorMode(true, PlayerPedId());
            FreezeEntityPosition(PlayerPedId(), true);
            SetEntityVisible(PlayerPedId(), false);

            CamStore = CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", Camerax, Cameray, Cameraz, CameraRotx, CameraRoty, CameraRotz, 50.00f, false, 0);
            SetCamActive(CamStore, true);
            RenderScriptCams(true, true, 500, true, true, 0);
            CreateObjectOnTable(storeId);

            Menu MyMenu = MenuStore.LoadMenu();

            MyMenu.OpenMenu();

            await Delay(10000);
            ExitBuyStore();
        }

        public static async Task CreateObjectOnTable(int storeId)
        {
            float objectX = float.Parse(GetConfig.Config["Stores"][storeId]["SpawnObjectStore"][0].ToString());
            float objectY = float.Parse(GetConfig.Config["Stores"][storeId]["SpawnObjectStore"][1].ToString());
            float objectZ = float.Parse(GetConfig.Config["Stores"][storeId]["SpawnObjectStore"][2].ToString());
            float objectH = float.Parse(GetConfig.Config["Stores"][storeId]["SpawnObjectStore"][3].ToString());
            uint idObject = (uint)GetHashKey("p_banana01x");
            await vorpstores_init.LoadModel(idObject);
            ObjectStore = CreateObject(idObject, objectX, objectY, objectZ, false, true, true, true, true);
        }

        public static async Task ExitBuyStore()
        {
            TriggerEvent("vorp:setInstancePlayer", false);
            NetworkSetInSpectatorMode(false, PlayerPedId());
            FreezeEntityPosition(PlayerPedId(), false);
            SetEntityVisible(PlayerPedId(), true);
            SetCamActive(CamStore, false);
            RenderScriptCams(false, true, 1000, true, true, 0);
            DestroyCam(CamStore, true);

            DeleteObject(ref ObjectStore);
        }

    }
}
