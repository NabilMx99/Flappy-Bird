using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class MainManager : MonoBehaviour
{


   [Header("Variables")]
   private float _swooshSoundVolume = 0.5f;
   private float _launchTime = 1.0f;

   [Header("Components")]
   private AudioSource _audioSource;
   public AudioClip swooshSound;
   private Button _playButton;

   void Awake()
   {

      _audioSource = GetComponent<AudioSource>(); // Get reference to the AudioSource component
      _playButton = GameObject.Find("Play Button").GetComponent<Button>(); // Get reference to the Button component

   }

   void Start()
   {

      _playButton.onClick.AddListener(LaunchGame); // Launch the game when the play button is clicked

   }

   void LaunchGame()
   {

      StartCoroutine(WaitForLaunch());

   }

   IEnumerator WaitForLaunch()
   {

      PlaySwooshSound();
      yield return new WaitForSeconds(_launchTime); // Wait for 1 second before loading the next scene
      LoadScene();

   }

   void LoadScene()
   {

      SceneManager.LoadScene("Flappy Bird"); // Load the Flappy Bird scene

   }

   void PlaySwooshSound()
   {

      _audioSource.PlayOneShot(swooshSound, _swooshSoundVolume); // Play swoosh sound

   }
}
