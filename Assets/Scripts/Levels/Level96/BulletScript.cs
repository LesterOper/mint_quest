using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float timeToDeath = 3f;
    void Start()
    {
        
    }

    
    void Update()
    {
        timeToDeath -= Time.deltaTime;
        if(timeToDeath <= 0)
            Destroy(gameObject);
    }
}
