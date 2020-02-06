
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataExamples
{
    [Serializable]
    public class PlayerSettings
    {
        public string playerName;
        public int someInteger = 5;
        public float someFloat = 0.54f;
    }
}