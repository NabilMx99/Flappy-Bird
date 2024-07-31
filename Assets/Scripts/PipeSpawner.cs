using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] float _spawnX = 10.0f;
    private float _spawnY;
    [SerializeField] private float _spawnTime = 1.5f;
    [SerializeField] private float _spawnRate = 2.0f;

    [Header("Other")]
    public GameObject pipePrefab;
    private GameManager _gameManager;

    void Awake()
    {

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Get access to the GameManager script

    }

    void Start()
    {

        InvokeRepeating("SpawnPipe", _spawnTime, _spawnRate);

    }

    void Update()
    {
 
        DestroyPipe();
        StopSpawn();

    }

    void SpawnPipe()
    {

        float minY = -0.8f;
        float maxY = 2.3f;

        _spawnY = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(_spawnX, _spawnY);
        Instantiate(pipePrefab, spawnPos, Quaternion.identity); // Spawn pipe at random heights

    }

    void DestroyPipe()
    {
        float xBound = -10.0f;

        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

        foreach(GameObject pipe in pipes)
        {

           if(pipe.transform.position.x <= xBound)
            
            Destroy(pipe); // Destroy pipe

        }
    }

    void StopSpawn()
    {

      if(_gameManager.isGameOver)

        CancelInvoke("SpawnPipe"); // Stop spawning pipes if the game is over

    }
}
