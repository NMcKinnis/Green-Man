using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string pickUpName;
    [SerializeField] AudioClip collectedSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Picking up the pick up");
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt(pickUpName, 1);
            AudioSource.PlayClipAtPoint(collectedSound, Camera.main.transform.position);
            Debug.Log("Unlocked the shoot upgrade");
            Destroy(this.gameObject);
        }
    }
}
