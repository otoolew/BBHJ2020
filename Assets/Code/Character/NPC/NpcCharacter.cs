using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcCharacter : Character {

    #region Declarations
    [SerializeField] public NpcCharacterData _characterData;
    public NpcCharacterData CharacterData { get => _characterData; set => _characterData = value; }

    [SerializeField] public NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent { get => _navAgent; set => _navAgent = value; }

    [SerializeField] public Animator animator;
    public override Animator Animator { get => animator; set => animator = value; }

    public override float MoveSpeed { get { return _characterData.moveSpeed; } }

    public override float RotationSpeed { get { return _characterData.rotationSpeed; } }

    public NpcGroup ActiveGroup { get; protected set; }


    #endregion

    #region MonoBehavior Overrides
    private void Awake() 
    {
    }

    private void Update() 
    {
        MoveAnimation(_navAgent.velocity.magnitude);
    }
    #endregion

    #region Methods
    public void Setup(NpcGroup group) {

    }

    public override void MoveAnimation(float value)
    {
        animator.SetFloat("MoveRate", value);
    }

    private void OnValidate()
    {
        if(_navAgent == null)
        {
            Debug.LogError("Set NavMesh Comp");
        }
    }
    #endregion
}
