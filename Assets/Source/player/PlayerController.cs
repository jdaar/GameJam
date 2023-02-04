using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private static float _playerPositionX = 0;
    private static float _playerPositionY = -3.5f;

    private static string PLAYER_PREFAB_PATH = "prefabs/Player";
    
    void Start()
    {
        this._player = PlayerController.GeneratePlayer();
    }

    void Update() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            this._player.Attack(AttackDirection.LEFT);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
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

        Vector3 _playerPosition = new Vector3(_playerPositionX, _playerPositionY, 0);
        Player newPlayer = Instantiate<Player>(playerPrefab, _playerPosition, Quaternion.identity);

        return newPlayer;
    }
}
