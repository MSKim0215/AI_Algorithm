using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent _agent, List<Transform> _original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach(Transform item in _original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            if(itemAgent != null && itemAgent.AgentFlock == _agent.AgentFlock)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}