using UnityEngine;
public class EnemyBurst : MonoBehaviour, IEnemy
{
    //IEnemyデータ
    [SerializeField] int _id;
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    [SerializeField] string _type;
    public string type
    {
        get { return _type; }
        set { _type = value; }
    }
    [SerializeField] int _HP;
    public int HP
    {
        get { return _HP; }
        set
        {
            _HP = value;
            if (HP <= 0) Destroy(gameObject);
        }
    }
    [SerializeField] int _power;
    public int power
    {
        get { return _power; }
        set
        {
            _power = value;
        }
    }
    //ステータスデータ
    [SerializeField] float decision_interval;
    [SerializeField] float walk_speed;
    [SerializeField] float approach_speed;
    [SerializeField] float approach_distance;
    [SerializeField] float leave_distance;
    [SerializeField] GameObject burst;
    [SerializeField] GameObject sprite;
    //共通データ
    [SerializeField] LayerMask detectLayers;
    private int rayCount = 36;
    private float time = 0;
    private float move_speed;
    private Vector3 move_direction;
    private Vector3 weapon_direction;
    private void Update()
    {

        if (time < decision_interval) time += Time.deltaTime;
        else
        {
            MoveSetting();
            Vector3 sprite_scale = sprite.transform.localScale;
            if (weapon_direction.x < 0) sprite_scale.x = -1;
            else sprite_scale.x = 1;
            sprite.transform.localScale = sprite_scale;
            time = 0;
        }
        transform.position += move_direction * move_speed * Time.deltaTime;
    }

    private void MoveSetting()
    {
        RaycastHit2D hit_robot = new RaycastHit2D();
        RaycastHit2D hit_wall = new RaycastHit2D();
        for (int i = 0; i < rayCount; i++)
        {
            float angle = (i - rayCount / 2) * 10;
            Vector3 ray_direction = Quaternion.Euler(0f, 0f, angle) * transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, ray_direction, Mathf.Infinity, detectLayers);
            Debug.DrawRay(transform.position, ray_direction * Mathf.Infinity, Color.red);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Robot")) hit_robot = hit;
                if (hit.collider.CompareTag("Wall") && (hit.transform.position - transform.position).magnitude < 1f) hit_wall = hit;
            }
        }
        if (hit_robot)
        {
            move_speed = approach_speed;
            weapon_direction = (hit_robot.transform.position - transform.position).normalized;
            if (hit_wall)
            {
                move_direction = -(hit_wall.transform.position - transform.position).normalized;
            }
            else
            {
                float distance = (hit_robot.transform.position - transform.position).magnitude;
                if (distance > approach_distance) move_direction = weapon_direction;
                else if (leave_distance < distance && distance < approach_distance) move_direction = Random.insideUnitCircle.normalized;
                else if (distance < leave_distance) move_direction = -weapon_direction;
            }
        }
        else
        {
            move_speed = walk_speed;
            if (hit_wall)
            {
                weapon_direction = (hit_wall.transform.position - transform.position).normalized;
            }
            else
            {
                weapon_direction = Random.insideUnitCircle.normalized;
            }
            move_direction = weapon_direction;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            GameObject instantiated = Instantiate(burst, transform.position, Quaternion.identity);
            instantiated.GetComponent<IProjectile>().user = Weapon.User.Enemy;
            instantiated.GetComponent<IProjectile>().damage = 3;
            instantiated.SetActive(true);
            Destroy(gameObject);
        }
    }
}
