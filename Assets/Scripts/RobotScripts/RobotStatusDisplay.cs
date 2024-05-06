using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RobotStatusDisplay : MonoBehaviour
{
    private Robot robotScript;
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] Slider HPSlider;
    [SerializeField] TextMeshProUGUI batteryText;
    [SerializeField] Slider batterySlider;
    [SerializeField] TextMeshProUGUI emissionText;
    [SerializeField] Slider emissionSlider;
    [SerializeField] TextMeshProUGUI emissionLevelText;
    void Awake()
    {
        robotScript = transform.root.gameObject.GetComponent<Robot>();
        robotScript.onHPUpdated += DisplayHP;
        robotScript.onBatteryUpdated += DisplayBattery;
    }
    private void DisplayHP(int HP, int max_HP)
    {
        HPSlider.value = (float)HP / (float)max_HP;
        HPText.text = HP + "/" + max_HP;
    }
    private void DisplayBattery(int battery, int max_battery)
    {
        batterySlider.value = (float) battery / (float) max_battery;
        batteryText.text = battery + "/" + max_battery;
    }
    void Update()
    {
        if (EnvironmentManager.emission_level != 3) emissionText.text = (int)EnvironmentManager.emission%100 + "/100";
        else emissionText.text = "MAX";
        emissionSlider.value = EnvironmentManager.emission%100 / 100;
        emissionLevelText.text = EnvironmentManager.emission_level.ToString();
    }
}
