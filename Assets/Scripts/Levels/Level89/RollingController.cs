using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingController : MonoBehaviour
{
   [SerializeField] private GameObject target;
   [SerializeField] private GameObject prefab;
   [SerializeField] private GameObject man_1, man_2;
   private RollingHelper _helper;
   private bool isCan = false;
   GameObject gm;
   private float timeToNextSpawn = 3f;
   
   private void Awake()
   {
      _helper = FindObjectOfType<RollingHelper>();
      gm = _helper.isFirst ? man_1 : _helper.isSecond ? man_2 : null;
      if(gm!=null)
         gm.SetActive(false);
      target = GameObject.FindWithTag("target");
   }

   private void Update()
   {
      timeToNextSpawn -= Time.deltaTime;
      gm = _helper.isFirst ? man_1 : _helper.isSecond ? man_2 : null;
      if(gm!=null)
         gm.SetActive(false);
      if (Vector2.Distance(target.transform.position, transform.position) <= 1f)
      {
            _helper.CreateTrain();
      }

      if (timeToNextSpawn <= 0)
      {
         
      }
   }
}
