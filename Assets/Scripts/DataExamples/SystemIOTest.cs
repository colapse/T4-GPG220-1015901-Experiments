using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataExamples
{
    public class SystemIOTest : MonoBehaviour
    {
        public string filePath = "";
        public string fileName = "playerSettings.json";

        public PlayerSettings playerSettings;

        public bool useBinaryWriter = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Save()
        {
            if (playerSettings == null)
                return;

            var playerSettingsJSON = JsonUtility.ToJson(playerSettings);
            var path = filePath == "" ? Path.Combine(Application.persistentDataPath, fileName) : filePath;

            if (!useBinaryWriter)
            {
                File.WriteAllText(path, playerSettingsJSON);
            }
            else
            {
                using (var writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                {
                    writer.Write(playerSettingsJSON);
                }
            }
        }

        public void Load()
        {
            var path = filePath == "" ? Path.Combine(Application.persistentDataPath, fileName) : filePath;
            var playerSettingsJSON = "";

            if (!useBinaryWriter)
            {
                playerSettingsJSON = File.ReadAllText(path);
            }
            else if (File.Exists(fileName))
            {
                
                using (var reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    playerSettingsJSON = reader.ReadString();
                }
            }
            
            try
            {
                playerSettings = JsonUtility.FromJson<PlayerSettings>(playerSettingsJSON);
            }
            catch (Exception e)
            {
                Debug.Log("Couldn't load player settings. " + e.Message);
            }

            if (playerSettings == null)
                playerSettings = new PlayerSettings();
        }
    }
}
