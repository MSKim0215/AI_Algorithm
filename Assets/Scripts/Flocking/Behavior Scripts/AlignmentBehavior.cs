using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent _agent, List<Transform> _context, Flock _flock)
    {
        if (_context.Count == 0) return _agent.transform.up;

        Vector2 alignmentMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? _context : filter.Filter(_agent, _context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= _context.Count;
        return alignmentMove;
    }
}