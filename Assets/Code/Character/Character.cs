using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Character : MonoBehaviour {
    [SerializeField] protected Animator _characterAnimator;
    public Animator CharacterAnimator { get => _characterAnimator; set => _characterAnimator = value; }

    public Vector3 Velocity { get; protected set; }
    public float ActiveMoveRate { get; protected set; }
    public float ActiveSpeed { get; protected set; }

    public abstract float MoveSpeed { get; }
    public abstract float MaxSpeed { get; }
    public abstract float RotationSpeed { get; }

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    protected virtual void Awake() {
        if (_characterAnimator == null) {
            _characterAnimator = GetComponent<Animator>();
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    protected virtual void Update() {
        // Somethin to do here?
    }

    /// <summary>
    /// Updates the animation.
    /// </summary>
    protected virtual void UpdateAnimation() {
        _characterAnimator.SetFloat("MoveRate", ActiveMoveRate);
    }
}
