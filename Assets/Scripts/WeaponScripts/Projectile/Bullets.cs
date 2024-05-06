using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour, IProjectile
{
    public Weapon.User user {get; set;}
    public int damage {get; set;}
    [SerializeField] GameObject gun;
    void Start()
    {
        for (int i = 0; i < 3; i ++)
        {
            GameObject instantiatedBullet = Instantiate(gun, transform.localPosition, Quaternion.Euler(0, 0, i * 30 - 30) * transform.rotation);
            instantiatedBullet.GetComponent<IProjectile>().user = user;
            instantiatedBullet.GetComponent<IProjectile>().damage = damage;
        }
        Destroy(gameObject);
    }
}