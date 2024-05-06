using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    //[SerializeField] private Player player;
    private void Awake() {
        // Create reference to animator component
        animator = transform.Find("Robot Character").GetComponent<Animator>();
    }
    /*
    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
    */
}
