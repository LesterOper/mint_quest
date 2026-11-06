using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float _time = 1.5f;
    private IEnumerator coroutine;
    
    IEnumerator DestroyMyself(float waitTime) 
    {
       yield return new WaitForSeconds(waitTime);
       Destroy(this.gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        coroutine = DestroyMyself(_time);
        StartCoroutine(coroutine);
    }
    
}
