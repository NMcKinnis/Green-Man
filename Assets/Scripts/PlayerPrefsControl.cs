
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsControl : MonoBehaviour
{
    /*     void Start()
         {
             PlayerPrefs.DeleteAll();
         }*/
    public static bool FeatureUnlocked(string name)
    {
        if (PlayerPrefs.GetInt(name) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}