using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private float time;
    public bool isAttackable;
    [SerializeField] GameObject punchArea;
    void Update()
    {
        if (time < 1.0f)
        {
            time += Time.deltaTime;
            isAttackable = false;
        }
        else isAttackable = true;
    }
    public void Attack()
    {
        StartCoroutine(AttackCount());
        time = 0;
        isAttackable = false;
    }
    IEnumerator AttackCount()
    {
        punchArea.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        punchArea.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Enemy")) other.GetComponent<IEnemy>().HP -= 1;
    }
}
