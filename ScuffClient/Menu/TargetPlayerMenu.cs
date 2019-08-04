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
            logoutColor.normalColor = new Color(1f, 0f, 1f, 0.5f);
            logoutColor.highlightedColor = new Color(1f, 0f, 1f, 1f);
            logoutColor.pressedColor = new Color(1f, 0f, 1f);
            logoutButton.SetActive(true);
            logoutButton.GetComponentInChildren<Text>().text = "Logout";
            logoutButton.GetComponent<Button>().colors = logoutColor;
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
            portalColor.normalColor = new Color(0f, 1f, 0f, 0.5f);
            portalColor.highlightedColor = new Color(0f, 1f, 0f, 1f);
            portalColor.pressedColor = new Color(0f, 1f, 0f);
            portalButton.SetActive(true);
            portalButton.GetComponentInChildren<Text>().text = "Portal";
            portalButton.GetComponent<Button>().colors = portalColor;
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
            teleportColor.normalColor = new Color(0f, 1f, 1f, 0.5f);
            teleportColor.highlightedColor = new Color(0f, 1f, 1f, 1f);
            teleportColor.pressedColor = new Color(0f, 1f, 1f);
            teleportButton.SetActive(true);
            teleportButton.GetComponentInChildren<Text>().text = "Teleport";
            teleportButton.GetComponent<Button>().colors = teleportColor;
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
                publicColor.normalColor = new Color(0f, 1f, 0f, 0.5f);
                publicColor.highlightedColor = new Color(0f, 1f, 0f, 1f);
                publicColor.pressedColor = new Color(0f, 1f, 1f);
                cloneButton.SetActive(true);
                cloneButton.GetComponentInChildren<Text>().text = "Yoink";
                cloneButton.GetComponent<Button>().colors = publicColor;
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
