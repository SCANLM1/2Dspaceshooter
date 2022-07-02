using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update]

    private float _baseEnemySpeed = 4.0f;
    private UIManager _UIManager;
    private Player _player;
    // handle to animator
    private Animator _anim;
    private AudioSource _audioSource;



    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _UIManager = GameObject.Find("Canvas").GetComponent <UIManager>();
        _audioSource = GetComponent<AudioSource>();
       
        if (_player == null)
        {
            Debug.LogError("THe player is NULL");
        }
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("the animator is null");
        }
        //transform.position = new Vector3(Random.Range(-10, 10), 8, 0);
        // assign animator 
    }

    // Update is called once per frame
    void Update()
    {
        // move the enemy dow 4ms
        transform.Translate(Vector3.down * _baseEnemySpeed * Time.deltaTime);
        // respawn at the top
        if (transform.position.y < -5.5)
        {
            transform.position = new Vector3(Random.Range(-10, 10), 15, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        // if other is player destroy enemy and deal damage to player
        if (other.tag == "Player")
        {
            // we need to damage the player 
            Player player = other.transform.GetComponent<Player>();

            if ( player != null)
            {
                player.Damage();
                Debug.Log("player takes 1 dmage");
               
            }
            
            _anim.SetTrigger("OnEnemyDeath");
            _baseEnemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
            
        }
        // if other is a lazer, destroy us and distroy laszer
        
        if (other.tag == "lazer")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
                Debug.Log("mase this works");
                
            }
            GetComponent<BoxCollider2D>().enabled = false;
            _anim.SetTrigger("OnEnemyDeath");
            //_baseEnemySpeed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
           
        }
    }

   
}
