using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEventView : MonoBehaviour
{
  public float translateRate = 0.3f;

  bool isEnabled = false;
  public void Update()
  {
    if(isEnabled)
    {
      Ascend();
    }
    else
    {
      Descend();
    }
  }
  public void Open()
  {
    isEnabled = true;
  }
  public void Close()
  {
    isEnabled = false;
  }

  public void Descend()
  {
    var transform = GetComponent<RectTransform>();
    var height = transform.sizeDelta.y;
    if (transform.anchoredPosition.y <= -height)
    {
      transform.anchoredPosition = new Vector2(0, -height);
    }
    else
    {
      transform.Translate(new Vector2(0, -translateRate));
    }
  }

  public void Ascend()
  {
    var transform = GetComponent<RectTransform>();
    if (transform.anchoredPosition.y >= 0)
    {
      transform.anchoredPosition = new Vector2(0, 0);
    }
    else
    {
      transform.Translate(new Vector2(0, translateRate));
    }
  }

}
