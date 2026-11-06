using System;
using Library.Dialogs;
using Swimmer.Library.Common;
using UnityEngine;

namespace Swimmer.Library.Dialogs
{
    public class DialogsContainer : MonoBehaviour
    {
        [SerializeField] protected RectTransform _parentRectTransform;

        [SerializeField] private GameObject _commonDialogPrefab;

        [SerializeField] private GameObject _progressLayoutPrefab;
        [SerializeField] private GameObject _toastPrefab;

        protected DialogController _dialogController;
        private GameObject _progressLayout;
        private GameObject _currentToast;
        


        protected void Awake()
        {
            if (_parentRectTransform == null)
                _parentRectTransform = GameObject.FindWithTag("UiParentRect").GetComponent<RectTransform>();
         
            _dialogController = new DialogController(_parentRectTransform);
        }

        private void Start()
        {
        }

        public bool HasActiveDialog()
        {
            return _dialogController.HasActiveDialog();
        }

        public bool OnBackKey()
        {
            if (_dialogController.HasActiveDialog())
            {
                _dialogController.CloseTopDialog();
                return true;
            }
            return false;
        }

        public void ShowDialog(GameObject dialog)
        {
            _dialogController.ShowDialog(dialog);
        }

        public void AddCountChangeListener(DialogController.CountChangeHandler countChangeHandler)
        {
            _dialogController.OnCountChanged += countChangeHandler;
        }

        public void RemoveCountChangeListener(DialogController.CountChangeHandler countChangeHandler)
        {
            _dialogController.OnCountChanged -= countChangeHandler;
        }


        // ==== COMMON DIALOGS ====

        public ThreeButtonDialog ShowTwoButtonsDialog(string titleKey, string messageKey,
            ThreeButtonDialog.PositiveHandler positiveHandler, ThreeButtonDialog.NegativeHandler negativeHandler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey)
                .SetMessage(messageKey)
                .SetNeutralEnabled(false)
                .AddPositiveHandler(positiveHandler)
                .AddNegativeHandler(negativeHandler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }
        
        public ThreeButtonDialog ShowTwoButtonsDialog(string titleKey, string messageKey,bool withHideArea,
            ThreeButtonDialog.PositiveHandler positiveHandler, ThreeButtonDialog.NegativeHandler negativeHandler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey)
                .RemoveHideArea()
                .SetMessage(messageKey)
                .SetNeutralEnabled(false)
                .AddPositiveHandler(positiveHandler)
                .AddNegativeHandler(negativeHandler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }

        public ThreeButtonDialog ShowTwoButtonsDialog(string titleKey, object[] titleArgs, string messageKey,
            object[] messageArgs,
            ThreeButtonDialog.PositiveHandler positiveHandler, ThreeButtonDialog.NegativeHandler negativeHandler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey, titleArgs)
                .SetMessage(messageKey, messageArgs)
                .SetNeutralEnabled(false)
                .AddPositiveHandler(positiveHandler)
                .AddNegativeHandler(negativeHandler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }

        public ThreeButtonDialog ShowTwoButtonsDialog(string titleKey, string delimiter, string[] messageKeys,
            ThreeButtonDialog.PositiveHandler positiveHandler, ThreeButtonDialog.NegativeHandler negativeHandler = null,
            Dialog.DialogHiddenHandler hiddenHandler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey)
                .SetMessage(delimiter, messageKeys)
                .SetNeutralEnabled(false)
                .AddPositiveHandler(positiveHandler)
                .AddNegativeHandler(negativeHandler)
                .AddHiddenHandler(hiddenHandler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }

        public ThreeButtonDialog ShowTwoButtonsDialog(string titleKey, string delimiter,
            Tuple<string, object[]>[] messageData,
            ThreeButtonDialog.PositiveHandler positiveHandler, ThreeButtonDialog.NegativeHandler negativeHandler = null,
            Dialog.DialogHiddenHandler hiddenHandler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey)
                .SetMessage(delimiter, messageData)
                .SetNeutralEnabled(false)
                .AddPositiveHandler(positiveHandler)
                .AddNegativeHandler(negativeHandler)
                .AddHiddenHandler(hiddenHandler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }

        public ThreeButtonDialog ShowOneButtonDialog(string titleKey, string messageKey,
            ThreeButtonDialog.NeutralHandler handler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey)
                .SetMessage(messageKey)
                .SetPositiveEnabled(false)
                .SetNegativeEnabled(false)
                .AddNeutralHandler(handler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }

        public ThreeButtonDialog ShowOneButtonDialog(string titleKey, object[] titleArgs, string messageKey,
            object[] messageArgs,
            ThreeButtonDialog.NeutralHandler handler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey, titleArgs)
                .SetMessage(messageKey, messageArgs)
                .SetPositiveEnabled(false)
                .SetNegativeEnabled(false)
                .AddNeutralHandler(handler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }

        public ThreeButtonDialog ShowOneButtonDialog(string titleKey, string delimiter, string[] messageKeys,
            ThreeButtonDialog.NeutralHandler handler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey)
                .SetMessage(delimiter, messageKeys)
                .SetPositiveEnabled(false)
                .SetNegativeEnabled(false)
                .AddNeutralHandler(handler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }

        public ThreeButtonDialog ShowOneButtonDialog(string titleKey, string delimiter,
            Tuple<string, object[]> messageData,
            ThreeButtonDialog.NeutralHandler handler = null)
        {
            var dialog = Instantiate(_commonDialogPrefab);
            dialog.GetComponent<ThreeButtonDialog>()
                .SetTitle(titleKey)
                .SetMessage(delimiter, messageData)
                .SetPositiveEnabled(false)
                .SetNegativeEnabled(false)
                .AddNeutralHandler(handler);
            ShowDialog(dialog);
            return dialog.GetComponent<ThreeButtonDialog>();
        }


        // ==== PROGRESS ====

        public void ShowProgress()
        {
            if (_progressLayout == null)
                _progressLayout = Instantiate(_progressLayoutPrefab,
                    FindObjectOfType<Canvas>().GetComponent<RectTransform>());
        }

        public void HideProgress()
        {
            if (_progressLayout != null)
            {
                _progressLayout.SetActive(false);
                Destroy(_progressLayout);
                _progressLayout = null;
            }
        }


        // ==== TOAST ====

        public void ShowToast(string textKey)
        {
            if (_currentToast != null) _currentToast.GetComponent<Toast>().Hide();

            _currentToast = Instantiate(_toastPrefab);
            _currentToast.GetComponent<Toast>().Show(_parentRectTransform, textKey);
            var rectTransform = _currentToast.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
        }

        public void ShowToast(string textKey, object objects)
        {
            if (_currentToast != null) _currentToast.GetComponent<Toast>().Hide();

            _currentToast = Instantiate(_toastPrefab);
            _currentToast.GetComponent<Toast>().Show(_parentRectTransform, textKey, objects);
            var rectTransform = _currentToast.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}