using System;
using Swimmer.Library.View;
using UnityEngine;
using UnityEngine.UI;

namespace Swimmer.Library.Dialogs
{
    public class ThreeButtonDialog : Dialog
    {
        public delegate void NegativeHandler();

        public delegate void NeutralHandler();

        public delegate void PositiveHandler();

        [SerializeField] public Button _negativeButton;
        [SerializeField] public Button _neutralButton;

        [SerializeField] public Button _positiveButton;
        [SerializeField] private LocalizedText _txtMessage;
        [SerializeField] private LocalizedText _txtTitle;
        private NegativeHandler OnNegative;
        private NeutralHandler OnNeutral;

        private PositiveHandler OnPositive;

        protected override void Start()
        {
            base.Start();

            _positiveButton.onClick.AddListener(OnPositiveClicked);
            _neutralButton.onClick.AddListener(OnNeutralClicked);
            _negativeButton.onClick.AddListener(OnNegativeClicked);
        }

        public ThreeButtonDialog SetTitle(string textKey)
        {
            _txtTitle.SetText(textKey);
            return this;
        }

        public ThreeButtonDialog SetTitle(string textKey, params object[] args)
        {
            _txtTitle.SetText(textKey, args);
            return this;
        }

        public ThreeButtonDialog SetMessage(string textKey)
        {
            _txtMessage.SetText(textKey);
            return this;
        }

        public ThreeButtonDialog SetMessage(string textKey, params object[] args)
        {
            _txtMessage.SetText(textKey, args);
            return this;
        }

        public ThreeButtonDialog SetMessage(string delimiter, string[] textKeys)
        {
            _txtMessage.SetMultiText(delimiter, textKeys);
            return this;
        }

        public ThreeButtonDialog SetMessage(string delimiter, params Tuple<string, object[]>[] textData)
        {
            _txtMessage.SetMultiText(delimiter, textData);
            return this;
        }

        public ThreeButtonDialog SetPositiveEnabled(bool buttonEnabled)
        {
            _positiveButton.gameObject.SetActive(buttonEnabled);
            return this;
        }

        public ThreeButtonDialog AddPositiveHandler(PositiveHandler positiveHandler)
        {
            OnPositive += positiveHandler;
            return this;
        }

        public ThreeButtonDialog RemovePositiveHandler(PositiveHandler positiveHandler)
        {
            OnPositive -= positiveHandler;
            return this;
        }

        public ThreeButtonDialog SetNeutralEnabled(bool buttonEnabled)
        {
            _neutralButton.gameObject.SetActive(buttonEnabled);
            return this;
        }

        public ThreeButtonDialog AddNeutralHandler(NeutralHandler neutralHandler)
        {
            OnNeutral += neutralHandler;
            return this;
        }

        public ThreeButtonDialog RemoveNeutralHandler(NeutralHandler neutralHandler)
        {
            OnNeutral -= neutralHandler;
            return this;
        }
        
        public ThreeButtonDialog RemoveHideArea()
        {
            base.DisableHideArea();
            return this;
        }

        public ThreeButtonDialog SetNegativeEnabled(bool buttonEnabled)
        {
            _negativeButton.gameObject.SetActive(buttonEnabled);
            return this;
        }

        public ThreeButtonDialog AddNegativeHandler(NegativeHandler negativeHandler)
        {
            OnNegative += negativeHandler;
            return this;
        }

        public ThreeButtonDialog RemoveNegativeHandler(NegativeHandler negativeHandler)
        {
            OnNegative -= negativeHandler;
            return this;
        }

        public virtual void OnPositiveClicked()
        {
            Debug.Log("[CommonDialogController] OnPositiveClicked");
            if (OnPositive != null)
                OnPositive();
            Hide();
        }

        public void OnNeutralClicked()
        {
            Debug.Log("[CommonDialogController] OnNeutralClicked");
            if (OnNeutral != null)
                OnNeutral();
            Hide();
        }

        public void OnNegativeClicked()
        {
            Debug.Log("[CommonDialogController] OnNegativeClicked");
            if (OnNegative != null)
                OnNegative();
            Hide();
        }
    }
}