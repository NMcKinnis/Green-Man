using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;

    [SerializeField] public static Vector2 playerPos0= new Vector2(-5, -1);
    [SerializeField] public static Vector2 playerPos1 = new Vector2 (-5, -1);
    public Vector2 playerPosition;
    private void OnEnable()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            playerPosition = playerPos0;
        }
        else
        {
            playerPosition = playerPos1;
        }
    }


    public void LoadScene(string nextLevel)
    {
        FindObjectOfType<Player>().inputAction.Disable();
        SceneManager.LoadScene(nextLevel);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
/*        Debug.Log(playerPosition);
        Debug.Log(playerPos0);*/
    }
}