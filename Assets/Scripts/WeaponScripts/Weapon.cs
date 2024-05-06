using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum User { Robot, Enemy };
    public int id;
    public string type;
    public int damage;
    public int battery;
    public int interval;
    public User user;
    [SerializeField] GameObject weaponSprite;
    [SerializeField] GameObject projectile;
    [SerializeField] bool isParent;
    
    private float time = 0;
    public bool isAttackable = false;
    private float weapon_size;
    void Start()
    {
        weapon_size = weaponSprite.transform.localScale.x;
    }
    void FixedUpdate()
    {
        if (time > interval) isAttackable = true;
        else
        {
            time += Time.fixedDeltaTime;
            isAttackable = false;
        }
        Vector3 weaponSpriteScale = weaponSprite.transform.localScale;
        if (transform.parent.eulerAngles.z < 180) weaponSpriteScale.x = -weapon_size;
        else weaponSpriteScale.x = weapon_size;
        weaponSprite.transform.localScale = weaponSpriteScale;
    }
    public void Attack()
    {
        if (isAttackable)
        {
            GameObject instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation);
            if (isParent) instantiatedProjectile.transform.SetParent(transform);
            instantiatedProjectile.transform.localScale *= transform.parent.localScale.x;
            IProjectile iProjectile = instantiatedProjectile.GetComponent<IProjectile>();
            iProjectile.user = user;
            iProjectile.damage = damage;
            time = 0;
        }
    }
}