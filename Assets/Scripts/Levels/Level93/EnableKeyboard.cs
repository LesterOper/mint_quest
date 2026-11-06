using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class EnableKeyboard : MonoBehaviour, IClickItem
{
   [SerializeField] private KeyboardAnim anim;
   public void tapOnItem()
   {
      anim.OnAnim();
   }
}
