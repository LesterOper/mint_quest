using System;
using App.Global;
using Swimmer.Library;
using UnityEngine;

namespace DefaultNamespace
{
    public class CommonAnimController : MonoBehaviour
    {
        private void OnEnable()
        {
            Messenger.AddListener(Messages.ANIM_EFFECTS,startAnim);
        }

        private void startAnim(Bundle bundle)
        {
            var animScript = bundle.Get<GameObject>(Messages.OBJECT_IANIMATE).GetComponent<IAnimate>();
            if (animScript != null)
            {
                animScript.setupAnim();
            }
        }

        private void OnDisable()
        {
            Messenger.RemoveListenerIfHas(Messages.ANIM_EFFECTS, startAnim);
        }
    }
}