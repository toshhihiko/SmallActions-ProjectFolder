using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour, IProjectile
{
    public Weapon.User user { get; set; }
    public int damage { get; set; }
    private bool isLeft;
    private float angle;
    private float angle_count;
    private float motion_speed = 300;
    [SerializeField] SpriteRenderer weaponSprite;
    [SerializeField] GameObject attackArea;
    void Start()
    {
        isLeft = transform.parent.parent.eulerAngles.z < 180 ? true : false;
        if (isLeft)
        {
            angle = transform.parent.parent.eulerAngles.z - 60;
            weaponSprite.flipX = true;
        }
        else angle = transform.parent.parent.eulerAngles.z + 60;
        
        angle_count = 0;
        transform.parent.Find("WeaponSprite").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    }
    void Update()
    {
        Quaternion inverseParentRotation = Quaternion.Inverse(transform.parent.parent.rotation);
        Quaternion childLocalRotation = Quaternion.Euler(0, 0, angle);
        transform.localRotation = inverseParentRotation * childLocalRotation;
        if (angle_count <= 60)
        {
            if (isLeft) angle += motion_speed * Time.deltaTime;
            else angle -= motion_speed * Time.deltaTime;
        }
        else if (60 < angle_count && angle_count <= 80)
        {
            attackArea.SetActive(true);
        }
        else if (angle_count > 80)
        {
            transform.parent.Find("WeaponSprite").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            Destroy(gameObject);
        }
        angle_count += motion_speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (user == Weapon.User.Robot && other.CompareTag("Enemy")) other.GetComponent<IEnemy>().HP -= damage;
        if (user == Weapon.User.Enemy && other.CompareTag("Robot")) other.GetComponent<Robot>().HP -= damage;
    }
}