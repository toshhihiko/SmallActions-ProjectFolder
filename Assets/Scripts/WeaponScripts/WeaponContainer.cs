using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponContainer : MonoBehaviour, IDropHandler
{
    [SerializeField] WeaponSlot WeaponSlotScript;
    [SerializeField] int container_number;
    private WeaponOnFloor weaponOnFloorScript;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dragged_weapon = eventData.pointerDrag;
            if (dragged_weapon != null && dragged_weapon.CompareTag("WeaponOnFloor"))
            {
                dragged_weapon.transform.SetParent(transform);
                dragged_weapon.GetComponent<RectTransform>().localPosition = Vector3.zero;
                //dragged_weapon.GetComponent<RectTransform>().localScale *= 2;
                dragged_weapon.tag = "Untagged";

                weaponOnFloorScript = dragged_weapon.GetComponent<WeaponOnFloor>();
                WeaponSlotScript.EquipWeapon(container_number, weaponOnFloorScript.type, weaponOnFloorScript.id);
                weaponOnFloorScript.OnWeaponContainer += RemoveWeapon;
            }
        }
    }
    private void RemoveWeapon()
    {
        WeaponSlotScript.RemoveWeapon(container_number);
        weaponOnFloorScript.OnWeaponContainer -= RemoveWeapon;
    }
}