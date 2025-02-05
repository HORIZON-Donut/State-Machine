using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string, int> state, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(state);
        this.action = action;
    }
}

public class GPlanner
{
    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldState states)
    {
        List<GAction> usableAction = new List<GAction>();
        foreach (GAction action in actions)
        {
            if (action.IsAchievable())
            { usableAction.Add(action); }
        }

        List<Node> leaves = new List<Node>();
        Node Start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null);

        bool suecess = BuildGraph(Start, leaves, usableAction, goal);
        if(!suecess)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach(Node leaf in leaves)
        {
            if(cheapest == null)
                cheapest = leaf;

            else
            {
                if (cheapest.cost < leaf.cost)
                    cheapest = leaf;
            }
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach(GAction a in result)
        {
            queue.Enqueue(a);
        }

        Debug.Log("The plan is: ");
        foreach(GAction a in queue)
        {
            Debug.Log(a.actionName);
        }

        return queue;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach(KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }

        return true;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }

        return subset;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableAction, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach(GAction action in usableAction)
        {
            if(action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach(KeyValuePair<string, int> eff in action.effects)
                {
                    if(!currentState.ContainsKey(eff.Key))
                    {
                        currentState.Add(eff.Key, eff.Value);
                    }
                }
                
                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = new ActionSubset(usableAction, action);
                    bool found = BuildGraph(parent, leaves, subset, goal);
                    if(found)
                        foundPath = true;
                }
            }       
        }

        return foundPath;
    }

}
