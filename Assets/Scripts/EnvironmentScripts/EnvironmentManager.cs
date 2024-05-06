using UnityEngine;
using System.Linq;

public class EnvironmentManager : MonoBehaviour
{
    static public bool[] floorFirstEntries = new bool[4] { true, true, true, true };
    static public float charging_battery = 0;
    static public int creator_hunger = 0;
    static public int[] creator_resouce = new int[2];
    static public float emission = 0;
    static public int emission_level = 0;
    static public int power_plant_type = 0;
    static public int[] soilStatusList = new int[5];
    static public void InitializeEnvironment()
    {
        floorFirstEntries = new bool[4] { true, true, true, true };
        charging_battery = 0;
        creator_hunger = 0;
        creator_resouce = new int[2];
        emission = 0;
        emission_level = 0;
        power_plant_type = 0;
        soilStatusList = new int[5];
    }
    static public void UpdateLab(float time)
    {
        if (emission < 100) emission_level = 0;
        else if (100 <= emission && emission < 200) emission_level = 1;
        else if (200 <= emission && emission < 300) emission_level = 2;
        else if (300 <= emission) emission_level = 3;
        if (power_plant_type == 0)
        {
            if (charging_battery < 100) charging_battery += time * 0.25f;
            else charging_battery = 100;
        }
        else if (power_plant_type == 1)
        {
            if (charging_battery <= 100) charging_battery += time * 1f;
            else charging_battery = 100;
            if (emission <= 300) emission += time * 1f;
            else emission = 300;
        }
        if (0 <= emission) emission -= time * soilStatusList.Sum() / 10;
        else emission = 0;
    }
}
