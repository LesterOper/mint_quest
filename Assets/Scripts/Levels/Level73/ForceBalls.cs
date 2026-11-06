using System;
using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;

public class ForceBalls : MonoBehaviour
{
    [SerializeField] private Rigidbody2D firstBall;
    [SerializeField] private Rigidbody2D secondBall;
    [SerializeField] private Rigidbody2D thirdBall;
    [Space] [SerializeField] private GameObject yBorder, xBorder;
    private AudioSource _source;


    private void Start()
    {   float width = Screen.width;
        float height = Screen.height;
        Vector2 vector = Camera.main.ScreenToWorldPoint(new Vector2(width, -height));
        Debug.Log(vector.x + "   " + vector.y);
        xBorder.transform.localPosition = new Vector3(vector.x , xBorder.transform.localPosition.y, 0);
        yBorder.transform.localPosition = new Vector3(yBorder.transform.localPosition.x, vector.y/3.8f, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("FORCE");
        if (other.gameObject == firstBall.gameObject)
        {
           // _source = Context.SoundPlayer.PlaySoundClick(_source, "billiards_1", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Debug.Log("FORCE FIRST");
            float x = firstBall.transform.position.x - transform.position.x;
            float y = firstBall.transform.position.y - transform.position.y;
            firstBall.AddForce(new Vector2(x, y) * 5f, ForceMode2D.Impulse);
        }
        if (other.gameObject == secondBall.gameObject)
        {
            //_source = Context.SoundPlayer.PlaySoundClick(_source, "billiards_1", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Debug.Log("FORCE Second");
            float x = secondBall.transform.position.x - transform.position.x;
            float y = secondBall.transform.position.y - transform.position.y;
            secondBall.AddForce(new Vector2(x, y) * 5f, ForceMode2D.Impulse);
        }
        if (other.gameObject == thirdBall.gameObject)
        {
            //_source = Context.SoundPlayer.PlaySoundClick(_source, "billiards_1", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Debug.Log("FORCE THIRD");
            float x = thirdBall.transform.position.x - transform.position.x;
            float y = thirdBall.transform.position.y - transform.position.y;
            thirdBall.AddForce(new Vector2(x, y) * 5f, ForceMode2D.Impulse);
        }
    }
}
