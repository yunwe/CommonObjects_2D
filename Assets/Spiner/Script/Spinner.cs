using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.sawyunwe.spinner
{
    public class Spinner : MonoBehaviour
    {
        const int rev = 360;

        public InputField inputField;
        public int slotCount = 6;

        private float m_speed;

        private bool m_isSpinning;
        private int m_stopIndex;
        private float m_deceleration;

        private float m_stopTime;
        private float m_TotalRev;
        private float distance;

        // Start is called before the first frame update
        void Start()
        {
            this.m_speed = 0f;
            this.m_stopIndex = -1;
            this.m_isSpinning = false;
            this.inputField.text = "0";
        }

        // Update is called once per frame
        void Update()
        {
            if(m_isSpinning)
            {
                transform.Rotate(0, 0, this.m_speed * rev * Time.deltaTime);
                if (m_stopIndex != -1)
                    m_TotalRev += this.m_speed * rev * Time.deltaTime;
            }

            if(m_isSpinning && m_stopIndex != -1)
            {
                m_isSpinning &= m_speed > 0; // if short hand
                this.m_speed -= m_deceleration * Time.deltaTime;
            }

            if(!m_isSpinning && m_stopIndex != -1)
            {
                m_stopIndex = -1;
                Debug.Log("After Angle - " + transform.rotation.eulerAngles.z);
                Debug.Log("Stop Time : " + (Time.time-m_stopTime));
                Debug.Log("Total Rev : " + m_TotalRev);
                Debug.Log("Dist Error : " + (m_TotalRev - distance * 360));
            }
        }

        public void RotateWheel()
        {
            m_isSpinning = true;
            m_speed = Random.Range(7, 10);
            Debug.Log("Speed - " + m_speed);
            m_stopIndex = -1;

        }

        public void StopWheel()
        {
            
            m_stopIndex = int.Parse(inputField.text);
            float nextRound = 1- (transform.rotation.eulerAngles.z/rev);
            float slotDist = (float)m_stopIndex/slotCount;
            distance = (m_speed + 3) + nextRound + slotDist;
            float time = CalculateDeltaTime(distance, m_speed);
            
            m_deceleration = m_speed/time;

            Debug.Log("Distance - " + distance);
            Debug.Log("Calculated Time - " + time);
            m_stopTime = Time.time;
            Debug.Log("Acceleration " + m_deceleration);
            Debug.Log("Before Angle - " + transform.rotation.eulerAngles.z);
        }

        //-------
        //Calculated by using two acceleration formula
        // acceleration formula 1 => [x = ut + (1/2)at^2]; u = initital velocity, a = acceleration
        //d = -a
        //x = ut - (1/2)dt^2
        //deceleration formulat 2 => [d = (v-u)/t]
        float CalculateDeltaTime(float dist, float initialSpeed)
        {
            return (2*dist)/initialSpeed;
        }
    }
}

