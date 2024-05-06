using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    public Weapon.User user {get; set;}
    public int damage {get; set;}
    private float moveSpeed;
    void Awake()
    {
        moveSpeed = Time.fixedDeltaTime * 15.0f;
    }
    void FixedUpdate()
    {
        transform.Translate(0, moveSpeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Wall")) Destroy(gameObject);
        if (user == Weapon.User.Robot && other.CompareTag("Enemy"))
        {
            other.GetComponent<IEnemy>().HP -= damage;
            Destroy(gameObject);
        }
        if (user == Weapon.User.Enemy && other.CompareTag("Robot"))
        {
            other.GetComponent<Robot>().HP -= damage;
            Destroy(gameObject);
        }
    }
}