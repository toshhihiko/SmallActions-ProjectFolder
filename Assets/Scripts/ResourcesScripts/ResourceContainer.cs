using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceContainer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dragged_resource = eventData.pointerDrag;
            if (eventData.pointerDrag != null && dragged_resource.CompareTag("ResourceOnFloor"))
            {
                dragged_resource.transform.SetParent(transform);
                //dragged_resource.GetComponent<RectTransform>().localScale
                dragged_resource.GetComponent<RectTransform>().localPosition = Vector3.zero;
                dragged_resource.tag = "Untagged";
            }
        }
    }
}