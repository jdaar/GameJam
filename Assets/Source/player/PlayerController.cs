using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private static string PLAYER_PREFAB_PATH = "prefabs/Player";
    
    void Start()
    {
        this._player = PlayerController.GeneratePlayer();
    }

    void Update() {
        if(Input.GetKey(KeyCode.W)) {
            this._player.Attack(AttackDirection.UP);
        }
        if(Input.GetKey(KeyCode.A)) {
            this._player.Attack(AttackDirection.LEFT);
        }
        if(Input.GetKey(KeyCode.S)) {
            this._player.Attack(AttackDirection.DOWN);
        }
        if(Input.GetKey(KeyCode.D)) {
            this._player.Attack(AttackDirection.RIGHT);
        }
    }

    private static Player GeneratePlayer()
    {
        Player playerPrefab = Resources.Load<Player>(PlayerController.PLAYER_PREFAB_PATH);

        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab not found at path: " + PlayerController.PLAYER_PREFAB_PATH);
            return null;
        }

        Player newPlayer = Instantiate<Player>(playerPrefab);
        
        newPlayer.transform.position =  new Vector3(0, 0, 0);

        return newPlayer;
    }
}
