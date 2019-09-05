using System;
using System.Collections.Generic;
using VRLoader.Attributes;
using VRLoader.Modules;
using ScuffClient.Patches;
using ScuffClient.Menu;
using UnityEngine.UI;
using UnityEngine;
using ScuffClient.Misc;
using VRCSDK2;
using VRC;
using System.Runtime.InteropServices;

namespace ScuffClient
{
    [ModuleInfo("ScuffClient", "?", "Nova")]
    public class Client : VRModule 
    {
        public void Start()
        {
            base.gameObject.AddComponent<AntiMenu>();
            base.gameObject.AddComponent<TargetPlayerMenu>();
            base.gameObject.AddComponent<Cheats>();
            base.gameObject.AddComponent<SpyCamera>();
            base.gameObject.AddComponent<ConfigHandler>();

            Config.CreateConfig();
            Patcher.CreatePatches();
        }
        public void Update()
        {
            if (Event.current.shift && Input.GetKeyDown(KeyCode.L))
                Exploits.TriggerIndexOutOfRange();
        }
    }
}
