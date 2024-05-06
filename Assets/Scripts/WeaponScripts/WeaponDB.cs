using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDB", menuName = "Prototype-1/WeaponDB", order = 0)]
public class WeaponDB : ScriptableObject
{
    public List<GameObject> greenWeaponList;
    public List<GameObject> greenWeaponOnFloorList;
    public List<GameObject> yellowWeaponList;
    public List<GameObject> yellowWeaponOnFloorList;
    public List<GameObject> redWeaponList;
    public List<GameObject> redWeaponOnFloorList;
}