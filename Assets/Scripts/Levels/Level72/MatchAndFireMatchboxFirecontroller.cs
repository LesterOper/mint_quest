using System;
using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;

public class MatchAndFireMatchboxFirecontroller : MonoBehaviour
{
    [SerializeField] private MatchAndFireController _matchAndFireController = null;
    [SerializeField] private GameObject _explosionPrefab = null;
    private AudioSource _source;
    private bool isPlaying;

    public AudioSource Source => _source;

    public void Explosion()
    {
        Instantiate(_explosionPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(transform.rotation.z * 100);
        if(transform.rotation.z * 100 == -100f) {
            GameObject Spark = Instantiate(_explosionPrefab, new Vector3(other.transform.position.x, other.transform.position.y+0.9f, 0f), Quaternion.identity);
            _matchAndFireController.ActivateFire();
            if (!isPlaying)
            {
                _source = Context.SoundPlayer.PlaySoundClick("fire", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                isPlaying = true;
            }
        
            Spark.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}