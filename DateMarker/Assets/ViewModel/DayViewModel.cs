﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayViewModel : MonoBehaviour
{
    public Image dayBackground;
    public Text dayNumber;
    public Day day;

    private Color32 todayColor = new Color32(210, 228, 255, 188);
    private Color32 normalColor = new Color32(171, 171, 171, 49);
    private Color32 hasEventColor = new Color32(255, 248, 150, 188);
    private Color32 fillColor = new Color32(0, 0, 0, 0);
    private Color32 fillTextColor = new Color32(155, 155, 155, 255);

    private bool isToday;
    private bool isFill = true;

    public void Update()
    {
        if(!isFill)
        {
            isToday = DateTime.Now.Day == day.DayNumber && DateTime.Now.Year == day.Year && DateTime.Now.Month == day.Month;
            
        }
        SetDayBackgroundColor();
    }

    public void SetDayBackgroundColor()
    {
        if(isFill)
        {
            dayNumber.color = fillTextColor;
            dayBackground.color = fillColor;
        }
        else
        {
            if (isToday)
            {
                dayBackground.color = todayColor;
            }
            else
            {
                dayBackground.color = normalColor;
            }
        }
    }

    public void SetModel(Day day)
    {
        isFill = false;

        this.day = day;
        dayNumber.text = day.DayNumber.ToString();
    }

    public void SetFillNumber(int number)
    {
        dayNumber.text = number.ToString();
    }
}