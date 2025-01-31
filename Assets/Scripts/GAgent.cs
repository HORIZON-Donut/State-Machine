using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAgent : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public float duration = 0;

    public GameObjsct target;
    public GameObject targetTag;

    public WorldState[] preCondition;
    public WorldState[] afterEffect;

    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public bool running = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
