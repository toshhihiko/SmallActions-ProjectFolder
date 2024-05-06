using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateBullet : MonoBehaviour, IProjectile
{
    public Weapon.User user {get; set;}
    public int damage {get; set;}
    private float moveSpeed;
    void Awake()
    {
        moveSpeed = Time.deltaTime * 15.0f;
    }
    void Update()
    {
        transform.Translate(0, moveSpeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (user == Weapon.User.Robot && other.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy>().HP -= damage;
        }
        if (user == Weapon.User.Enemy && other.CompareTag("Robot"))
        {
            other.GetComponent<Robot>().HP -= damage;
        }
    }
}