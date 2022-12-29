using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numDontDestroy = FindObjectsOfType<DontDestroy>().Length;
        if (numDontDestroy > 1)
        {
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(this);
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
