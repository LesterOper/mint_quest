using System;
using System.Collections;
using DG.Tweening;
using Swimmer.Library;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Logo
{
    public class LogoController : MonoBehaviour
    {
        [SerializeField] private GameObject _circleObject;
        [SerializeField] private GameObject _logoObject;
        [Space]
        [SerializeField] private Image _imageProgress;
        [SerializeField] private TextMeshProUGUI _progressText;
        [Space]
        [SerializeField] private float _duration = 1;
        
        private AsyncOperation _asyncLoad;

        

        void Start()
        {
            MenuUtils.ResetMenuLoadWithSelectDialog();
            
            _progressText.text = "0";
            _imageProgress.fillAmount = 0;
            
            DOTween.To(() => _imageProgress.fillAmount, x => _imageProgress.fillAmount = x, 1f, _duration).OnComplete((() => 
                {
                    Animation();
                }));

            StartCoroutine(LoadMainSceneAsync());
        }

        private void Update()
        {
            _progressText.text = "" + (int)(_imageProgress.fillAmount * 100);
        }


        private IEnumerator LoadMainSceneAsync()
        {
            PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 0);
            _asyncLoad = SceneManager.LoadSceneAsync("main_menu");
            _asyncLoad.allowSceneActivation = false;

            yield return null;
        }
        
        
        private void Animation()
        {
            Sequence animCircle = DOTween.Sequence();

            animCircle.Append(_circleObject.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -263), 0.4f).SetEase(Ease.InQuint));
            animCircle.AppendCallback(() => FindObjectOfType<Animation>().Play("Logo_anim"));
            animCircle.Append(_circleObject.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, -360) * 3, 2.5f, RotateMode.FastBeyond360));
            animCircle.Join(_circleObject.GetComponent<RectTransform>().DOJumpAnchorPos(new Vector2(800, -300), 100f, 1, 1.5f).SetEase(Ease.OutQuad));
            
            animCircle.InsertCallback(2.2f,() => _asyncLoad.allowSceneActivation = true);
        }
        
        
    }
}