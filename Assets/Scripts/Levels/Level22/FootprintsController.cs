using System.Collections;
using System.Collections.Generic;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class FootprintsController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SpriteRenderer footprint;

    [SerializeField] private Sprite active;

    [SerializeField] private Sprite deActive;

    [SerializeField] private FootprintHelper _helper;

    private bool isReset = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 && !isReset)
        {
            ResetFootprints();
            isReset = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_helper.FootprintsChecker(gameObject))
        {
            //Context.SoundPlayer.PlaySoundClick("step_1_2", AudioMixerController.MasterMixerGroup.SFX, 1);
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
            footprint.sprite = active;
            isReset = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        footprint.sprite = deActive;
    }

    private void ResetFootprints()
    {
        footprint.sprite = deActive;
        _helper.ResetFoot();
    }
}
