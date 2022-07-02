using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // varibles 

    private float _powerUpSpeed = 3.0f;
    
    // power up id's
    // 0 - tripple 
    // 1 - speed 
    // 2 shields
    [SerializeField]
    private int powerUpID;
    [SerializeField]
    private AudioClip _clip;
    


    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-10, 10), 8, 0);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        // move down at a speed of 3
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);
        // destroy when we leave the screen
        if (transform.position.y < -5.5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
                // power up logic using a switch

                switch (powerUpID)
                {
                    case 0:
                        player.trippleShotActivator();
                        Debug.Log("player is powered up with tripple shot");
                        break;
                    case 1:
                        player.speedBoostActivator();
                        Debug.Log("player is powered up with Speed");
                        break;
                    case 2:
                        player.shieldsUp();
                        Debug.Log("shields");
                        break;
                    default:
                        Debug.Log("error");
                        break;

                }

            }
            
            Destroy(this.gameObject);
        }
    }

  
   
    // on trigger collision 
    // check for the player. use tags.
}
