using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Robot");
        player.transform.Find("RobotCanvas").gameObject.GetComponent<Canvas>().worldCamera = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        if (player != null) transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}