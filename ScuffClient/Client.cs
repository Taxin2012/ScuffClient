using System;
using VRLoader.Attributes;
using VRLoader.Modules;
using ScuffClient.Patches;
using ScuffClient.Menu;
using UnityEngine.UI;
using UnityEngine;
using ScuffClient.Misc;

namespace ScuffClient
{
    [ModuleInfo("ScuffClient", "?", "Nova")]
    public class Client : VRModule 
    {
        public void Start()
        {
            base.gameObject.AddComponent<AntiMenu>();
            Patcher.CreatePatches();
        }
        public void Update()
        {
            if (Event.current.shift && Input.GetKeyDown(KeyCode.L))
                Exploits.TriggerIndexOutOfRange(VRC.PlayerManager.GetPlayer(VRC.Core.APIUser.CurrentUser.id));
        }
    }
}
