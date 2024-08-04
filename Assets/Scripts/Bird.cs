using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class Bird : MonoBehaviour
{
    [Header("Variables")]

    [SerializeField]
    private float _upwardForce = 5.5f;
    private float _wingSoundVolume = 0.5f;
    private float _hitSoundVolume = 0.5f;
    private float _dieSoundVolume = 0.5f;
    private float _pointSoundVolume = 0.5f;
    private float _maximumHeight = 4.2f;
    public bool isFlying;
    public bool hasHitPipe = false;

    [Header("Audio clips")]
    public AudioClip wingSound;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip pointSound;

    [Header("Bird components")]
    private AudioSource _audioSource;
    private Animator _animator;
    private Rigidbody2D _rb2D;

    [Header("Other")]
    private GameManager _gameManager;
   
    void Awake()
    {

      _audioSource = GetComponent<AudioSource>(); // Get reference to the AudioSource component
      _animator = GetComponent<Animator>(); // Get reference to the Animator component
      _rb2D = GetComponent<Rigidbody2D>(); // Get reference to the Rigidbody2D component

      _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Get access to the GameManager script

    }

    void Start()
    {

      float initialXPos = 0.0f;
      float initialYPos = 0.0f;

      float scaleX = 1.4f;
      float scaleY = 1.4f;
      float scaleZ = 0.0f;

      transform.position = new Vector2(initialXPos, initialYPos); // Set the bird to its initial position
      transform.localScale = new Vector3(scaleX, scaleY, scaleZ); // Change bird scale

      // Rigidbody2D settings

      _rb2D.bodyType = RigidbodyType2D.Dynamic;
      _rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
      _rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
      _rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
      _rb2D.drag = 1.0f;
      _rb2D.gravityScale = 1.5f;

      isFlying = true;

    }

    void FixedUpdate()
    {

        if(isFlying)
        {

          Fly();
          isFlying = false;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

      if(other.gameObject.tag == "Trigger")
      {
 
        PlayPointSound();
        _gameManager.UpdateScore(); // Update the player's score
        Debug.Log(gameObject.name + " has entered the " + other.gameObject.name);

      }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Ground")
        {

          if(!hasHitPipe)
          {

            PlayHitSound();

          }

          _animator.SetBool("Stop_Animation", true); // Stop the bird animation
          _gameManager.isGameOver = true; // Set isGameOver to true
          Debug.Log("Game Over");
 
        }

        if(collision.gameObject.tag == "Pipe-Down" || collision.gameObject.tag == "Pipe-Up")
        {

          if(!hasHitPipe)
          {

            PlayHitSound();
            PlayDieSound();
            hasHitPipe = true;

          }

          _animator.SetBool("Stop_Animation", true); // Stop the bird animation
          _gameManager.isGameOver = true; // Set isGameOver to true
          Debug.Log("Game Over");

        }
    }

    void Update()
    {

        bool spaceKeyPressed = Input.GetKeyDown(KeyCode.Space);

        if(spaceKeyPressed && !_gameManager.isGameOver)
        {

          PlayWingSound();
          isFlying = true; // Set isFlying variable to true if the space key is pressed

        }

        KeepBirdInBounds();
    }

    void Fly()
    {

      Vector2 force = Vector2.up;
      _rb2D.AddForce(force * _upwardForce, ForceMode2D.Impulse); // Add upward force to the bird so it can fly

    }

    void KeepBirdInBounds()
    {
        float currentY = transform.position.y; // Get the current y position of the bird

        if(currentY >= _maximumHeight)
        {

          float drag = 0.5f;
          transform.position = new Vector2(transform.position.x, _maximumHeight);
          _rb2D.velocity = new Vector2(_rb2D.velocity.x, -drag);

        }
    }

    void PlayWingSound()
    {

      _audioSource.PlayOneShot(wingSound, _wingSoundVolume); // Play the bird's wing sound 

    }

    void PlayHitSound()
    {

      _audioSource.PlayOneShot(hitSound, _hitSoundVolume); // Play the hit sound when the bird hits something

    }

    void PlayDieSound()
    {

      _audioSource.PlayOneShot(dieSound, _dieSoundVolume); 

    }

    void PlayPointSound()
    {

      _audioSource.PlayOneShot(pointSound, _pointSoundVolume); // Play point sound when the bird scores
      _audioSource.panStereo = -0.5f;

    }
}




