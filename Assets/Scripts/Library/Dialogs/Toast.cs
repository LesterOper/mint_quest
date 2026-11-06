using System.Collections.Generic;
using I2.Loc;
using UnityEngine;

namespace Library.Dialogs
{
    public class Toast : MonoBehaviour
    {
        [SerializeField] private Localize _messageText;
        public void Show(RectTransform parentTransform, string textKey)
        {
            GetComponentInChildren<Localize>().SetTerm(textKey);
            GetComponent<RectTransform>().SetParent(parentTransform);
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.localPosition = new Vector3(0, 0, 0);
            gameObject.SetActive(true);
            StartCoroutine(HideAfterDelay());
        }

        public void Show(RectTransform parentTransform, string textKey, object objects)
        {
            _messageText.SetTerm(textKey);
            _messageText.GetComponent<LocalizationParamsManager>().SetParameterValue("VALUE",
                objects.ToString());
            GetComponent<RectTransform>().SetParent(parentTransform);
            var rectTransform = GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.localPosition = new Vector3(0, 0, 0);
            gameObject.SetActive(true);
            StartCoroutine(HideAfterDelay());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        private IEnumerator<WaitForSeconds> HideAfterDelay()
        {
            yield return new WaitForSeconds(2f);
            Hide();
        }
    }
}