using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI battery_text;
    [SerializeField] Button switchButton;
    private Robot robotScript;
    void Awake()
    {
        switchButton.gameObject.SetActive(false);
        robotScript = GameObject.FindGameObjectWithTag("Robot").GetComponent<Robot>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Robot"))
        {
            switchButton.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Robot"))
        {
            switchButton.gameObject.SetActive(false);
        }    
    }
    void Update()
    {
        battery_text.text = (int)EnvironmentManager.charging_battery + "/" + (int)robotScript.max_battery;
    }
    public void SwitchBattery()
    {
        float charging_battery = EnvironmentManager.charging_battery;
        int attached_battery = GameObject.FindGameObjectWithTag("Robot").GetComponent<Robot>().battery;
        GameObject.FindGameObjectWithTag("Robot").GetComponent<Robot>().battery = (int)charging_battery;
        EnvironmentManager.charging_battery = attached_battery;
    }
}