using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour, IProjectile
{
    public Weapon.User user { get; set; }
    public int damage { get; set; }

    private float burst_time = 0;
    [SerializeField] int subject = 0;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject burstArea;
    [SerializeField] float speed = 2.0f;

    void Update()
    {
        if (subject == 0)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        if (subject == 1)
        {
            burst_time += Time.deltaTime;
            if (burst_time > 0.5f) Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (user == Weapon.User.Robot)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (subject == 0)
                {
                    subject = 1;
                    burstArea.SetActive(true);
                    bomb.SetActive(false);
                }
                if (subject == 1)
                {
                    other.gameObject.GetComponent<IEnemy>().HP -= damage;
                }
            }
            if (other.gameObject.CompareTag("Wall"))
            {
                if (subject == 0)
                {
                    subject = 1;
                    burstArea.SetActive(true);
                    bomb.SetActive(false);
                }
            }
        }
        if (user == Weapon.User.Enemy)
        {
            if (other.gameObject.CompareTag("Robot"))
            {
                if (subject == 0)
                {
                    subject = 1;
                    burstArea.SetActive(true);
                    bomb.SetActive(false);
                }
                if (subject == 1)
                {
                    other.gameObject.GetComponent<Robot>().HP -= damage;
                }
            }
            if (other.gameObject.CompareTag("Wall"))
            {
                if (subject == 0)
                {
                    subject = 1;
                    burstArea.SetActive(true);
                    bomb.SetActive(false);
                }
            }
        }
    }
}