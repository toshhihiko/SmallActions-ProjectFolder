using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatorButton : MonoBehaviour
{
    public enum Type {Fix, Upgrade}
    [SerializeField] Type type;
    [SerializeField] CreatorDrop creatorDrop;
    private Robot robot;
    public void OnClick()
    {
        robot = GameObject.FindGameObjectWithTag("Robot").GetComponent<Robot>();
        if (type == Type.Fix)
        {
            if (EnvironmentManager.creator_hunger >= 1 && EnvironmentManager.creator_resouce[0] >= 1)
            {
                robot.HP = robot.max_HP;
                EnvironmentManager.creator_hunger -= 1;
                EnvironmentManager.creator_resouce[0] -= 1;
                creatorDrop.UpdateText();
            }
        }
        if (type == Type.Upgrade)
        {
            if (EnvironmentManager.creator_hunger >= 1 && EnvironmentManager.creator_resouce[1] >= 1)
            {
                robot.max_HP += 10;
                EnvironmentManager.creator_hunger -= 1;
                EnvironmentManager.creator_resouce[1] -= 1;
                creatorDrop.UpdateText();
            }
        }
    }
}
