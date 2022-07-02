using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // --------------------------------------------------------------- General references ------------------------------------------------
    public float horizontalInput;
    [SerializeField]
    private int _lives = 3;


    // ------------------------------------------------------------------ speed values ----------------------------------------------------------
    [SerializeField] // allows the designers to see and augment value
    private float _speed = 8f; //defult value = 0
    private float _speedMultiplier = 2;
    private bool _isSpeedActive = false;

    // ---------------------------------------------------------------- class references --------------------------------------------------------
    private SpawnManager _spawnManager;
    private UIManager _uIManager;

    // ---------- game objects  ---------------------//
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private GameObject _lazerPrefab;
    [SerializeField]



    // ----------------------------------------------------------------power up varibles ------------------------------------------------------
    // reference code to powerUp

    private PowerUp _PowerUp;
    // is tripple shot emabeled? 
    [SerializeField]
    private bool _trippleShotPwr = false; // default it is false
    // is speed boost active ?
    [SerializeField]
    // shields varible 
    private bool _shieldStatus = false;
    private float _baseLazerFireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _shieldVisualiser;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;


    // 
    [SerializeField]
    private int _score;

    // audio
    [SerializeField]
    private AudioClip _lazerSoundClip;
    
    private AudioSource _audioSource;


    void Start()
    {
        // take current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        

      
        if (_uIManager == null)
        {
            Debug.LogError("uimanager class not received");

        }
       


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {

            shootLazer();
        }

        calculateMovement();

        

    }

    private void enableDamageAni()
    {
        
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }
    }
    void shootLazer()
    {
        // if space key is hit spawn lazer object
        _canFire = Time.time + _baseLazerFireRate;
        

        if (Input.GetKeyDown(KeyCode.Space) && _trippleShotPwr == true)
        {
            
            Instantiate(_trippleShotPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            
        }
        else
        {
           
            Instantiate(_lazerPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            
        }

        

    }
    void calculateMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        // alt code to clamp 
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        // if player position on x is greater than 11
        // y = -11
        // bounds teleport at edge of screen
        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11.0f)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

   

    public void Damage()
    {
        // shields code in damage
        if (_shieldStatus == true)
        {
            _shieldStatus = false;
            _shieldVisualiser.SetActive(false);
           
            //Debug.Log("player was not damaged");
            return;
        }
        
        
            
         _lives -- ;
        enableDamageAni();
        
        // if lives 2 enable right engine
        //else if lives is one add both
         _uIManager.UpdateLives(_lives);
            
        

        // kill code
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            Debug.Log("game over ");

        }
    }


    // -------------------------------------------------------------------- sheilds ---------------------------------------------------------------
    public void shieldsUp()
    {
        _shieldStatus = true;
        _shieldVisualiser.SetActive(true);
        
    }


    // ------------------------------------------------------------------ speed boost ---------------------------------------------------------------
    public void speedBoostActivator()
    {

        _isSpeedActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(speedBoostCooldown());

    }
    IEnumerator speedBoostCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedActive = false;
        _speed /= +_speedMultiplier;
        Debug.Log("speed boost is over");

    }

    // ------------------------------------------------------------------ tripple shot ---------------------------------------------------------------
    public void trippleShotActivator()
    {
        _trippleShotPwr = true;
        StartCoroutine(trippleShotCoolDOwn());
        // power down courtine 
    }
    // ienumerator 
    IEnumerator trippleShotCoolDOwn()
    {
        yield return new WaitForSeconds(5.0f);
        _trippleShotPwr = false;
        Debug.Log("tripple shot is over");
    }

    public void AddScore(int points)
    {
        _score += points;
        _uIManager.updateScore(_score);
    }

    // method to add 10 to the score
    // comunicate UI manager 

}
