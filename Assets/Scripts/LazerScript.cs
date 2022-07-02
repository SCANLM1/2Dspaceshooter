using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    // speed varible
    private float _lazerSpeed = 12.0f;

    

    //color varible
    // Start is called before the first frame update
    void Start()
    {
        //vertical position varible

        float verticalInput = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        // make laszer translate up
        transform.Translate(Vector3.up * _lazerSpeed * Time.deltaTime);

        if (transform.position.y >= 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
        
    }
}
