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
                this.originalGravity = Physics.gravity;
                Physics.gravity = Vector3.zero;

                Vector3 pos = VRCPlayer.Instance.transform.position;

                if(Input.GetKeyDown(KeyCode.Q))
                { }
            }
            if(!isFlying)
            {
                Physics.gravity = this.originalGravity;
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
