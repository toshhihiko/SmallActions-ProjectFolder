using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FarmDrop : MonoBehaviour, IDropHandler
{
    [SerializeField] int index;
    [SerializeField] Image imageTop;
    private Image imageBottom;
    [SerializeField] Sprite[] imageTops = new Sprite[3];
    [SerializeField] Sprite[] imageBottoms = new Sprite[3];
    void Awake()
    {
        imageBottom = GetComponent<Image>();
        imageTop.sprite = imageTops[EnvironmentManager.soilStatusList[index]];
        imageBottom.sprite = imageBottoms[EnvironmentManager.soilStatusList[index]];
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged_resource = eventData.pointerDrag;
        if (eventData.pointerDrag != null && dragged_resource.CompareTag("ResourceOnFloor"))
        {
            ResourceOnFloor resourceOnFloorScript = dragged_resource.GetComponent<ResourceOnFloor>();
            if (resourceOnFloorScript.type == "nature" && resourceOnFloorScript.id == EnvironmentManager.soilStatusList[index])
            {
                EnvironmentManager.soilStatusList[index] += 1;
                imageTop.sprite = imageTops[EnvironmentManager.soilStatusList[index]];
                imageBottom.sprite = imageBottoms[EnvironmentManager.soilStatusList[index]];
                Destroy(dragged_resource);
            }
        }
    }
}