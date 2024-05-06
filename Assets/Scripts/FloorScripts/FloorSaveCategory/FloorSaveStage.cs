using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorSaveStages
{
    public List<FloorSaveStage> floorSaveStageList;
}
[System.Serializable]
public class FloorSaveStage
{
    public int[] stagePaths = new int[4];
    public int stageType;
    public bool isEntered;
    public Vector3 position;
}