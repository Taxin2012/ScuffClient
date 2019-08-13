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
        #endregion

        public void Update()
        {
            if (IsGONull(mainMenu))
                mainMenu = menu.transform.Find("ShortcutMenu").gameObject;

            if (IsGONull(customMenuButton))
                CreateCustomMenuButton();

            //if (IsGONull(customMenu))
                //CustomMenu();
            

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

            customButton.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            customButton.onClick.RemoveAllListeners();
            customButton.onClick.AddListener(delegate ()
            {
                Exploits.DropPortal();
            });
        }

        private void CustomMenu()
        {

        }
    }
}
