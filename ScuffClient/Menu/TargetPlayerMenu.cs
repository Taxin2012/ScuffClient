using UnityEngine;
using System;
using System.Reflection;
using VRC.Core;
using VRC;
using System.Linq;
using UnityEngine.UI;
using ScuffClient.Reflections;

namespace ScuffClient.Menu
{
    public class TargetPlayerMenu
    {
        private MethodInfo m_QuickMenuInstance;
        private FieldInfo f_SelectedUser;
        private QuickMenu menu;
        private Player selectedPlayer;
        private VRCPlayer selectedVRCPlayer;
        private APIUser selectedAPIUser;
        private int PhotonID() => selectedPlayer.GetInstigatorId().GetValueOrDefault();

        public void Update()
        {
            try
            {
                
            }
            catch { }
        }

        public TargetPlayerMenu()
        {
            m_QuickMenuInstance = typeof(QuickMenu).GetProperties().First((PropertyInfo p) => p.PropertyType == typeof(QuickMenu)).GetGetMethod();
            f_SelectedUser = typeof(QuickMenu).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First((FieldInfo f) => f.FieldType == typeof(APIUser));
        }
    }
}
