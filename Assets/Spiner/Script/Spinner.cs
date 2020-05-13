using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sawyunwe.spinner
{
    public class Spinner : MonoBehaviour
    {
        public float speed;
        private float m_speed;

        // Start is called before the first frame update
        void Start()
        {
            this.m_speed = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.m_speed = this.speed;
            }

            transform.Rotate(0, 0, this.m_speed);
            this.m_speed *= 0.96f;
        }
    }
}

