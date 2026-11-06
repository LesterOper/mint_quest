using App.Global;
using UnityEngine;
using DG.Tweening;
using Swimmer.Library;

public class ToilettPaperFinishController : MonoBehaviour
{
    [SerializeField] private GameObject _mintBackground = null;

    private void FinishLvl()
    {
        _mintBackground.transform.position = new Vector3(0f, 0f, 0f);
        _mintBackground.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f);
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "paperHole")
        {
            other.GetComponent<CircleCollider2D>().enabled = false;
            FinishLvl();
        }
    }

}
