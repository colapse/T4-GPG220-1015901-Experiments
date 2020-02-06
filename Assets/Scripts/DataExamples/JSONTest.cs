using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.UIElements;
using UnityEngine;

namespace DataExamples
{
    
    public class JSONTest : MonoBehaviour
    {
        public PlayerSettings playerSettings;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SaveSettings()
        {
            if (playerSettings == null)
                return;
            
            var playerSettingsJSON = JsonUtility.ToJson(playerSettings);
            PlayerPrefs.SetString("PlayerSettings",playerSettingsJSON);
        }

        public void LoadSettings()
        {
            var playerSettingsJSON = PlayerPrefs.GetString("PlayerSettings");

            try
            {
                playerSettings = JsonUtility.FromJson<PlayerSettings>(playerSettingsJSON);
                
                Debug.Log("Loaded Player Settings: Name/"+playerSettings.playerName+" SomeInteger/"+playerSettings.someInteger+" SomeFloat/"+playerSettings.someFloat);
            }
            catch (Exception e)
            {
                Debug.Log("Couldn't load player settings. " + e.Message);
            }
            
            if(playerSettings == null)
                playerSettings = new PlayerSettings();
        }
    }
}
