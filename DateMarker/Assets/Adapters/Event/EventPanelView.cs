using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventPanelView : MonoBehaviour
{
    public GameObject eventContent;
    public RectTransform eventContentRect;
    public GameObject eventPrefab;
    public List<EventViewModel> eventViews = new List<EventViewModel>();

    public EventViewModel selectedEventViewModel;

    private float currentEventContentY = 0;

    public void Start()
    {
        List<DateMarkerEvent> list = new List<DateMarkerEvent>() { new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
             new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
             new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
            new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"),
           new DateMarkerEvent("Evento 1", DateTime.Now, DateTime.Now, "Teste"), };

        FillEventScroll(list);
        var firstEvent = eventViews.FirstOrDefault();
        firstEvent?.SetSelected();
    }

    public void Update()
    {
        if(currentEventContentY != eventContentRect.anchoredPosition.y)
        {
            TranslateContent();
        }
        else
        {
            SetContentYAccordinglySelectedView();
        }
    }

    public void ClearEventScroll()
    {
        eventViews.ForEach(v => Destroy(v.gameObject));
        eventViews.Clear();
        currentEventContentY = 0;
        eventContentRect.localPosition = new Vector2(0, -392.0641f);
        eventContentRect.sizeDelta = Vector2.zero;
    }
    public void FillEventScroll(List<DateMarkerEvent> dateMarkerEvents)
    {
        ClearEventScroll();
        dateMarkerEvents.ForEach(d =>
        {
            var eventView = InstantiateEventPrefab();
            eventView.SetModel(d, OnClickEvent);
        });
        var firstEvent = eventViews.FirstOrDefault();
        firstEvent?.SetSelected();
    }

    public EventViewModel InstantiateEventPrefab()
    {
        var instance = Instantiate(eventPrefab, eventContent.transform, false);
        eventViews.Add(instance.GetComponent<EventViewModel>());
        return instance.GetComponent<EventViewModel>();
    }

    public void OnClickEvent(EventViewModel selectedEvent)
    {
        selectedEventViewModel = selectedEvent;
        FormatEventViewAppearance();
    }

    public void FormatEventViewAppearance()
    {
        UnSelectAllViews();
        int index = eventViews.IndexOf(selectedEventViewModel);

        selectedEventViewModel.isSelected = true;
        selectedEventViewModel.SelectedAppearance();
        if (eventViews.Count > 1)
        {
            if (index == 0)
            {
                eventViews[1].SecondAppearance();
            }
            else if (index == eventViews.Count - 1)
            {
                eventViews[index - 1].SecondAppearance();
            }
            else
            {
                eventViews[index - 1].SecondAppearance();
                eventViews[index + 1].SecondAppearance();
            }
        }
        Invoke("SetContentYAccordinglySelectedView", 0.05f);
    }

    public void SetContentYAccordinglySelectedView()
    {
        var selectedEventViewModelRect = selectedEventViewModel.GetComponent<RectTransform>();
        currentEventContentY = -selectedEventViewModelRect.localPosition.y;
    }

    public void TranslateContent()
    {
        if (eventContentRect.localPosition.y - currentEventContentY > 3 || eventContentRect.localPosition.y - currentEventContentY < -3)
        {
            if (eventContentRect.localPosition.y >= currentEventContentY)
            {
                eventContentRect.Translate(new Vector2(0, -3f));
            }
            if (eventContentRect.localPosition.y <= currentEventContentY)
            {
                eventContentRect.Translate(new Vector2(0, 3f));
            }
        }
    }

    public void DragContent(BaseEventData eventData)
    {
        eventContent.transform.localPosition = new Vector2(0, ((PointerEventData)eventData).position.y);
    }

    public void UnSelectAllViews()
    {
        eventViews.ForEach(v =>
        {
            v.isSelected = false;
            v.ThirdAppearance();
        });
    }
}
