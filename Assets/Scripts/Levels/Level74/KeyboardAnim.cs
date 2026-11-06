using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class KeyboardAnim : MonoBehaviour
{
    [SerializeField] private List<RectTransform> buttons;
    private List<Vector2> startPoses;
    private float width, height;


    private void Awake()
    {
        width = Screen.width;
        height = Screen.height;
        startPoses = new List<Vector2>();
        buttons.ForEach(obj =>
        {
            startPoses.Add(obj.anchoredPosition);
        });
        OffAnim(0, 0);
    }

    private void Start()
    {
       
    }

    public void OffAnim(int index, float time)
    {
        if (index == buttons.Count) return;
        
        float randX, randY;
        int rand = Random.Range(0, 10);

        if (rand < 5)
        {
            randX = Random.Range(-width - 300, -width-100);
            randY = Random.Range(-height - 300, -height-100);
               
        }
        else
        {
            randX = Random.Range(width+100, width + 300);
            randY = Random.Range(height+100, height + 300);
        }

        buttons[index].DOAnchorPos(new Vector2(randX, randY), time).SetEase(Ease.OutCubic);
        index = index + 1;
            OffAnim(index, time);
    }
    
    public void OnAnim()
    {
        int i = 0;
        buttons.ForEach(obj =>
        {
            obj.DOAnchorPos(startPoses[i], 0.35f).SetEase(Ease.OutCubic);
            i++;
        });
    }
}
