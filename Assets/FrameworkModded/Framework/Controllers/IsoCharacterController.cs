using System;
using UnityEngine;
using static Framework;

// Requires
public class IsoCharacterController : MonoBehaviour
{
    // Components
    protected MovementComponent _MovementComponent;

    // Variables

    protected virtual void Awake()
    {
        TryGetComponent<MovementComponent>(out _MovementComponent);
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void LookTowards(Vector3 targetPosition)
    {

    }

    protected virtual void Move(Vector2 moveVector)
    {
        _MovementComponent.SetMoveVector(moveVector);
    }
}
