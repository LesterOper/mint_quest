using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class PistolController : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private MuhaController muha;
    private GameObject bullet;
    private Rigidbody2D rb;
    private float timeToNextShoot = 1.2f;
    private bool isEnableShoot;
    private bool isCartridgeAttach = false;

    

    private void Update()
    {
        if(!isCartridgeAttach) return;
        timeToNextShoot -= Time.deltaTime;
        if (timeToNextShoot <= 0)
        {
            isEnableShoot = true;
            timeToNextShoot = 1.2f;
        }
    }

    public void tapOnItem()
    {
        if (isEnableShoot)
        {
            var Ptr = transform.position;
            var Etr = target.transform.position;
            var heading = Etr - Ptr;
            var dist = heading.magnitude;
            var dir = heading / dist;
            bullet = Instantiate(prefabBullet);
            rb = bullet.GetComponent<Rigidbody2D>();

            Context.SoundPlayer.PlaySoundClick("shoot_silenced", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            if (Vector2.Distance(target.transform.position, muha.transform.position) <= 0.25f)
            {
                muha.IsEnable = false;
                muha.StopSequance();
                muha.transform.DOMove(target.transform.position, 0.15f).SetEase(Ease.Linear);
                bullet.transform.DOMove(target.transform.position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
                });
            }
            else rb.AddForce(dir * 7f, ForceMode2D.Impulse);
            isEnableShoot = false;
        }
    }

    public void SetFlag()
    {
        isCartridgeAttach = true;
        timeToNextShoot = 0;
    }
}
