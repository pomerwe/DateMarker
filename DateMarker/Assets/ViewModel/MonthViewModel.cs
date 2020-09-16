using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonthViewModel : MonoBehaviour, IPointerClickHandler
{
    public Text monthName;
    public Month month;

    public Action<MonthViewModel> setCurrentMonth;

    public void SetModel(Month month, Action<MonthViewModel> setCurrentMonth)
    {
        this.setCurrentMonth = setCurrentMonth;
        this.month = month;
        monthName.text = month.MonthName;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        setCurrentMonth.Invoke(this);
    }
}
