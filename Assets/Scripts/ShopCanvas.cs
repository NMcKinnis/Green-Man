using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numDontDestroy = FindObjectsOfType<ShopCanvas>().Length;
        if (numDontDestroy > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(this);
        }
        DontDestroyOnLoad(this);
    }

}
