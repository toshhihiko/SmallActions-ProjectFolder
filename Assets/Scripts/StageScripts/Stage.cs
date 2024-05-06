using System.Collections;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private int floorId;
    public int stageType;
    public int[] stagePaths = new int[4];
    public bool isEntered;
    private int interval;
    private int numEnemies;
    [SerializeField] StageShape stageShapeScript;
    [SerializeField] EnemyDB enemyDB;
    [SerializeField] GameObject[] rewards = new GameObject[2];
    void Awake()
    {
        floorId = transform.root.GetComponent<FloorSave>().floorId;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEntered && other.CompareTag("Robot"))
        {
            isEntered = true;
            if (stageType == 0) Instantiate(rewards[0], transform.position, Quaternion.identity);
            else
            {
                stageShapeScript?.CloseDoors(true);
                StartCoroutine(SpawnEnemies());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            numEnemies--;
            if (numEnemies == 0)
            {
                stageShapeScript?.CloseDoors(false);
                Instantiate(rewards[Random.Range(0, rewards.Length)], transform.position, Quaternion.identity);
            }
        }
    }
    IEnumerator SpawnEnemies()
    {
        if (stageType == 1)
        {
            interval = 3;
            if (EnvironmentManager.emission_level >= 1) interval *= 2;
            numEnemies = interval * 3;
            for (int i = 0; i < interval; i ++)
            {
                for (int pos = 0; pos < 3; pos++)
                {
                    GameObject enemy;
                    if (floorId == 1) enemy = enemyDB.cityEnemyList[Random.Range(0, enemyDB.cityEnemyList.Count)];
                    else enemy = enemyDB.natureEnemyList[Random.Range(0, enemyDB.natureEnemyList.Count)];
                    GameObject instantiatedEnemy = Instantiate(enemy, transform.position + new Vector3(pos * 3 - 3, 0, 0), Quaternion.identity);
                    if (EnvironmentManager.emission_level >= 2) instantiatedEnemy.GetComponent<IEnemy>().HP *= 2;
                    if (EnvironmentManager.emission_level >= 3) instantiatedEnemy.GetComponent<IEnemy>().power *= 2;
                }
                yield return new WaitForSeconds(5);
            }
        }
        else if (stageType == 2)
        {
            numEnemies = 1;
            GameObject instantiatedEnemy = Instantiate(enemyDB.bossEnemyList[floorId - 1], transform.position, Quaternion.identity);
            if (EnvironmentManager.emission_level >= 2) instantiatedEnemy.GetComponent<IEnemy>().HP *= 2;
            if (EnvironmentManager.emission_level >= 3) instantiatedEnemy.GetComponent<IEnemy>().power *= 2;
        }
    }
}