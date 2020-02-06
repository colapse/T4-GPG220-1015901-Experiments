using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerNickname;
    public float health = 100f;
    public float strength = 10f;

    public int currentExperience = 0;
    
    public int Level => currentExperience / 200;

    public void PrintLevel()
    {
        Debug.Log("Level "+Level+" ("+currentExperience+"xp)");
    }
    
    [MenuItem("Tools/Clear Player Prefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
