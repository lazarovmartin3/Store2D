using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        Player.instance.gameObject.transform.position = new Vector2(-34, -7);
        Player.instance.ShowPlayer();
        EnemyManager.instance.StartGame();
    }
}
