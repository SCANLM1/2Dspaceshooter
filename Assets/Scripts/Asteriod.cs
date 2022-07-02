using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    private float _asteroidRotateSpeed = 3.0f;
    
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _asteroidRotateSpeed * Time.deltaTime);
    }

    // check for lazer collision (trigger)
    // instatioate expolsion at position of the astreriod
    // destroy explosion after 3 seconds
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "lazer")
        {
           
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.15f);
            Destroy(other.gameObject);
            
            
        }
    }
}
