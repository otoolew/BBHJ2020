using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour 
{
    public abstract Animator Animator { get; set; }
    public abstract float MoveSpeed { get; }
    public abstract float RotationSpeed { get; }
    public abstract void MoveAnimation(float value);
}
