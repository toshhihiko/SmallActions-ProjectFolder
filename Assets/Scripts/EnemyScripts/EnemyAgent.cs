using UnityEngine;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents;

public class EnemyAgent : Agent, IEnemy
{
    [SerializeField] GameObject gameClear;
    //[SerializeField] int agent_id;
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
            //enemyTrainingManager.Attack(1 - agent_id);
            //if (HP <= 0) enemyTrainingManager.EndEpisode(1 - agent_id);
            if (HP <= 0)
            {
                Instantiate(gameClear);
                Destroy(gameObject);
                Destroy(GameObject.FindGameObjectWithTag("Robot"));
            }
        }
    }
    [SerializeField] int _power;
    public int power
    {
        get { return _power; }
        set 
        {
            _power = value;
            weaponScript.damage += power;
        }
    }
    //ステータスデータ
    [SerializeField] GameObject sprite;
    [SerializeField] float sprite_size;
    [SerializeField] GameObject weapon;
    [SerializeField] Weapon weaponScript;
    //共通データ
    //[SerializeField] EnemyTrainingManager enemyTrainingManager;
    private float move_speed;

    public override void Initialize()
    {
        move_speed = 5.0f * Time.deltaTime;
        sprite_size = sprite.transform.localScale.x;
    }
    public override void OnEpisodeBegin()
    {
        //enemyTrainingManager?.Reset();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(weaponScript.isAttackable);
        sensor.AddObservation(weapon.transform.rotation.z);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0] * move_speed;
        controlSignal.y = actionBuffers.ContinuousActions[1] * move_speed;
        transform.position += controlSignal;
        weapon.transform.rotation = Quaternion.Euler(0, 0, actionBuffers.ContinuousActions[2] * -180f);

        Vector3 sprite_scale = sprite.transform.localScale;
        if (controlSignal.x != 0)
        {
            if (controlSignal.x < 0) sprite_scale.x = -sprite_size;
            else sprite_scale.x = sprite_size;
            sprite.transform.localScale = sprite_scale;
        }

        int discreteActions = actionBuffers.DiscreteActions[0];
        if (discreteActions == 1 && weaponScript.isAttackable)
        {
            weaponScript.Attack();
        }
    }
    public override void Heuristic(in ActionBuffers actionBuffers)
    {
        var continuousActions = actionBuffers.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
        continuousActions[2] = Input.GetAxis("Rotation");

        var discreteActions = actionBuffers.DiscreteActions;
        discreteActions[0] = 0;
        if (Input.GetKey(KeyCode.Space)) discreteActions[0] = 1;
    }
}
