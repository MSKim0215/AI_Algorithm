using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    private List<FlockAgent> agents = new List<FlockAgent>();
    private const float AgentDesity = 0.08f;
    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;

    public FlockAgent agentPrefab;
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    public float SquareAvoidanceRadius
    {
        get => squareAvoidanceRadius;
    }

    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for(int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDesity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = $"Agent {i}";
            agents.Add(newAgent);
        }
    }

    private void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent _agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(_agent.transform.position, neighborRadius);
        foreach(Collider2D coll in contextColliders)
        {
            if(coll != _agent.AgentCollider)
            {
                context.Add(coll.transform);
            }
        }
        return context; 
    }
}