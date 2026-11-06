using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;

public class PositionAccrodingBanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Context.AdsHelper.isBannerShown)
        {
            var sp = GetComponent<SpriteRenderer>();

            if (sp != null)
            {
                transform.DOMoveY(transform.position.y + 0.3f, 0);
            }
            else
            {
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
