using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utility
{
    public class Billboard : MonoBehaviour
    {
        private GameObject cam;

        private void Awake()
        {
            cam = GameObject.FindGameObjectWithTag(Constants.CAMERA_TAG);
        }

        void LateUpdate()
        {
            Vector3 camDirection = transform.position + cam.transform.forward;
            transform.LookAt(camDirection);
        }
    }
}
