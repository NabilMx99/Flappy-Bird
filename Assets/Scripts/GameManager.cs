using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour  
{
    [Header("Variables")]
    private int _score;
    private float _swooshSoundVolume = 0.5f;
    private float _restartTime = 0.5f;
    public bool isGameOver;

    [Header("Components")]
    private AudioSource _audioSource;
    public AudioClip swooshSound;
    public Button restartButton;
    public Image restartButtonImage;
    public Image gameOverImage;
    public TextMeshProUGUI scoreText;

    void Awake()
    {

        _audioSource = GetComponent<AudioSource>(); // Get reference to the AudioSource component

    }

    void Start() 
    {
        // Score text settings

       _score = 0;

       scoreText.text = _score.ToString();
       scoreText.fontStyle = FontStyles.Bold;
       scoreText.fontSize = 100.0f;
       scoreText.horizontalAlignment = HorizontalAlignmentOptions.Center;
       scoreText.verticalAlignment = VerticalAlignmentOptions.Middle;
       scoreText.enableWordWrapping = false;

       isGameOver = false;

       gameOverImage.enabled = false;
       restartButtonImage.enabled = false;

       restartButton.onClick.AddListener(RestartGame);
        
    }

   
    void Update()
    {

        CheckGameOver();
        
    }

    void AddScore()
    {

        _score++; // Increase the player's score by one

    }

    public void UpdateScore()
    {

        AddScore();
        scoreText.SetText(_score.ToString());

    }

    public void ShowGameOverImage()
    {

       gameOverImage.enabled = true; // Enable the game over UI image

    }

    public void ShowRestartButton()
    {

       restartButtonImage.enabled = true; // Enable the restart button UI image

    }

    void ReloadScene()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex); // Reload the current scene

    }

    void RestartGame()
    {

        StartCoroutine(WaitForRestart());

    }

    IEnumerator WaitForRestart()
    {

        PlaySwooshSound();
        yield return new WaitForSeconds(_restartTime); // Wait for 0.5 seconds before restarting the game
        ReloadScene();
 
    }

    void PlaySwooshSound()
    {

       _audioSource.PlayOneShot(swooshSound, _swooshSoundVolume); // Play swoosh sound

    }

    void CheckGameOver()
    {

        if(isGameOver)
        {

           ShowGameOverImage(); // Show game over image
           ShowRestartButton(); // Show restart button

        }
    }
}
