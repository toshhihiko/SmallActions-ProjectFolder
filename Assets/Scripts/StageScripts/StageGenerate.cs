using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageGenerate : MonoBehaviour
{
    public GameObject[] stages;
    public GameObject[] startGoalStages;
    private readonly int[] move = new int[] { -3, 1, 3, -1 };
    private (int[][], int[]) FindPath(int[][] map_2D, int[] map_1D)
    {
        int[][] map_2D_copy = new int[9][] {new int[4],new int[4],new int[4],new int[4],new int[4],new int[4],new int[4],new int[4],new int[4]};
        int[] map_1D_copy = new int[9];
        for (int i = 0; i < 9; i ++)
        {
            map_1D_copy[i] = map_1D[i];
            for (int j = 0; j < 4; j ++) map_2D_copy[i][j] = map_2D[i][j];
        }

        List<int> starts = new();
        List<int> goals = new();
        if (map_1D_copy.Contains<int>(1))
        {
            for (int i = 0; i < 9; i++)
            {
                if (map_1D_copy[i] == 0) starts.Add(i);
                if (map_1D_copy[i] == 1) goals.Add(i);
            }
        }
        else
        {
            starts = new() { 0, 1, 2 };
            goals = new() { 6, 7, 8 };
        }

        int current_pos = starts[Random.Range(0, starts.Count)];
        while (true)
        {
            List<int> choices = new();
            int[] current_stage = map_2D_copy[current_pos];
            for (int i = 0; i < 4; i++)
            {
                if (current_stage[i] == 0 && map_2D_copy[current_pos + move[i]][(i + 2) % 4] == 0) choices.Add(i);
            }
            if (choices.Count == 0) return FindPath(map_2D, map_1D);
            int choice = choices[Random.Range(0, choices.Count)];
            map_1D_copy[current_pos] = 1;
            map_2D_copy[current_pos][choice] = 1;

            int next_pos = current_pos + move[choice];
            map_1D_copy[next_pos] = 1;
            map_2D_copy[next_pos][(choice + 2) % 4] = 1;
            
            if (goals.Contains<int>(next_pos)) return (map_2D_copy, map_1D_copy);
            current_pos = next_pos;
        }
    }

    public void GenerateStage()
    {
        int[][] map_2D = new int[9][]
        {
            new int[]{-1, 0, 0,-1}, new int[]{-1, 0, 0, 0}, new int[]{-1,-1, 0, 0},
            new int[]{ 0, 0, 0,-1}, new int[]{ 0, 0, 0, 0}, new int[]{ 0,-1, 0, 0},
            new int[]{ 0, 0,-1,-1}, new int[]{ 0, 0,-1, 0}, new int[]{ 0,-1,-1, 0},
        };
        int[] map_1D = new int[9]
        {
            0, 0, 0,
            0, 0, 0,
            0, 0, 0,
        };
        while (map_1D.Contains<int>(0)) (map_2D, map_1D) = FindPath(map_2D, map_1D);
        for (int i = 0; i < 2; i ++)
        {
            int random_pos = Random.Range(0,3);
            startGoalStages[i].transform.position = new Vector3((random_pos-1)*32, -128*i+64, 0);
            map_2D[i*6+random_pos][i*2] = 1;
        }
        for (int i = 0; i < map_2D.Length; i++)
        {
            Stage stage = stages[i].GetComponent<Stage>();
            stage.stagePaths = map_2D[i];
        }
    }
}