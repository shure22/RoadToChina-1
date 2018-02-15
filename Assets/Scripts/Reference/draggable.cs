﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum Slot { Boil, Fry, Grill };
    public Slot typeOfItem = Slot.Boil;
    public GameObject newObj;
    private RectTransform plane;
    private Transform parentToReturnTo;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturnTo = this.transform.parent;
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
          return;
        newObj = gameObject;
        newObj.transform.SetParent(canvas.transform, false);
        newObj.transform.SetAsLastSibling();
        //var image = newObj.AddComponent<Image>();
        // The icon will be under the cursor.
        // We want it to be ignored by the event system.
        var group = newObj.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
        //image.sprite = GetComponent<Image>().sprite;
        //image.SetNativeSize();

        plane = transform as RectTransform;

        SetDraggedPosition(eventData);
  }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        //this.transform.position = eventData.position;
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
      if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
        plane = eventData.pointerEnter.transform as RectTransform;

      var rt = newObj.GetComponent<RectTransform>();
      Vector3 globalMousePos;
      if (RectTransformUtility.ScreenPointToWorldPointInRectangle(plane, eventData.position, eventData.pressEventCamera, out globalMousePos))
      {
        rt.position = globalMousePos;
        rt.rotation = plane.rotation;
      }
    }

  public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        
        newObj.transform.SetParent(parentToReturnTo);
        newObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
      if (go == null) return null;
      var comp = go.GetComponent<T>();

      if (comp != null)
        return comp;

      var t = go.transform.parent;
      while (t != null && comp == null)
      {
        comp = t.gameObject.GetComponent<T>();
        t = t.parent;
      }
      return comp;
    }


}
