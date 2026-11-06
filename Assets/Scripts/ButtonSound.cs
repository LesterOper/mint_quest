using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.UI;

namespace App.Sound
{
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private string _buttonSound;
        private void Start()
        {
            foreach (var btn in GetComponentsInChildren<Button>())
            {
                btn.onClick.AddListener(PlaySound);
            }
        }

        private void PlaySound()
        {
            Context.SoundPlayer.PlaySound(_buttonSound);
        }

        public void ListenChildren(GameObject obj)
        {
            foreach (var button in obj.GetComponentsInChildren<Button>())
            {
                button.onClick.AddListener(PlaySound);
            }
        }

        public void RemoveListener(GameObject obj)
        {
            foreach (var button in obj.GetComponentsInChildren<Button>())
            {
                button.onClick.RemoveListener(PlaySound);
            }
        }
    }
}
