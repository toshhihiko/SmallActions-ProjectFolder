using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    public static Robot robotInstance;
    [SerializeField] int _HP;
    public int HP
    {
        get { return _HP; }
        set
        {
            _HP = value;
            onHPUpdated?.Invoke(HP, max_HP);
            if (HP <= 0)
            {
                Instantiate(gameOver);
                Destroy(this.gameObject);
            }
        }
    }
    [SerializeField] private int _max_HP;
    public int max_HP
    {
        get { return _max_HP; }
        set 
        {
            _max_HP = value;
            onHPUpdated?.Invoke(HP, max_HP);
        }
    }
    public event Action<int, int> onHPUpdated;

    [SerializeField] private int _battery;
    public int battery
    {
        get { return _battery; }
        set
        {
            _battery = value;
            onBatteryUpdated?.Invoke(battery, max_battery);
        }
    }
    [SerializeField] private int _max_battery;
    public int max_battery
    {
        get { return _max_battery; }
        set 
        {
            _max_battery = value;
            onBatteryUpdated?.Invoke(battery, max_battery);
        }
    }
    public event Action<int, int> onBatteryUpdated;

    public float _moveSpeed;
    private float moveSpeed;

    private void Awake()
    {
        if (robotInstance == null)
        {
            robotInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Start()
    {
        HP = _HP;
        max_HP = _max_HP;
        battery = _battery;
        max_battery = _max_battery;
        moveSpeed = Time.fixedDeltaTime * _moveSpeed;
    }
    void Update()
    {
        EnvironmentManager.UpdateLab(Time.deltaTime);
    }
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = Input.GetAxis("Horizontal") * moveSpeed;
        controlSignal.y = Input.GetAxis("Vertical") * moveSpeed;
        transform.position += controlSignal;
    }
}