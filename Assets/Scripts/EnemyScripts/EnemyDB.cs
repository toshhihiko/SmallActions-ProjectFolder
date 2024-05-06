using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDB", menuName = "Prototype-1/EnemyDB", order = 0)]
public class EnemyDB : ScriptableObject 
{
    public List<GameObject> cityEnemyList;
    public List<GameObject> natureEnemyList;
    public List<GameObject> bossEnemyList;
}