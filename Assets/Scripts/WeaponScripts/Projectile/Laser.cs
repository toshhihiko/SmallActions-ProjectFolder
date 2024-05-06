using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IProjectile
{
    public Weapon.User user {get; set;}
    public int damage {get; set;}
    private float time;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (user == Weapon.User.Robot && other.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy>().HP -= damage;
        }
        if (user == Weapon.User.Enemy && other.CompareTag("Robot"))
        {
            other.GetComponent<Robot>().HP -= damage;
        }
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.3f) Destroy(gameObject);
    }
}