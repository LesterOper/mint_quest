using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class CubePieceController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private CubeHelper _cubeHelper;
    [SerializeField] private DragController _dragController;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite blackSprite;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        _dragController.enabled = false;
        _cubeHelper.IncrementConnectedPieces();
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeSprite()
    {
        _renderer.DOFade(0.3f, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _renderer.sprite = blackSprite;
            _renderer.DOFade(1, 0.2f).SetEase(Ease.Linear).SetEase(Ease.Linear);
        });
    }
    
    public void FadeSprite()
    {
        _renderer.DOFade(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
