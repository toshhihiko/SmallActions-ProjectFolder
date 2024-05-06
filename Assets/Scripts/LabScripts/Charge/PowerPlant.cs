using UnityEngine;
using UnityEngine.UI;
public class PowerPlant : MonoBehaviour
{
    [SerializeField] Button switchButton;
    [SerializeField] int power_plant_type;
    [SerializeField] GameObject[] powerPlants = new GameObject[2];
    void Awake()
    {
        switchButton.gameObject.SetActive(false);
        power_plant_type = EnvironmentManager.power_plant_type;
        powerPlants[power_plant_type].SetActive(true);
        powerPlants[1-power_plant_type].SetActive(false);

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
    public void SwitchPowerPlant()
    {
        power_plant_type = 1 - power_plant_type;
        powerPlants[power_plant_type].SetActive(true);
        powerPlants[1-power_plant_type].SetActive(false);
        EnvironmentManager.power_plant_type = power_plant_type;
    }
}
