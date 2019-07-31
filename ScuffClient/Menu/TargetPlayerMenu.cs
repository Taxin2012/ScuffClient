using UnityEngine;
using System;
using System.Reflection;
using VRC.Core;
using VRC;
using System.Linq;
using UnityEngine.UI;
using ScuffClient.Reflections;
using ScuffClient.Misc;

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
        private GameObject portalButton;
        private GameObject logoutButton;

        public void Update()
        {
            try
            {
                menu = (QuickMenu)m_QuickMenuInstance.Invoke(null, null);
                selectedAPIUser = (APIUser)f_SelectedUser.GetValue(menu);
                if (selectedAPIUser != null)
                {
                    selectedPlayer = PlayerManager.GetPlayer(selectedAPIUser.id);
                    selectedVRCPlayer = selectedPlayer.vrcPlayer;

                    #region PortalButton
                    portalButton = GameObject.Find("UserInteractMenu").transform.Find("BanButton").gameObject;
                    ColorBlock colors = portalButton.GetComponent<Button>().colors;
                    colors.normalColor = new Color(0f, 1f, 0f, 0.5f);
                    colors.highlightedColor = new Color(0f, 1f, 0f, 1f);
                    colors.pressedColor = new Color(0f, 1f, 0f);
                    portalButton.SetActive(true);
                    portalButton.GetComponentInChildren<Text>().text = "Portal";
                    portalButton.GetComponent<Button>().colors = colors;
                    portalButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    portalButton.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        Exploits.DropPortal(selectedPlayer);
                    });
                    #endregion

                    #region LogoutButton
                    logoutButton = GameObject.Find("UserInterActMenu").transform.Find("BanButton").gameObject;
                    ColorBlock colors2 = portalButton.GetComponent<Button>().colors;
                    colors.normalColor = new Color(1f, 0f, 0.5f, 0.5f);
                    colors.highlightedColor = new Color(1f, 0f, 0.5f, 1f);
                    colors.pressedColor = new Color(1f, 0f, 1f);
                    portalButton.SetActive(true);
                    portalButton.GetComponentInChildren<Text>().text = "Logout";
                    portalButton.GetComponent<Button>().colors = colors;
                    portalButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    portalButton.GetComponent<Button>().onClick.AddListener(delegate ()
                    {
                        Exploits.TriggerIndexOutOfRange(selectedPlayer);
                    });
                    #endregion
                }
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
