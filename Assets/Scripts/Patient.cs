using UnityEngine;

public class Patient : GAgent
{
    protected override void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("hasArrived", 1, true);
        goals.Add(s1, 3);
    }
}
