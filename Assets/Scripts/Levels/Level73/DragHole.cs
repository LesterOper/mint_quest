using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class DragHole : MonoBehaviour, IDraggableItems
{
    [SerializeField] private CircleCollider2D _collider2D;
    [SerializeField] private List<BallsController> balls;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PolygonCollider2D polygon;
    private int count = 0;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        _collider2D.enabled = false;
        _rigidbody.simulated = false;
        balls.ForEach(obj => { obj.enabled = true; });
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }

    public void IncrementBallsCounter()
    {
        count++;
        if (count == 2)
        {
            polygon.enabled = false;
            Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL);
        }
    }
}
