using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    private float _moveSpeed = 3.0f;
    private GameManager _gameManager;

    void Awake()
    {

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Get access to the GameManager script

    }

    void Update()
    {

        MoveToTheLeft();

    }

    void MoveToTheLeft()
    {

        if(!_gameManager.isGameOver)
        {

          transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);

        }
    }
}
