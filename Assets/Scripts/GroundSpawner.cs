using System.Collections;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] private float _spawnX = 20.1f;
    private float _spawnY = -4.0f;
    private float _spawnTime = 0.0f; 
    [SerializeField] private float _spawnRate = 6.7f; 

    [Header("Other")]
    public GameObject groundPrefab;
    private GameManager _gameManager;

    void Awake()
    {

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Get access to the GameManager script

    }

    void Start()
    {

        InvokeRepeating("SpawnGround", _spawnTime, _spawnRate);

    }

    void Update()
    {

        DestroyGround();
        StopSpawn();

    }

    void SpawnGround()
    {

        Vector2 spawnPos = new Vector2(_spawnX, _spawnY);
        Instantiate(groundPrefab, spawnPos, Quaternion.identity); // Spawn ground
              
    }

    void DestroyGround()
    {

        float xBound = -20.1f;
 
        GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");


        foreach(GameObject ground in grounds)
        {

            if(ground.transform.position.x <= xBound)
            {

               Destroy(ground); // Destroy ground

            }
        }
    }

    void StopSpawn()
    {

       if(_gameManager.isGameOver)

        CancelInvoke("SpawnGround"); // Stop spawning ground if the game is over

    }
}
