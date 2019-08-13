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
    public class TargetPlayerMenu : MonoBehaviour
    {
        private MethodInfo m_QuickMenuInstance;
        private FieldInfo f_SelectedUser;
        private static Color themeColor = new Color(1f, 0f, 1f);
        private QuickMenu menu;
        private Player selectedPlayer;
        private VRCPlayer selectedVRCPlayer;
        private APIUser selectedAPIUser;
        private GameObject portalButton;
        private GameObject logoutButton;
        private GameObject teleportButton;
        private GameObject cloneButton;

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
                    LoadPortalButton();
                    LoadCloneButton();
                    LoadLogoutButton();
                    LoadTeleportButton();
                }
            }
            catch { }
        }

        public TargetPlayerMenu()
        {
            m_QuickMenuInstance = typeof(QuickMenu).GetProperties().First((PropertyInfo p) => p.PropertyType == typeof(QuickMenu)).GetGetMethod();
            f_SelectedUser = typeof(QuickMenu).GetFields(BindingFlags.Instance | BindingFlags.NonPublic).First((FieldInfo f) => f.FieldType == typeof(APIUser));
        }
        
        public void LoadLogoutButton()
        {
            logoutButton = GameObject.Find("UserInteractMenu").transform.Find("ForceLogoutButton").gameObject;
            ColorBlock logoutColor = portalButton.GetComponent<Button>().colors;
            logoutColor.normalColor = themeColor + new Color(0f, 0f, 0f, 0.5f);
            logoutColor.highlightedColor = themeColor + new Color(0f, 0f, 0f, 1f);
            logoutColor.pressedColor = themeColor;
            logoutButton.SetActive(true);
            logoutButton.GetComponentInChildren<Text>().text = "Logout";
            logoutButton.GetComponent<Button>().colors = logoutColor;
            //disable persistent button
            logoutButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            logoutButton.GetComponent<Button>().onClick.RemoveAllListeners();
            logoutButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Exploits.TriggerIndexOutOfRange(selectedPlayer);
            });
        }
        
        public void LoadPortalButton()
        {
            portalButton = GameObject.Find("UserInteractMenu").transform.Find("BanButton").gameObject;
            ColorBlock portalColor = portalButton.GetComponent<Button>().colors;
            portalColor.normalColor = themeColor + new Color(0f, 0f, 0f, 0.5f);
            portalColor.highlightedColor = themeColor + new Color(0f, 0f, 0f, 1f);
            portalColor.pressedColor = themeColor;
            portalButton.SetActive(true);
            portalButton.GetComponentInChildren<Text>().text = "Portal";
            portalButton.GetComponent<Button>().colors = portalColor;
            //disable persistent button
            portalButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            portalButton.GetComponent<Button>().onClick.RemoveAllListeners();
            portalButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Exploits.DropPortal(selectedPlayer);
            });
        }

        public void LoadTeleportButton()
        {
            teleportButton = GameObject.Find("UserInteractMenu").transform.Find("KickButton").gameObject;
            ColorBlock teleportColor = teleportButton.GetComponent<Button>().colors;
            teleportColor.normalColor = themeColor + new Color(0f, 0f, 0f, 0.5f);
            teleportColor.highlightedColor = themeColor + new Color(0f, 0f, 0f, 0.5f);
            teleportColor.pressedColor = themeColor;
            teleportButton.SetActive(true);
            teleportButton.GetComponentInChildren<Text>().text = "Teleport";
            teleportButton.GetComponent<Button>().colors = teleportColor;
            //disable persistent button
            teleportButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            teleportButton.GetComponent<Button>().onClick.RemoveAllListeners();
            teleportButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Exploits.Teleport(selectedPlayer);
            });
        }

        public void LoadCloneButton()
        {
            if (PlayerReflections.GetApiAvatar(selectedPlayer).releaseStatus == "public")
            {
                cloneButton = GameObject.Find("UserInteractMenu").transform.Find("WarnButton").gameObject;
                ColorBlock publicColor = portalButton.GetComponent<Button>().colors;
                publicColor.normalColor = themeColor + new Color(0f, 0f, 0f, 0.5f);
                publicColor.highlightedColor = themeColor + new Color(0f, 0f, 0f, 1f);
                publicColor.pressedColor = themeColor;
                cloneButton.SetActive(true);
                cloneButton.GetComponentInChildren<Text>().text = "Yoink";
                cloneButton.GetComponent<Button>().colors = publicColor;
                //disable persistent button
                cloneButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                cloneButton.GetComponent<Button>().onClick.RemoveAllListeners();
                cloneButton.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Exploits.LoadAvatarFromId(PlayerReflections.GetApiAvatar(selectedPlayer).id);
                });
                cloneButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                cloneButton = GameObject.Find("UserInteractMenu").transform.Find("WarnButton").gameObject;
                cloneButton.SetActive(true);
                cloneButton.GetComponentInChildren<Text>().text = "PRIVATE";
                cloneButton.GetComponent<Button>().onClick.RemoveAllListeners();
                cloneButton.GetComponent<Button>().interactable = false;
            }
        }
    }
}
