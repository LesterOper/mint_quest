using System;
using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class ClickArrows : MonoBehaviour, IClickItem
{
    [SerializeField] private BallWayController _controller;
    [SerializeField] private List<float> positions;
    [SerializeField] private string dir;
    private float[] posY = new float[]{1.72f, 0.672f};
    private int Xindex = 0;
    private int Yindex = 0;
    private bool isDrag = true;

    private void Start()
    {
        
    }

    public void tapOnItem()
    {
        if (isDrag)
        {
            isDrag = false;
            return;
        }
        switch (dir)
        {
            case "RD":
                if (Xindex == 1)
                {
                    if (_controller.Move("R", positions[Xindex], 0))
                    {
                        Xindex++;
                        //Yindex++;
                    }
                }
                else
                {
                    if (_controller.Move("RD", positions[Xindex], posY[Yindex]))
                    {
                        Xindex++;
                        Yindex++;
                    }
                }
                break;
            case "U" :
                if (_controller.Move("U", positions[Xindex], 0))
                {
                    Xindex++;
                }
                break;
            case "L" :
                if (_controller.Move("L", positions[Xindex], 0))
                {
                    Xindex++;
                }
                break;
        }
    }
}
