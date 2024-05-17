using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player playerToSpawn;
    private Vector3 spawnLocation;
    private GameObject playerSpawnPos;


    void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        spawnLocation = new Vector3(0f, 1f, 0f);
        playerSpawnPos = new GameObject();
        playerSpawnPos.name = "PlayerStart";
        playerSpawnPos.transform.position = spawnLocation;
        playerToSpawn.tag = "Player";

        Instantiate(playerToSpawn, playerSpawnPos.transform.position, Quaternion.identity);
    }
}
