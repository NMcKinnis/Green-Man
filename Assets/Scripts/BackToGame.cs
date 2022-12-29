using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGame : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public void CloseShopMenu()
    {
        Time.timeScale = 1;
        player.shopCanvas.SetActive(false);
    }
}
