using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent _agent, List<Transform> _context, Flock _flock)
    {
        if (_context.Count == 0) return Vector2.zero;

        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? _context : filter.Filter(_agent, _context);
        foreach (Transform item in filteredContext)
        {
            if(Vector2.SqrMagnitude(item.position - _agent.transform.position) < _flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(_agent.transform.position - item.position);
            }
        }

        if(nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}