using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
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

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if(preCondition != null )
        {
            foreach( WorldState w in preCondition )
            {
                preconditions.Add(w.key, w.value);
            }
        }

        if( afterEffect != null )
        {
            foreach( WorldState w in afterEffect )
            {
                effects.Add(w.key, w.value);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
