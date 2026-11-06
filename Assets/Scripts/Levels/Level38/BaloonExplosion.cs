using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;

public class BaloonExplosion : MonoBehaviour
{
    [SerializeField] private GameObject _prefabExplosion = null;
    [SerializeField] private GameObject _objThread = null;
    [SerializeField] private BaloonsController _baloonsController = null;

    private void OnDestroy()
    {
        DOTween.Kill(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.ToString() == "end_button" || other.name.ToString() == "scissors")
        {
            Context.SoundPlayer.PlaySoundClick("balloon_1", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            _baloonsController.DecreaseBaloon();
            if (_objThread.GetComponent<Animation>() != null) 
            {
                _objThread.GetComponent<Animation>().enabled = false;
            }
            Instantiate(_prefabExplosion, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            _objThread.transform.DOMoveY(-30f, 5f);
            Destroy(gameObject);
        }
    }

}
