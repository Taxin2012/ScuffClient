using System;
using UnityEngine;

namespace ScuffClient.Misc
{
    public class Cheats : MonoBehaviour
    {
        private bool isFlying;
        private bool isNoclip;
        private bool isSpeedHacking;
        private LocomotionInputController controller;
        private VRCMotionState motion;
        private Vector3 originalGravity;

        public void Update()
        {
            try
            {
                if(VRCPlayer.Instance != null && (controller == null || motion == null))
                {
                    controller = VRCPlayer.Instance.GetComponent<LocomotionInputController>();
                    motion = VRCPlayer.Instance.GetComponent<VRCMotionState>();
                }
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
            if(isFlying)
            {
                originalGravity = Physics.gravity;
                Physics.gravity = Vector3.zero;

                Vector3 pos = VRCPlayer.Instance.transform.position;

                if(Input.GetKey(KeyCode.Q))
                    VRCPlayer.Instance.transform.position = new Vector3(pos.x, pos.y - (12 * Variables.speedHackValue) * Time.deltaTime, pos.z);
                
                if(Input.GetKey(KeyCode.E))
                    VRCPlayer.Instance.transform.position = new Vector3(pos.x, pos.y + (12 * Variables.speedHackValue) * Time.deltaTime, pos.z);
            }
            if(!isFlying)
            {
                Physics.gravity = originalGravity;
                controller.strafeSpeed = 2f;
                controller.walkSpeed = 2f;
                controller.runSpeed = 4f;
            }
        }
        private void NoClip()
        {

        }
        private void SpeedHack()
        {

        }
    }
}
