using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorSaveWeapons
{
    public List<FloorSaveWeapon> floorSaveWeaponList;
}
[System.Serializable]
public class FloorSaveWeapon
{
    public int id;
    public string type;
    public Vector3 position;
}
