using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceDB", menuName = "Prototype-1/ResourceDB", order = 0)]
public class ResourceDB : ScriptableObject
{
    public List<GameObject> foodOnFloorList;
    public List<GameObject> cityOnFloorList;
    public List<GameObject> natureOnFloorList;
}