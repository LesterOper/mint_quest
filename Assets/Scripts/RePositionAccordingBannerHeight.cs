using System.Collections;
using System.Collections.Generic;
using Swimmer.Library.Common;
using UnityEngine;

public class RePositionAccordingBannerHeight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position.y);
        var height = Context.AdsHelper.BannerHeight;
        var vec = Camera.main.ScreenToWorldPoint(new Vector3(0, height, 0));
        Debug.Log(vec.y);
        transform.position -= vec;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
