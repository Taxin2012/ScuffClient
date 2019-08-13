﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ScuffClient.Reflections;

namespace ScuffClient.Misc
{
    public class SpyCamera : MonoBehaviour
    {
        private static bool isCameraActive() => MiscReflections.GetCameraMode() == 2;
        private float movSpeed = 4f;
        private float rotSpeed = 15f;

        public void Update()
        {
            if (Event.current.shift && Input.GetKeyDown(KeyCode.I))
            {
                if (isCameraActive())
                {
                    UserCameraController cam = UserCameraController.Instance;
                    cam.transform.localRotation = Quaternion.identity; //Reset to fix rotation "bug"
                    cam.transform.localPosition = Vector3.zero; //Reset to fix rotation "bug"
                    MiscReflections.SetCameraMode(0);
                    return;
                }
            }
            if (isCameraActive())
            {
                UserCameraController cam = UserCameraController.Instance;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    cam.transform.Translate(Vector3.forward * (Time.deltaTime * movSpeed), Space.Self);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    cam.transform.Translate(Vector3.back * (Time.deltaTime * movSpeed), Space.Self);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //cam.transform.Translate(Vector3.left * (Time.deltaTime * speed), Space.Self);
                    cam.transform.Rotate(0f, 10f * (Time.deltaTime * (-rotSpeed)), 0f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    //cam.transform.Translate(Vector3.right * (Time.deltaTime * speed), Space.Self);
                    cam.transform.Rotate(0f, 10f * (Time.deltaTime * rotSpeed), 0f);
                }
                if (Input.GetKey(KeyCode.E))
                {
                    cam.transform.Translate(Vector3.up * (Time.deltaTime * movSpeed));
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    cam.transform.Translate(Vector3.down * (Time.deltaTime * movSpeed));
                }
            }
        }
    }
}
