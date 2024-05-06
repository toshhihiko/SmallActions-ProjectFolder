using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] int curr_pos;
    [SerializeField] GameObject[] weapons;
    [SerializeField] WeaponDB weaponDB;
    [SerializeField] Punch punchScript;
    private void Start()
    {
        weapons = new GameObject[2];
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Attack();
        if (Input.GetKeyDown(KeyCode.LeftShift)) SwitchWeapon();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        transform.up = (mousePosition - transform.position).normalized;
    }
    private void Attack()
    {
        if (weapons[curr_pos] != null)
        {
            Robot robotScript = transform.parent.GetComponent<Robot>();
            Weapon weaponScript = weapons[curr_pos].GetComponent<Weapon>();
            if (robotScript.battery >= weaponScript.battery && weaponScript.isAttackable)
            {
                weaponScript.Attack();
                robotScript.battery -= weaponScript.battery;
            }
        }
        else
        {
            if (punchScript.isAttackable) punchScript.Attack();
        }
    }
    public void EquipWeapon(int container_number, string weapon_type, int weapon_id)
    {
        GameObject weapon = null;
        if (weapon_type == "green") weapon = Instantiate(weaponDB.greenWeaponList[weapon_id]);
        if (weapon_type == "yellow") weapon = Instantiate(weaponDB.yellowWeaponList[weapon_id]);
        if (weapon_type == "red") weapon = Instantiate(weaponDB.redWeaponList[weapon_id]);
        weapons[container_number] = weapon;
        weapon.transform.parent = transform;
        weapon.transform.position = transform.position;
        weapon.transform.rotation = transform.rotation;
        weapon.GetComponent<Weapon>().user = Weapon.User.Robot;
        if (container_number != curr_pos) weapon.SetActive(false);
    }
    public void RemoveWeapon(int container_number)
    {
        Destroy(weapons[container_number]);
        weapons[container_number] = null;
    }
    private void SwitchWeapon()
    {
        int next_pos = curr_pos == 0? 1 : 0;
        for (int i = 0; i < 2; i++)
        {
            if (weapons[i] != null)
            {
                if (i == next_pos) weapons[i].SetActive(true);
                else weapons[i].SetActive(false);
            }
        }
        curr_pos = next_pos;
    }
}