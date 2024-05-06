using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorSaveResources
{
    public List<FloorSaveResource> floorSaveResourceList;
}
[System.Serializable]
public class FloorSaveResource
{
    public string category;
    public string type;
    public string rarity;
    public int id;
    public Vector3 position;
}