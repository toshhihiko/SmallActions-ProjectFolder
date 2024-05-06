using System.Collections.Generic;
using UnityEngine;

public class StageReward : MonoBehaviour
{
    [SerializeField] List<GameObject> boxes;
    [SerializeField] int rewardType;
    [SerializeField] WeaponDB weaponDB;
    [SerializeField] ResourceDB resourceDB;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            //Weapon
            if (rewardType == 0)
            {
                int randomWeapon = Random.Range(0, 3);
                if (other.otherCollider.gameObject == boxes[0])
                {
                    Instantiate(weaponDB.greenWeaponOnFloorList[randomWeapon], boxes[0].transform.position, Quaternion.identity);
                }
                else if (other.otherCollider.gameObject == boxes[1])
                {
                    Instantiate(weaponDB.yellowWeaponOnFloorList[randomWeapon], boxes[1].transform.position, Quaternion.identity);
                    EnvironmentManager.emission += 25;
                }
                else if(other.otherCollider.gameObject == boxes[2]) 
                {
                    Instantiate(weaponDB.redWeaponOnFloorList[randomWeapon], boxes[2].transform.position, Quaternion.identity);
                    EnvironmentManager.emission += 50;
                }
            }
            //Food
            else if (rewardType == 1)
            {
                if (other.otherCollider.gameObject == boxes[0]) Instantiate(resourceDB.foodOnFloorList[0], boxes[0].transform.position, Quaternion.identity);
                else if (other.otherCollider.gameObject == boxes[1])
                {
                    Instantiate(resourceDB.foodOnFloorList[1], boxes[1].transform.position, Quaternion.identity);
                    EnvironmentManager.emission += 25;
                }
                else if (other.otherCollider.gameObject == boxes[2])
                {
                    Instantiate(resourceDB.foodOnFloorList[2], boxes[2].transform.position, Quaternion.identity);
                    EnvironmentManager.emission += 50;
                }
            }
            //City
            else if (rewardType == 2)
            {
                int randomNums = Random.Range(1, 4);
                for (int i = 0; i <= randomNums; i ++)
                {
                    int chance = Random.Range(0,4);
                    if (chance == 0 || chance == 1) Instantiate(resourceDB.cityOnFloorList[0], boxes[0].transform.position, Quaternion.identity);
                    else if (chance == 2) Instantiate(resourceDB.cityOnFloorList[1], boxes[0].transform.position, Quaternion.identity);
                }
            }
            //Nature
            else if (rewardType == 3)
            {
                int randomNums = Random.Range(1, 4);
                for (int i = 0; i <= randomNums; i ++)
                {
                    int chance = Random.Range(0, 2);
                    if (chance == 0) Instantiate(resourceDB.natureOnFloorList[0], boxes[0].transform.position, Quaternion.identity);
                    else if (chance == 1) Instantiate(resourceDB.natureOnFloorList[1], boxes[0].transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }
}
