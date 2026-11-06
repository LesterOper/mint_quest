using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class CubeDecompozition : MonoBehaviour
{
    [SerializeField] private SpriteRenderer scan;
    [SerializeField] private GameObject correct;
    [SerializeField] private List<GameObject> boxes;

    public void DetectCorrect(GameObject tapped)
    {
        if (tapped == correct)
        {
            boxes.Remove(correct);
            boxes.ForEach(obj =>
            {
                obj.GetComponent<SpriteRenderer>().DOFade(0, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    correct.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear);
                    scan.DOFade(0, 0.3f).SetEase(Ease.Linear);
                });
            });
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);

            ShuffleBoxes();
        }
    }

    private void ShuffleBoxes()
    {
        boxes.ForEach(obj => { obj.transform.DOScale(0, 0.35f).SetEase(Ease.Linear); });
        var buf = new List<GameObject>(boxes);
        for (int i = 0; i < boxes.Count; i++)
        {
            int randOne = Random.Range(0, buf.Count);
            int randTwo = Random.Range(0, buf.Count);

            var pos = buf[randOne].transform.position;
            buf[randOne].transform.position = buf[randTwo].transform.position;
            buf[randTwo].transform.position = pos;
        }
        
        boxes.ForEach(obj => { obj.transform.DOScale(0.75f, 0.35f).SetEase(Ease.Linear); });
    }
}
