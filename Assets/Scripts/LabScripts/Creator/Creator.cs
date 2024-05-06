using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Creator : MonoBehaviour
{
    [SerializeField] GameObject[] flames = new GameObject[2];
    [SerializeField] Button [] buttons = new Button[2];
    void Awake()
    {
        flames[0].SetActive(false);
        flames[1].SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Robot"))
        {
            flames[0].SetActive(true);
            flames[1].SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Robot"))
        {
            flames[0].SetActive(false);
            flames[1].SetActive(false);
        }    
    }
}
