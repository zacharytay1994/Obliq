using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    int total_points_ = 0;
    int kill_points_ = 0;
    int goal_points_ = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddKillPoints(int points, float multiplier)
    {
        kill_points_ += Mathf.RoundToInt(points * multiplier);
    }

    public void AddGoalPoints(int points, float multiplier)
    {
        goal_points_ += Mathf.RoundToInt(points * multiplier);
    }

    public void EndOfRoom()
    {
        total_points_ += kill_points_ + goal_points_;
        kill_points_ = 0;
        goal_points_ = 0;
    }

    public void SetTotalPoints(int newTotalPoints)
    {
        total_points_ = newTotalPoints;
    }

    public int GetKillPoints()
    {
        return kill_points_;
    }

    public int GetGoalPoints()
    {
        return goal_points_;
    }

    public int GetTotalPoints()
    {
        return total_points_;
    }
}
