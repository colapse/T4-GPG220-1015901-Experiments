using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DataExamples
{
    public class Test : MonoBehaviour
    {
        public Text uiText;

        public float randomNum = 0;

        // Start is called before the first frame update
        void Start()
        {
            Load();
        }

        private float cd = 0;

        // Update is called once per frame
        void Update()
        {
            /*if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                randomNum = Random.Range(500, 5000);
                Save();
            }*/

            if (cd <= 0)
            {
                Debug.Log("Current Random Number " + randomNum);
                cd = 10;
            }
            else
            {
                cd -= Time.deltaTime;
            }
        }

        public void GenNewRandomNumber()
        {
            randomNum = Random.Range(500, 5000);
        }

        public void Load()
        {
            randomNum = PlayerPrefs.GetFloat("RandomNumber");
        }

        public void Save()
        {
            PlayerPrefs.SetFloat("RandomNumber", randomNum);
        }
    }
}
