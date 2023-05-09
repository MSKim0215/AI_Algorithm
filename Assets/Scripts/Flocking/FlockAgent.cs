using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    private Collider2D agentCollider;

    public Collider2D AgentCollider
    {
        get => agentCollider;
    }

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 _velocity)
    {
        transform.up = _velocity;
        transform.position += (Vector3)_velocity * Time.deltaTime;
    }  
}