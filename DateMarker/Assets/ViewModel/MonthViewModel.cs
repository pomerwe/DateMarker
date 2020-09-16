using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonthViewModel : MonoBehaviour
{
    public Text monthName;
    public Month month;
    public void SetModel(Month month)
    {
        this.month = month;
        monthName.text = month.MonthName;
    }
}
