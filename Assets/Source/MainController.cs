using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private Player _player;

    public static string PLAYER_PREFAB_PATH = "prefabs/Player";

    void Start()
    {
        this._player = MainController.GeneratePlayer();
    }

    private static Player GeneratePlayer()
    {
        Player playerPrefab = Resources.Load<Player>(MainController.PLAYER_PREFAB_PATH);

        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab not found at path: " + MainController.PLAYER_PREFAB_PATH);
            return null;
        }

        Player newPlayer = Instantiate(playerPrefab);

        return newPlayer;
    }

    void Update()
    {
        
    }
}
