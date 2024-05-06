using UnityEngine;
using System.IO;

public class FloorSave : MonoBehaviour
{
    private string resourceSavePath;
    private string weaponSavePath;
    private string stageSavePath;
    [SerializeField] FloorSaveResources floorSaveResources;
    [SerializeField] FloorSaveWeapons floorSaveWeapons;
    [SerializeField] FloorSaveStages floorSaveStages;
    [SerializeField] ResourceDB resourceDB;
    [SerializeField] WeaponDB weaponDB;
    public int floorId;
    void Awake()
    {
        resourceSavePath = Application.persistentDataPath + "/SaveData/" + floorId + ".SaveResources" + ".json";
        weaponSavePath = Application.persistentDataPath + "/SaveData/" + floorId + ".SaveWeapons" + ".json";
        stageSavePath = Application.persistentDataPath + "/SaveData/" + floorId + ".SaveStages" + ".json";

        string resourceDirectoryPath = Path.GetDirectoryName(resourceSavePath);
        string weaponDirectoryPath = Path.GetDirectoryName(weaponSavePath);
        string stageDirectoryPath = Path.GetDirectoryName(stageSavePath);

        if (!Directory.Exists(resourceDirectoryPath)) Directory.CreateDirectory(resourceDirectoryPath);
        if (!Directory.Exists(weaponDirectoryPath)) Directory.CreateDirectory(weaponDirectoryPath);
        if (!Directory.Exists(stageDirectoryPath)) Directory.CreateDirectory(stageDirectoryPath);

        bool isFloorFirstEntry = EnvironmentManager.floorFirstEntries[floorId];
        if (!isFloorFirstEntry) Load();
        if (isFloorFirstEntry && floorId != 0 && floorId != 3) transform.Find("Stages").GetComponent<StageGenerate>().GenerateStage();
        EnvironmentManager.floorFirstEntries[floorId] = false;
    }
    public void Save()
    {
        GameObject[] resources = GameObject.FindGameObjectsWithTag("ResourceOnFloor");
        foreach (GameObject resource in resources)
        {
            FloorSaveResource floorSaveResource = new FloorSaveResource();
            ResourceOnFloor resourceOnFloorScript = resource.GetComponent<ResourceOnFloor>();
            floorSaveResource.id = resourceOnFloorScript.id;
            floorSaveResource.type = resourceOnFloorScript.type;
            floorSaveResource.position = resource.GetComponent<RectTransform>().position;
            floorSaveResources.floorSaveResourceList.Add(floorSaveResource);
        }
        GameObject[] weaponsOnFloor = GameObject.FindGameObjectsWithTag("WeaponOnFloor");
        foreach (GameObject weaponOnFloor in weaponsOnFloor)
        {
            FloorSaveWeapon floorSaveWeapon = new FloorSaveWeapon();
            WeaponOnFloor weaponOnFloorScript = weaponOnFloor.GetComponent<WeaponOnFloor>();
            floorSaveWeapon.id = weaponOnFloorScript.id;
            floorSaveWeapon.type = weaponOnFloorScript.type;
            floorSaveWeapon.position = weaponOnFloor.GetComponent<RectTransform>().position;
            floorSaveWeapons.floorSaveWeaponList.Add(floorSaveWeapon);
        }
        Transform stages = GameObject.Find("Stages")?.transform;
        if (stages != null)
        {
            foreach (Transform stage in stages)
            {
                FloorSaveStage floorSaveStage = new FloorSaveStage();
                Stage stageScript = stage.GetComponent<Stage>();
                floorSaveStage.stagePaths = stageScript.stagePaths;
                floorSaveStage.stageType = stageScript.stageType;
                floorSaveStage.isEntered = stageScript.isEntered;
                floorSaveStage.position = stage.position;
                floorSaveStages.floorSaveStageList.Add(floorSaveStage);
            }
        }
        StreamWriter resourceWriter;
        string jsonSaveResources = JsonUtility.ToJson(floorSaveResources);
        resourceWriter = new StreamWriter(resourceSavePath, false);
        resourceWriter.WriteLine(jsonSaveResources);
        resourceWriter.Flush();
        resourceWriter.Close();

        StreamWriter WeaponWriter;
        string jsonSaveWeapons = JsonUtility.ToJson(floorSaveWeapons);
        WeaponWriter = new StreamWriter(weaponSavePath, false);
        WeaponWriter.WriteLine(jsonSaveWeapons);
        WeaponWriter.Flush();
        WeaponWriter.Close();

        StreamWriter stageWriter;
        string jsonSaveStages = JsonUtility.ToJson(floorSaveStages);
        stageWriter = new StreamWriter(stageSavePath, false);
        stageWriter.WriteLine(jsonSaveStages);
        stageWriter.Flush();
        stageWriter.Close();
    }

    public void Load()
    {
        StreamReader resourceReader;
        resourceReader = new StreamReader(resourceSavePath, false);
        string jsonSaveResources = resourceReader.ReadToEnd();
        FloorSaveResources floorSaveResources = JsonUtility.FromJson<FloorSaveResources>(jsonSaveResources);
        foreach (FloorSaveResource floorSaveResource in floorSaveResources.floorSaveResourceList)
        {
            GameObject resource = null;
            if (floorSaveResource.type == "food") resource = resourceDB.foodOnFloorList[floorSaveResource.id];
            if (floorSaveResource.type == "city") resource = resourceDB.cityOnFloorList[floorSaveResource.id];
            if (floorSaveResource.type == "nature") resource = resourceDB.natureOnFloorList[floorSaveResource.id];
            Instantiate(resource, floorSaveResource.position, Quaternion.identity);
        }
        resourceReader.Close();

        StreamReader weaponReader;
        weaponReader = new StreamReader(weaponSavePath, false);
        string jsonSaveWeapons = weaponReader.ReadToEnd();
        FloorSaveWeapons floorSaveWeapons = JsonUtility.FromJson<FloorSaveWeapons>(jsonSaveWeapons);
        foreach (FloorSaveWeapon floorSaveWeapon in floorSaveWeapons.floorSaveWeaponList)
        {
            GameObject weaponOnFloor = null;
            if (floorSaveWeapon.type == "green") weaponOnFloor = weaponDB.greenWeaponOnFloorList[floorSaveWeapon.id];
            if (floorSaveWeapon.type == "yellow") weaponOnFloor = weaponDB.yellowWeaponOnFloorList[floorSaveWeapon.id];
            if (floorSaveWeapon.type == "red") weaponOnFloor = weaponDB.redWeaponOnFloorList[floorSaveWeapon.id];
            Instantiate(weaponOnFloor, floorSaveWeapon.position, Quaternion.identity);
        }
        weaponReader.Close();

        StreamReader stageReader;
        stageReader = new StreamReader(stageSavePath, false);
        string jsonSaveStages = stageReader.ReadToEnd();
        FloorSaveStages floorSaveStages = JsonUtility.FromJson<FloorSaveStages>(jsonSaveStages);
        Transform stages = transform.Find("Stages")?.transform;
        if (stages != null)
        {
            int stageCount = 0;
            foreach (Transform stage in stages)
            {
                FloorSaveStage floorSaveStage = floorSaveStages.floorSaveStageList[stageCount];
                stage.GetComponent<Stage>().stagePaths = floorSaveStage.stagePaths;
                stage.GetComponent<Stage>().stageType = floorSaveStage.stageType;
                stage.GetComponent<Stage>().isEntered = floorSaveStage.isEntered;
                stage.transform.position = floorSaveStage.position;
                stageCount++;
            }
            stageReader.Close();
        }
    }
}