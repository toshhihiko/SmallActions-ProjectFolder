using UnityEngine;
using Unity.MLAgents;

public class EnemyTrainingManager : MonoBehaviour
{
    public Agent[] agents;
    private float stageSize = 6f;
    void Start()
    {
        Reset();
    }
    public void Attack(int agentId)
    {
        if (agentId == 0)
        {
            agents[0].AddReward(0.1f);
            agents[1].AddReward(-0.1f);
        }
        else
        {
            agents[0].AddReward(-0.1f);
            agents[1].AddReward(0.1f);
        }
    }
    public void EndEpisode(int agentId)
    {
        if (agentId == 0)
        {
            agents[0].AddReward(1.0f);
            agents[1].AddReward(-1.0f);
        }
        else
        {
            agents[0].AddReward(-1.0f);
            agents[1].AddReward(1.0f);
        }
        Debug.Log("Agent " + agentId + " wins");
        agents[0].EndEpisode();
        agents[1].EndEpisode();
    }
    public void Reset()
    {
        agents[0].gameObject.transform.localPosition = new Vector2(Random.value*stageSize*2 - stageSize, Random.value*stageSize*2 - stageSize);
        agents[0].GetComponent<EnemyAgent>().HP = 3;
        agents[1].gameObject.transform.localPosition = new Vector2(Random.value*stageSize*2 - stageSize, Random.value*stageSize*2 - stageSize);
        agents[1].GetComponent<EnemyAgent>().HP = 3;
    }
}
