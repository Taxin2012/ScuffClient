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
            if (logoutButton == null)
            {
                GameObject original = GameObject.Find("UserInteractMenu").transform.Find("ForceLogoutButton").gameObject;
                Transform parent = GameObject.Find("UserInteractMenu").transform;
                //instantiate the original button to new object
                logoutButton = GameObject.Instantiate(original, original.transform.position, default(Quaternion), parent);
                logoutButton.GetComponent<Button>().colors = Functions.SetThemeColor(logoutButton.GetComponent<Button>().colors);
                logoutButton.SetActive(true);
                logoutButton.GetComponentInChildren<Text>().text = "Logout";
                //disable persistent button
                logoutButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                logoutButton.GetComponent<Button>().onClick.RemoveAllListeners();
                logoutButton.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Exploits.FakeLogout(selectedPlayer);
                });
            }
        }
        
        public void LoadPortalButton()
        {
            if (portalButton == null)
            {
                GameObject original = GameObject.Find("UserInteractMenu").transform.Find("ForceLogoutButton").gameObject;
                Transform parent = GameObject.Find("UserInteractMenu").transform;
                //instantiate the original button to new object
                portalButton = GameObject.Instantiate(original, original.transform.position, default(Quaternion), parent);
                portalButton.GetComponent<Button>().colors = Functions.SetThemeColor(portalButton.GetComponent<Button>().colors);
                portalButton.SetActive(true);
                portalButton.GetComponentInChildren<Text>().text = "Portal";
                //disable persistent button
                portalButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                portalButton.GetComponent<Button>().onClick.RemoveAllListeners();
                portalButton.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Exploits.DropPortal(selectedPlayer);
                });
                portalButton.transform.localPosition = portalButton.transform.localPosition + new Vector3(420, 0);
            }
        }

        public void LoadTeleportButton()
        {
            if (teleportButton == null)
            {
                GameObject original = GameObject.Find("UserInteractMenu").transform.Find("ForceLogoutButton").gameObject;
                Transform parent = GameObject.Find("UserInteractMenu").transform;
                //instantiate the original button to new object
                teleportButton = GameObject.Instantiate(original, original.transform.position, default(Quaternion), parent);
                teleportButton.GetComponent<Button>().colors = Functions.SetThemeColor(teleportButton.GetComponent<Button>().colors);
                teleportButton.SetActive(true);
                teleportButton.GetComponentInChildren<Text>().text = "Teleport";
                //disable persistent button
                teleportButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                teleportButton.GetComponent<Button>().onClick.RemoveAllListeners();
                teleportButton.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Exploits.Teleport(selectedPlayer);
                });
                teleportButton.transform.localPosition = teleportButton.transform.localPosition + new Vector3(-420, 0);
            }
        }

        public void LoadCloneButton()
        {
            void CreateButtons(GameObject go = null)
            {
                Transform parent = GameObject.Find("UserInteractMenu").transform;
                if (PlayerReflections.GetApiAvatar(selectedPlayer).releaseStatus == "public")
                {
                    if (go != null)
                        cloneButton = GameObject.Instantiate(go, go.transform.position, default(Quaternion), parent);

                    cloneButton.GetComponent<Button>().colors = Functions.SetThemeColor(cloneButton.GetComponent<Button>().colors);
                    cloneButton.SetActive(true);
                    cloneButton.GetComponentInChildren<Text>().text = "Yoink";
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
                    if(go != null)
                        cloneButton = GameObject.Instantiate(go, go.transform.position, default(Quaternion), parent);

                    cloneButton.SetActive(true);
                    cloneButton.GetComponentInChildren<Text>().text = "PRIVATE";
                    cloneButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    cloneButton.GetComponent<Button>().interactable = false;
                }
            }
            if (cloneButton == null)
            {
                CreateButtons(GameObject.Find("UserInteractMenu").transform.Find("ForceLogoutButton").gameObject);
                cloneButton.transform.localPosition = cloneButton.transform.localPosition + new Vector3(840, 0);
            }
            if(cloneButton != null)
                CreateButtons();
        }
    }
}
