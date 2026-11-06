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

public class ButtonClickedContr : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject mintBack;
    [SerializeField] private GameObject bat;
    [SerializeField] private GameObject shine;
    [SerializeField] private GameObject eyes;
    [SerializeField] private Sprite onBtn;
    private AudioSource _source;

    private void Awake()
    {
        Vector2 screen = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width, Screen.height));
        transform.position = new Vector3(screen.x/4.2f, transform.position.y);
        Camera.main.orthographic = false;
    }

    public void tapOnItem()
    {
        GetComponent<SpriteRenderer>().DOFade(0.1f, 0.4f).OnComplete(() =>
            {
                GetComponent<SpriteRenderer>().sprite = onBtn;
                GetComponent<SpriteRenderer>().DOFade(1f, 0.4f).OnComplete(() =>
                    {
                        GetComponent<SpriteRenderer>().DOFade(0, 0.25f);
                        shine.GetComponent<SpriteRenderer>().DOFade(0, 0.25f).OnComplete(() =>
                            {
                                shine.GetComponent<SpriteRenderer>().DOFade(1, 0.25f).OnComplete(() =>
                                {
                                    shine.GetComponent<SpriteRenderer>().DOFade(0, 0.25f).OnComplete(() =>
                                    {
                                        Context.SoundPlayer.StopPlayingSounds(_source);
                                        eyes.gameObject.SetActive(false);
                                        mintBack.SetActive(true);
                                        bat.SetActive(true);
                                        shine.SetActive(false);
                                    });
                                });
                            });
                    });
            });
        _source = Context.SoundPlayer.PlaySoundClick("electricity", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
    }
}
