using UnityEngine;
using ScuffClient.Reflections;
using System.Reflection;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using System;

namespace ScuffClient.Menu
{
    public class AntiMenu : MonoBehaviour
    {
        #region Variables
        private MethodInfo m_QuickMenuInstance;
        private QuickMenu menu;
        private GameObject elementsMenu;
        private GameObject cameraMenu;
        #endregion

        public void Update()
        {
            if (IsGONull(elementsMenu))
                elementsMenu = menu.transform.Find("UIElementsMenu").gameObject;

            try
            {
                menu = (QuickMenu)m_QuickMenuInstance.Invoke(null, null);
                CreateAntiPortalButton();
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
        }
        private void CreateAntiPortalButton()
        {
            elementsMenu.transform.Find("ToggleNameplatesButton").gameObject.SetActive(false);

            UiToggleButton button = elementsMenu.transform.Find("ToggleHUDButton").gameObject.GetComponentInChildren<UiToggleButton>();

            foreach (Text t in elementsMenu.transform.Find("ToggleHUDButton").GetComponentsInChildren<Text>())
            {
                if (t.text == "HUD On")
                {
                    t.text = "Portals On";
                    t.color = Color.green;
                }
                if (t.text == "HUD Off")
                {
                    t.text = "Portals Off";
                    t.color = Color.red;
                }
            }
        }
    }
}
