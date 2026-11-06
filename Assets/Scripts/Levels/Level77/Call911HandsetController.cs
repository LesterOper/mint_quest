using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Call911HandsetController : MonoBehaviour
{
    [SerializeField] private Call911Controller _call911Controller = null;
    [SerializeField] private GameObject DropPlaceA = null;
    [SerializeField] private GameObject DropPlaceB = null;
    [SerializeField] private GameObject _prefabDrop = null;
    private IEnumerator coroutine;
    private bool isActive = false;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Powtorrrr!!!");
        if (!isActive)
        {
            isActive = true;
            _call911Controller.ShowPhonePanel();
            _call911Controller.StartFlood();
            StartCoroutine(coroutine); 
        }
    }
    public void RestartHandset()
    {
        StopCoroutine(coroutine);
        isActive = false;
    }

    public void StopDrop()
    {
        StopCoroutine(coroutine);
    }
    
    public void SpawnWater()
    {
        float _rnd = Random.Range(-0.1f, 0.1f);
        float _rnd2 = Random.Range(-0.1f, 0.1f);
        
        var Aobj = Instantiate(_prefabDrop, new Vector3(DropPlaceA.transform.position.x+_rnd*2, DropPlaceA.transform.position.y, 0), Quaternion.identity);
       // Aobj.transform.localScale =  new Vector3(Aobj.transform.localScale.x+_rnd*0.7f, Aobj.transform.localScale.y+_rnd*0.7f, 1f);
        var Bobj = Instantiate(_prefabDrop, new Vector3(DropPlaceB.transform.position.x+_rnd2*2, DropPlaceB.transform.position.y, 0), Quaternion.identity);
        //Bobj.transform.localScale =  new Vector3(Bobj.transform.localScale.x+_rnd2*0.7f, Bobj.transform.localScale.y+_rnd2*0.7f, 1f);
    }
    
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            SpawnWater();
            yield return new WaitForSeconds(waitTime);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        coroutine = WaitAndPrint(0.2f);
    }
}
