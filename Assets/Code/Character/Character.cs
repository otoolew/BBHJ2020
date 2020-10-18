using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour
{
    public abstract float MoveSpeed { get; set; }

    public abstract float RotationSpeed { get; set; }

    public abstract void Move(Vector3 vector);
}
