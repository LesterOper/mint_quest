using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using Random = UnityEngine.Random;


public class TVRemoteHelper : MonoBehaviour
{
   [SerializeField] private List<GameObject> fires;
   [SerializeField] private List<GameObject> buttons;
   [SerializeField] private DragController drag;
   private Dictionary<string, GameObject> firesRandom;
   private List<GameObject> activateFiresOrder = new List<GameObject>();
   private List<GameObject> bufActivateFiresOrder;
   private GameObject currentFireToActivate;
   private GameObject lastFire;

   private void Start()
   {
      RandomRemoteButtons();
      RandomOrder();
      bufActivateFiresOrder = new List<GameObject>(activateFiresOrder);
      currentFireToActivate = bufActivateFiresOrder[0];
      drag.GetComponent<DragController>().enabled = false;
   }

   public void CheckFiresClicked(GameObject obj)
   {
      Debug.Log("Clicked:" + firesRandom[obj.name].name);
      Debug.Log("Current:" + currentFireToActivate.name);
      if (firesRandom[obj.name] == currentFireToActivate)
      {
         obj.GetComponent<CircleCollider2D>().enabled = false;
         bufActivateFiresOrder[0].SetActive(true);
         bufActivateFiresOrder.RemoveAt(0);
         if (bufActivateFiresOrder.Count == 0)
         {
            drag.enabled = true;
            drag.SetTarget(lastFire);
            return;
         }

         currentFireToActivate = bufActivateFiresOrder[0];
      }
      else
      {
         Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

         ResetFires();
      }
   }

   private void RandomRemoteButtons()
   {
      firesRandom = new Dictionary<string, GameObject>();
      List<GameObject> bufFires = new List<GameObject>(fires);
      List<GameObject> bufButtons = new List<GameObject>(buttons);
      for (int i = 0; i < buttons.Count; i++)
      {
         int randI = Random.Range(0, bufButtons.Count);
         int randJ = Random.Range(0, bufFires.Count);
         firesRandom.Add(bufButtons[randI].name, bufFires[randJ]);
         bufButtons.RemoveAt(randI);
         bufFires.RemoveAt(randJ);
      }

      lastFire = bufFires[0];
   }

   private void RandomOrder()
   {
      List<GameObject> bufFires = new List<GameObject>(fires);
      bufFires.Remove(lastFire);
      int count = bufFires.Count;

      for (int i = 0; i < count; i++)
      {
         int rand = Random.Range(0, bufFires.Count);
         activateFiresOrder.Add(bufFires[rand]);
         bufFires.RemoveAt(rand);
      }
   }

   private void ResetFires()
   {
      bufActivateFiresOrder = new List<GameObject>(activateFiresOrder);
      bufActivateFiresOrder.ForEach(obj =>
      {
         obj.SetActive(false);
      });
      currentFireToActivate = bufActivateFiresOrder[0];
      
      buttons.ForEach(obj => { obj.GetComponent<CircleCollider2D>().enabled = true; });
   }
}
