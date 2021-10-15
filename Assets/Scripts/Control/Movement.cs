using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace unimelb.fps
{
    public class Movement : MonoBehaviour
    {
        public float speed;

        private Rigidbody rig;
        void Start()
        {
            //Camera.main.enabled = false;
            rig = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            float tempYMove = Input.GetAxisRaw("Vertical");
            float tempXMove = Input.GetAxisRaw("Horizontal");

            Vector3 tempDir = new Vector3(tempXMove, 0, tempYMove);
            tempDir.Normalize();

            rig.velocity = transform.TransformDirection(tempDir) * speed * Time.deltaTime;
        }
    }
}
