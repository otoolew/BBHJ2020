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

    public override float MoveSpeed { get { return _characterData.moveSpeed; } }
    public override float MaxSpeed { get { return _characterData.maxSpeed; } }
    public override float RotationSpeed { get { return _characterData.rotationSpeed; } }

    public NpcGroup ActiveGroup { get; protected set; }
    #endregion

    #region MonoBehavior Overrides
    protected override void Awake() {
        base.Awake();


    }

    protected override void Update() {
        base.Update();


    }
    #endregion

    #region Methods
    public void Setup(NpcGroup group) {

    }
    #endregion
}
