using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steerd Cohesion")]
public class SteerdCohesionBehavior : FilteredFlockBehavior
{
    private Vector2 currentVelocity;

    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent _agent, List<Transform> _context, Flock _flock)
    {
        // 이웃이 존재하지 않으면, zero 리턴
        if (_context.Count == 0) return Vector2.zero;

        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? _context : filter.Filter(_agent, _context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= _context.Count;

        cohesionMove -= (Vector2)_agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(_agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}