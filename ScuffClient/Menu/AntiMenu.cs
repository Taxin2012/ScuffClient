using UnityEngine;
using ScuffClient.Reflections;
using System.Reflection;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using System;
using ScuffClient.Misc;

namespace ScuffClient.Menu
{
    public class AntiMenu : MonoBehaviour
    {
        #region Variables
        private MethodInfo m_QuickMenuInstance;
        private static Color themeColor = new Color(1f, 0f, 1f);
        private QuickMenu menu;
        private GameObject mainMenu;
        private GameObject customMenuButton;
        private GameObject customMenu;
        private GameObject cameraMenu;

        private GameObject antiPortalButton;
        private GameObject flyButton;
        #endregion

        public void Update()
        {
            if (IsGONull(mainMenu))
                mainMenu = menu.transform.Find("ShortcutMenu").gameObject;

            if (IsGONull(customMenuButton))
                CreateCustomMenuButton();

            if (IsGONull(customMenu))
                CreateCustomMenu();

            if (IsGONull(flyButton))
                CreateFlyButton();

            if (IsGONull(antiPortalButton))
                CreateAntiPortalButton();
            
            try
            {
                menu = (QuickMenu)m_QuickMenuInstance.Invoke(null, null);
                ActivateCameraModeButtons();

            }
            catch { }
        }
        public AntiMenu()
        {
            m_QuickMenuInstance = typeof(QuickMenu).GetProperties().First((PropertyInfo p) => p.PropertyType == typeof(QuickMenu)).GetGetMethod();
        }

        private static bool IsGONull(GameObject go)
        {
            return VRCPlayer.Instance != null && go == null;
        }

        private void ActivateCameraModeButtons()
        {
            if (IsGONull(cameraMenu))
                cameraMenu = menu.transform.Find("CameraMenu").gameObject;

            cameraMenu.transform.Find("PhotoMode").gameObject.SetActive(true);
            cameraMenu.transform.Find("VideoMode").gameObject.SetActive(true);
            cameraMenu.transform.Find("DisableCamera").gameObject.SetActive(true);
        }

        private void CreateCustomMenuButton()
        {
            GameObject devTools = mainMenu.transform.Find("DevToolsButton").gameObject;
            customMenuButton = GameObject.Instantiate(devTools, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), mainMenu.transform);

            Text customText = customMenuButton.GetComponentInChildren<Text>();
            Button customButton = customMenuButton.GetComponent<Button>();
            Vector3 pos = devTools.transform.localPosition;
            customMenuButton.transform.localPosition = new Vector3(-pos.x, pos.y, pos.z);

            customMenuButton.SetActive(true);

            customText.text = "ScuffMenu";
            customText.color = Color.yellow;

            ColorBlock colors = customMenuButton.GetComponent<Button>().colors;
            colors.normalColor = themeColor + new Color(0f, 0f, 0f, 0.5f);
            colors.highlightedColor = themeColor + new Color(0f, 0f, 0f, 1f);
            colors.pressedColor = themeColor;
            customButton.colors = colors;

            customMenuButton.GetComponent<UiTooltip>().text = "Open ScuffClient custom menu";
        }

        private void CreateCustomMenu()
        {
            customMenu = menu.transform.Find("ModerationMenu").gameObject;
            foreach (Component comp in customMenu.GetComponentsInChildren<Component>())
            {
                if (comp.GetComponent<Button>() != null && comp.name.ToLower() != "backbutton")
                {
                    comp.gameObject.SetActive(false);
                }

                if (comp.GetComponent<Button>() != null && comp.name.ToLower() == "backbutton")
                {
                    comp.GetComponent<Button>().colors = Functions.SetThemeColor(comp.GetComponent<Button>().colors);
                    comp.GetComponent<Button>().GetComponentInChildren<Text>().text = "return";
                    comp.GetComponent<Button>().GetComponentInChildren<Text>().color = Color.white;
                }
            }
        }

        private void CreateAntiPortalButton()
        {
            GameObject original = customMenu.transform.Find("ClearRoomHub").gameObject;
            antiPortalButton = GameObject.Instantiate(original, original.transform.position, default(Quaternion), customMenu.transform);
            antiPortalButton.SetActive(true);
            antiPortalButton.name = "AntiPortalButton";
            antiPortalButton.GetComponent<UiTooltip>().text = "Toggle Anti-Portal On/Off";
            antiPortalButton.GetComponentInChildren<Text>().text = !Variables.antiPortal ? "Enable\nPortals" : "Disable\nPortals";
            antiPortalButton.GetComponentInChildren<Text>().color = !Variables.antiPortal ? Color.red : Color.green;
            antiPortalButton.GetComponent<Button>().colors = Functions.SetThemeColor(antiPortalButton.GetComponent<Button>().colors);
            //antiPortalButton.transform.localPosition += new Vector3(-840f, -60f);
            antiPortalButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            antiPortalButton.GetComponent<Button>().onClick.RemoveAllListeners();
            antiPortalButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Variables.antiPortal = !Variables.antiPortal;
                antiPortalButton.GetComponentInChildren<Text>().text = !Variables.antiPortal ? "Enable\nPortals" : "Disable\nPortals";
                antiPortalButton.GetComponentInChildren<Text>().color = !Variables.antiPortal ? Color.red : Color.green;
            });
        }

        private void CreateFlyButton()
        {
            GameObject original = customMenu.transform.Find("ClearRoomHub").gameObject;
            flyButton = GameObject.Instantiate(original, original.transform.position, default(Quaternion), customMenu.transform);
            flyButton.SetActive(true);
            flyButton.name = "FlyButton";
            flyButton.GetComponent<UiTooltip>().text = "Toggle Fly On/Off";
            flyButton.GetComponentInChildren<Text>().text = !base.gameObject.GetComponent<Cheats>().isFlying ? "Enable\nFly" : "Disable\nFly";
            flyButton.GetComponentInChildren<Text>().color = !base.gameObject.GetComponent<Cheats>().isFlying ? Color.green : Color.green;
            flyButton.GetComponent<Button>().colors = Functions.SetThemeColor(flyButton.GetComponent<Button>().colors);
            flyButton.GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            flyButton.GetComponent<Button>().onClick.RemoveAllListeners();
            flyButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                base.gameObject.GetComponent<Cheats>().ToggleFly();
                flyButton.GetComponentInChildren<Text>().text = !base.gameObject.GetComponent<Cheats>().isFlying ? "Enable\nFly" : "Disable\nFly";
                flyButton.GetComponentInChildren<Text>().color = !base.gameObject.GetComponent<Cheats>().isFlying ? Color.green : Color.red;
            });
            flyButton.transform.localPosition += new Vector3(420f, 0f);
        }
    }
}
