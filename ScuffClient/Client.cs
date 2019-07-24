using System;
using VRLoader.Attributes;
using VRLoader.Modules;
using ScuffClient.Patches;
using ScuffClient.Menu;
using UnityEngine.UI;
using UnityEngine;

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

        }
    }
}
