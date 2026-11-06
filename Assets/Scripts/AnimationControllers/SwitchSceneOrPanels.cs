using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchSceneOrPanels : MonoBehaviour, IAnimate
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject mintScreen;
    [SerializeField] private GameObject btnPlay;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject listLevels;
    [SerializeField] private GameObject currecntLevel;
    public void setupAnim()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Join(mainMenu.GetComponent<Image>().DOFade(0f, 2.5f));
        sequence.Join(mainMenu.transform.DOMoveX(400f, 2.5f));
        sequence.Join(mintScreen.transform.DOMoveX(-400f, 2.5f));
        sequence.Join(mintScreen.transform.DOMoveX(0, 2.5f));
        sequence.Join(mainMenu.transform.DOMoveX(0, 2.5f));
        sequence.Join(mintScreen.GetComponent<Image>().DOFade(1, 2f));
        sequence.Join(mainMenu.GetComponent<CanvasGroup>().DOFade(0, 2f));
        sequence.SetEase(Ease.Linear);
        Invoke(nameof(ToLevelScene), 2.7f);
    }

    private void ToLevelScene()
    {
        SceneManager.LoadScene("level_scene");
    }
}
