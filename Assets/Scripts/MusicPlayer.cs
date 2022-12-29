using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [field: SerializeField] public AudioSource audioSource { get; private set; }

    void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(this);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

}
