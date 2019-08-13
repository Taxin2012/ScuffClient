using System.Reflection;
using System.Linq;

namespace ScuffClient.Reflections
{
    public class MiscReflections
    {
        #region MiscReflections
        private readonly static MethodInfo m_setCameraMode = typeof(UserCameraController).GetMethod("set_mode");
        private readonly static MethodInfo m_getCameraMode = typeof(UserCameraController).GetMethod("get_mode");
        private readonly static MethodInfo m_FlowInstanceMethod = typeof(VRCFlowManager).GetMethod("get_instance");
        private readonly static MethodInfo m_QuickMenuInstance = typeof(QuickMenu).GetMethod("get_instance");
        private readonly static MethodInfo m_UiManagerInstanceMethod = typeof(VRCUiManager).GetProperties().FirstOrDefault(p => p.PropertyType == typeof(VRCUiManager))?.GetGetMethod();
        #endregion

        public static VRCUiManager GetUiManagerInstance() => (VRCUiManager)m_UiManagerInstanceMethod.Invoke(null, null);
        public static void SetCameraMode(int mode) => m_setCameraMode.Invoke(UserCameraController.Instance, new object[] { (mode <= 2) ? mode : 1 });
        public static int GetCameraMode() => (int)m_getCameraMode.Invoke(UserCameraController.Instance, null);
        public static QuickMenu GetQuickMenuInstance() => (QuickMenu)m_QuickMenuInstance.Invoke(null, null);

    }
}
