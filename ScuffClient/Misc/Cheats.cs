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
        private Vector3 originalGravity;

        public void Update()
        {
            try
            {
                if (Event.current.shift && Input.GetKeyDown(KeyCode.Alpha1))
                    ToggleFly();
                if(VRCPlayer.Instance != null && controller == null)
                {
                    controller = VRCPlayer.Instance.GetComponent<LocomotionInputController>();
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
    }
}
