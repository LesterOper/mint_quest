using System;
using App;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Swimmer.Library.Dialogs
{
    public class Dialog : MonoBehaviour
    {
        public delegate void DialogHiddenHandler(GameObject dialog);

        public delegate void DialogShownHandler(GameObject dialog);

        [SerializeField] protected Button _closeButton;
        [SerializeField] private Button _hideDialogButtonArea;

        public bool Visible { get; private set; }

        private event DialogShownHandler OnDialogShown;
        private event DialogHiddenHandler OnDialogHidden;

        protected virtual void Start()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(() =>
                { 
                    Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                    Hide();
                });

            if (_hideDialogButtonArea != null)
                _hideDialogButtonArea.onClick.AddListener(Hide);

            if (GetComponent<Button>() != null)
                GetComponent<Button>().onClick.AddListener(() =>
                { 
                    Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                    Hide();
                });
        }

        public virtual void Show(RectTransform parentTransform)
        {
            if (!Visible)
            {
                gameObject.SetActive(true);
                GetComponent<RectTransform>().SetParent(parentTransform);
                Visible = true;
                NotifyListeners();
            }
        }

        public void SetOnTopCanvas()
        {
            transform.parent = GetComponentInParent<Canvas>().transform;
            transform.SetAsLastSibling();
        }

        public virtual void Hide()
        {
           
            if (Visible)
            {
                //Context.SoundPlayer.PlaySoundClick("click_2");
                gameObject.SetActive(false);
                Visible = false;
                NotifyListeners();
                Destroy(gameObject);
               // Invoke(nameof(Resources.UnloadUnusedAssets), 1f);
            }
        }

        public void ForceHide()
        {
            //if (Visible)
            //{
                //Context.SoundPlayer.PlaySoundClick("click_2");
                gameObject.SetActive(false);
                Visible = false;
                NotifyListeners();
                Destroy(gameObject);
            //}
        }

        protected void NotifyListeners()
        {
            if (Visible)
            {
                if (OnDialogShown != null)
                    OnDialogShown(gameObject);
            }
            else
            {
                if (OnDialogHidden != null)
                    OnDialogHidden(gameObject);
            }
        }

        public Dialog AddShownHandler(DialogShownHandler dialogShownHandler)
        {
            OnDialogShown += dialogShownHandler;
            return this;
        }

        public Dialog RemoveShownHandler(DialogShownHandler dialogShownHandler)
        {
            OnDialogShown -= dialogShownHandler;
            return this;
        }

        public virtual Dialog AddHiddenHandler(DialogHiddenHandler dialogHiddenHandler)
        {
            OnDialogHidden += dialogHiddenHandler;
            return this;
        }

        public virtual Dialog RemoveHiddenHandler(DialogHiddenHandler dialogHiddenHandler)
        {
            OnDialogHidden -= dialogHiddenHandler;
            return this;
        }
        
        public  Dialog DisableHideArea()
        {
            _hideDialogButtonArea.enabled = false;
            return this;
        }
    }
}