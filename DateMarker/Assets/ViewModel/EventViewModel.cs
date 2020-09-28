using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventViewModel : MonoBehaviour, IPointerClickHandler
{
    public RectTransform rectTransform;
    public bool isSelected = false;

    private Vector3 secondAppearanceValue = new Vector3(0.8f, 0.8f, 1);
    private Vector3 thirdAppearanceValue = new Vector3(0.6f, 0.6f, 1);

    public DateMarkerEvent dateMarkerEvent;

    public Action<EventViewModel> onClickAction;

    public Vector2 currentScale = new Vector2(1, 1);

    public Text eventText;

    public void Update()
    {
        ChangeAppearanceAnimation();
    }

    public void SetModel(DateMarkerEvent dateMarkerEvent, Action<EventViewModel> onClickAction)
    {
        this.dateMarkerEvent = dateMarkerEvent;
        this.onClickAction = onClickAction;

        eventText.text = $"{dateMarkerEvent.Title} - {dateMarkerEvent.Description} {dateMarkerEvent.Start.ToString("HH:mm")} - {dateMarkerEvent.End.ToString("HH: mm")}";
    }

    public void SetSelected()
    {
        onClickAction.Invoke(this);
    }

    public void SelectedAppearance()
    {
        currentScale = new Vector2(1, 1);
    }

    public void SecondAppearance()
    {
        currentScale = secondAppearanceValue;
    }
    public void ThirdAppearance()
    {
        currentScale = thirdAppearanceValue;
    }

    public void ChangeAppearanceAnimation()
    {
        Vector3 scale = new Vector3(0, 0, 0);

        if (rectTransform.localScale.x - currentScale.x > 0.01 || rectTransform.localScale.x - currentScale.x < -0.01)
        {
            if (rectTransform.localScale.x >= currentScale.x)
            {
                scale.x = -0.01f;
            }
            else if (rectTransform.localScale.x <= currentScale.x)
            {
                scale.x = 0.01f;
            }
        }

        if (rectTransform.localScale.y - currentScale.y > 0.01 || rectTransform.localScale.y - currentScale.y < -0.01)
        {
            if (rectTransform.localScale.y >= currentScale.y)
            {
                scale.y = -0.01f;
            }
            else if (rectTransform.localScale.y <= currentScale.y)
            {
                scale.y = 0.01f;
            }
        }
        rectTransform.localScale += scale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetSelected();
    }
}
