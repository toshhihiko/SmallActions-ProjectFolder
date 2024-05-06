using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorGate : MonoBehaviour
{
    [SerializeField] private int currentFloorId;
    [SerializeField] private int nextFloorId;
    [SerializeField] private Vector3 localSpawnPosition;
    private void EnterPosition(Scene next, LoadSceneMode mode)
    {
        GameObject robot = GameObject.FindGameObjectWithTag("Robot");
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate");
        foreach (GameObject gate in gates)
        {
            FloorGate floorGate = gate.GetComponent<FloorGate>();
            if (floorGate.currentFloorId == nextFloorId && floorGate.nextFloorId == currentFloorId)
            {
                robot.transform.position = gate.transform.parent.TransformPoint(floorGate.localSpawnPosition);
            }
        }
        SceneManager.sceneLoaded -= EnterPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Robot"))
        {
            transform.root.GetComponent<FloorSave>()?.Save();
            SceneManager.sceneLoaded += EnterPosition;
            if (nextFloorId == 0) SceneManager.LoadScene("0Lab");
            if (nextFloorId == 1) SceneManager.LoadScene("1City");
            if (nextFloorId == 2) SceneManager.LoadScene("2Nature");
            if (nextFloorId == 3) SceneManager.LoadScene("3Boss");
        }

    }
}
