using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageShape : MonoBehaviour
{
    [SerializeField] GameObject[] open = new GameObject[4];
    [SerializeField] GameObject[] close = new GameObject[4];
    [SerializeField] GameObject[] doors = new GameObject[4];
    private Stage stageScript;
    private int[] stagePaths = new int[4];

    void Start()
    {
        stageScript = transform.parent.GetComponent<Stage>();
        stagePaths = stageScript.stagePaths;
        ActivatePaths();
    }
    private void ActivatePaths()
    {
        for (int i = 0; i < 4; i++)
        {
            if (stagePaths[i] == 1)
            {
                close[i].SetActive(false);
                open[i].SetActive(true);
            }
            else
            {
                close[i].SetActive(true);
                open[i].SetActive(false);
            }
        }
    }
    public void CloseDoors(bool isClosed)
    {
        for (int i = 0; i < 4; i++)
        {
            if (stagePaths[i] == 1)
            {
                doors[i].SetActive(isClosed);
            }
        }
    }
}
