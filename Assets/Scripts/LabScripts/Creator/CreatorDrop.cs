using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CreatorDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] TextMeshProUGUI satiety_text;
    [SerializeField] TextMeshProUGUI screwdriver_text;
    [SerializeField] TextMeshProUGUI parts_text;
    void Awake()
    {
        UpdateText();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged_resource = eventData.pointerDrag;
        if (eventData.pointerDrag != null && dragged_resource.CompareTag("ResourceOnFloor"))
        {
            ResourceOnFloor resourceOnFloorScript = dragged_resource.GetComponent<ResourceOnFloor>();
            if (resourceOnFloorScript.type == "food")
            {
                if (resourceOnFloorScript.id == 0) EnvironmentManager.creator_hunger += 1;
                if (resourceOnFloorScript.id == 1) EnvironmentManager.creator_hunger += 2;
                if (resourceOnFloorScript.id == 2) EnvironmentManager.creator_hunger += 3;
                UpdateText();
                Destroy(dragged_resource);
            }
            else if (resourceOnFloorScript.type == "city")
            {
                if (resourceOnFloorScript.id == 0) EnvironmentManager.creator_resouce[0] += 1;
                if (resourceOnFloorScript.id == 1) EnvironmentManager.creator_resouce[1] += 1;
                UpdateText();
                Destroy(dragged_resource);
            }
        }
    }
    public void UpdateText()
    {
        satiety_text.text = EnvironmentManager.creator_hunger.ToString();
        screwdriver_text.text = EnvironmentManager.creator_resouce[0].ToString();
        parts_text.text = EnvironmentManager.creator_resouce[1].ToString();
    }
}