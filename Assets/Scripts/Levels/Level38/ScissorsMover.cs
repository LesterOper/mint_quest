using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class ScissorsMover : MonoBehaviour, IDraggableItems
{
    private GameObject camera;
    private GameObject BackGround;
    [SerializeField] private SwipeScreen _swipeScreen;
    [SerializeField] private GameObject backMint;

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.ToString() == "ScissorsCutPlace")
        {
            GetComponent<DragController>().enabled = false;
            _swipeScreen.gameObject.SetActive(false);
            BringCloserScissorsToCutPlace();
        }
    }

    public void BringCloserScissorsToCutPlace()
    {
        Context.SoundPlayer.PlaySoundClick("right_do");
        transform.DOMove(new Vector3(-0.14f, -11.05f, 0), 0.5f).OnComplete(() =>
        {
            //Context.SoundPlayer.PlaySoundClick("scisssors_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            GetComponent<Animation>().Play();
        });
    }

    public void DropDownScissorsAndRope() //use in animation!
    {
        GameObject _treadsBottom = GameObject.Find("threadsBottom");
        GameObject _end_button = GameObject.Find("end_button");
        float _durationDropDown = 2f;
        transform.DOMoveY(transform.position.y-5f,_durationDropDown).SetEase(Ease.Flash);
        _end_button.transform.DOMoveY(_end_button.transform.position.y-5f,_durationDropDown).SetEase(Ease.Flash);
        _treadsBottom.transform.DOMoveY(_treadsBottom.transform.position.y-2f, _durationDropDown);
    }

    public void MoveCameraToBaloons() //use in animation!
    {
        Vector3 _centerBaloonPosition = GameObject.Find("centerBaloonPosition").transform.position;
        GameObject.Find("threadSimple1").GetComponent<Animation>().Play();
        GameObject.Find("threadSimple3").GetComponent<Animation>().Play();
        camera.transform.DOMoveY(_centerBaloonPosition.y, 2f).SetEase(Ease.Flash).OnComplete(FinishLevel);
        BackGround.transform.DOMoveY(_centerBaloonPosition.y, 2f).SetEase(Ease.Flash);
    }

    public void FinishLevel()
    {
        GameObject.FindWithTag("BaloonsWithThreads").gameObject.transform.DOMoveY(10f, 3f);
        camera.transform.DOMoveY(0f, 3f);
        Invoke("SpecialEffect", 2f);
    }
    
    public void moveItem(Vector2 pos)
    {
        // Debug.Log("!!!!");
        // transform.DOMove(pos, 0.15f).SetEase(Ease.Linear);
        // SpecialEffect();
    }

    public void attachToParentObject()
    {
    }
    
    private void SpecialEffect()
    {
        backMint.transform.DOMoveY(0f, 2f);
        BackGround.transform.DOMoveY(0f, 2f);
        camera.transform.DOMoveY(0f, 2.2f)
            .OnComplete(() => Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM));
        CancelInvoke("SpecialEffect");
    }

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        BackGround = GameObject.FindWithTag("Background");
    }
}
