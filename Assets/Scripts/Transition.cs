using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] AudioClip loadSound;
    [SerializeField] float waitTime;
    LevelLoader levelLoader;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().inputAction.Disable();
            AudioSource.PlayClipAtPoint(loadSound, Camera.main.transform.position);
            StartCoroutine(WaitAndLoad());
        }
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitTime);
        levelLoader.LoadScene(sceneToLoad);
    }
}
