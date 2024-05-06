using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponOnFloor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject worldCanvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 pointerOffset;

    public int id;
    public string type;

    public event Action OnWeaponContainer;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        worldCanvas = GameObject.Find("WorldCanvas");
        transform.SetParent(worldCanvas.transform);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pointerOffset);
        if (transform.parent.CompareTag("WeaponContainer"))
        {
            worldCanvas = GameObject.Find("WorldCanvas");
            transform.SetParent(worldCanvas.transform);
            tag = "WeaponOnFloor";
            OnWeaponContainer?.Invoke();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            rectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        if (!transform.parent.CompareTag("WeaponContainer"))
        {
            GameObject instantiated = Instantiate(gameObject);
            Destroy(gameObject);
        }
    }
}