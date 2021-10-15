using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unimelb.fps
{
    public class Look : MonoBehaviour
    {
        public static bool cursorLocked = true;
        public Transform player;
        public Transform cams;

        public float xSens;
        public float ySens;
        public float maxAngle;

        // cam's origin of rotation to center
        private Quaternion camCenter;

        void Start()
        {
            camCenter = cams.localRotation;
        }

        void Update()
        {
            SetY();
            SetX();
            UpdateCursor();
        }

        void SetY()
        {
            float tempInput = Input.GetAxis("Mouse Y") * ySens * Time.deltaTime;
            Quaternion tempAdj = Quaternion.AngleAxis(tempInput, -Vector3.right);
            Quaternion tempDelta = cams.localRotation * tempAdj;

            if (Quaternion.Angle(camCenter, tempDelta) < maxAngle)
            {
                cams.localRotation = tempDelta;
            }
        }
        void SetX()
        {
            float tempInput = Input.GetAxis("Mouse X") * xSens * Time.deltaTime;
            Quaternion tempAdj = Quaternion.AngleAxis(tempInput, Vector3.up);
            Quaternion tempDelta = player.localRotation * tempAdj;
            player.localRotation = tempDelta;
        }

        void UpdateCursor()
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = false;
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = true;
                }
            }
        }
    }
}
