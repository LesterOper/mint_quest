using System;
using System.Linq;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Swimmer.Library.View
{
    [RequireComponent(typeof(Image))]
    public class LocalizedImage : MonoBehaviour
    {
        [SerializeField] private LocalizedImageData[] _imageDatas;

        private void Start()
        {
            UpdateSprite();
        }

        public void UpdateSprite()
        {
            var image = GetComponent<Image>();

            var currLocale = Context.LocaleHelper.GetCurrentLocaleKey();
            var imageFound = false;

            foreach (var data in _imageDatas)
                if (data.LocaleString.Equals(currLocale))
                {
                    image.sprite = data.Sprite;
                    image.SetNativeSize();
                    imageFound = true;
                    break;
                }

            if (!imageFound) image.sprite = _imageDatas[0].Sprite;
        }
    }

    [Serializable]
    public class LocalizedImageData
    {
        [SerializeField] private string _localeString;
        [SerializeField] private Sprite _sprite;

        public string LocaleString => _localeString;

        public Sprite Sprite => _sprite;
    }
}