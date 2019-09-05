using System;
using UnityEngine;

namespace ScuffClient.Misc
{
    public class Cheats : MonoBehaviour
    {
        public bool isFlying = false;
        public bool isNoclip;
        public bool isSpeedHacking;
        private LocomotionInputController controller;
        private Vector3 originalGravity;

        public void Update()
        {
            try
            {
                if(VRCPlayer.Instance != null && controller == null)
                {
                    controller = VRCPlayer.Instance.GetComponent<LocomotionInputController>();
                }
                
                if (Event.current.shift && Input.GetMouseButtonDown(0))
                    TeleportToRaycast();
                
                AddJumpComponent();
                Flying();
                NoClip();
                SpeedHack();
            }
            catch { }
        }
        #region toggles
        public void ToggleFly()
        {
            isFlying = !isFlying;

            if (isFlying)
            {
                originalGravity = Physics.gravity;
                Physics.gravity = Vector3.zero;
            }
            else
                Physics.gravity = originalGravity;

        }
        public void ToggleNoClip()
        {
            isNoclip = !isNoclip;
        }
        public void ToggleSpeedHack()
        {
            isSpeedHacking = !isSpeedHacking;
        }
        #endregion
        private void Flying()
        {
            if (isFlying)
            {
                Vector3 pos = VRCPlayer.Instance.transform.position;
                Physics.gravity = Vector3.zero;

                if (Input.GetKey(KeyCode.Q))
                    VRCPlayer.Instance.transform.position = new Vector3(pos.x, pos.y - (12 * Variables.speedHackValue) * Time.deltaTime, pos.z);

                if (Input.GetKey(KeyCode.E))
                    VRCPlayer.Instance.transform.position = new Vector3(pos.x, pos.y + (12 * Variables.speedHackValue) * Time.deltaTime, pos.z);
            }
        }
        private void NoClip()
        {

        }
        private void SpeedHack()
        {

        }
        private void AddJumpComponent()
        {
            if (VRCPlayer.Instance.gameObject.GetComponent<PlayerModComponentJump>() == null)
                VRCPlayer.Instance.gameObject.AddComponent<PlayerModComponentJump>();
        }
        private void TeleportToRaycast()
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            if(Physics.Raycast(ray.origin, ray.direction, out RaycastHit rayCast, float.PositiveInfinity))
                VRCPlayer.Instance.transform.position = rayCast.point;
        }
    }
}
