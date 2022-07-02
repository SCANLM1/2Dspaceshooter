using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour

{
    // enemys
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float spawnTime = 5.0f;
   
    [SerializeField]
    private GameObject _enemyPrefab;
   

    // tripple shot 
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _powerUpContainer;
    private AudioSource _audioSource;
   
    
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // call to start spawning objects
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(spawnTripple());
    }
    // tripple shot implemetation
    IEnumerator spawnTripple() 
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3.0f);
            // power up spawn
            Vector3 posSpawn = new Vector3(Random.Range(-10, 10), 8, 0);
            int randomPowerUp = Random.Range(0, 3 );
            Instantiate(_powerUps[randomPowerUp], posSpawn, Quaternion.identity);
            

            yield return new WaitForSeconds(Random.Range(3, 10));
        }
        // instantite enemy 
    }

    IEnumerator SpawnEnemy()
    {
        // delay for game to start 
        yield return new WaitForSeconds(3.0f);
        // use a while loop
        while (_stopSpawning == false)
        {
            // enemy spawn
            Vector3 pwrPosSpawn = new Vector3(Random.Range(-10, 10), 8, 0);
            GameObject newEnemy =  Instantiate( _enemyPrefab, pwrPosSpawn, Quaternion.identity);

            

            newEnemy.transform.parent = _enemyContainer.transform;
           

            yield return new WaitForSeconds(5.0f);

        }
       
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }


}
