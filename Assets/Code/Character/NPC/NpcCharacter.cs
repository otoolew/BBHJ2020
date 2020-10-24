using UnityEngine;
using UnityEngine.AI;

public class NpcCharacter : Character {
    #region Declarations
    [SerializeField] private NpcCharacterData _characterData;
    public NpcCharacterData CharacterData { get => _characterData; set => _characterData = value; }

    [SerializeField] private NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent { get => _navAgent; set => _navAgent = value; }

    [SerializeField] private Animator _animator;
    public override Animator Animator { get => _animator; set => _animator = value; }

    [SerializeField] private ParticleSystem _scareParticle;

    public override float MoveSpeed { get { return _characterData.moveSpeed; } }
    public override float RotationSpeed { get { return _characterData.rotationSpeed; } }

    public NpcGroup ActiveGroup { get; protected set; }
    #endregion

    #region MonoBehavior Overrides
    private void Awake() {
    }

    private void Update() {
        _navAgent.SetDestination(ActiveGroup.transform.position);
        MoveAnimation(_navAgent.velocity.magnitude);
    }
    #endregion

    #region Methods
    public void SetupCharacterGroup(NpcGroup group) {
        ActiveGroup = group;

        _navAgent.speed = MoveSpeed;
        _navAgent.angularSpeed = RotationSpeed;
    }

    public override void MoveAnimation(float value) {
        _animator.SetFloat("MoveRate", value);
    }

    public void AddScareValue(float fearValue) {
        _scareParticle.Play();
        Animator.SetTrigger("Terrified");
        ActiveGroup.AddFearValue(fearValue);
    }

    private void OnValidate() {
        if (_navAgent == null) {
            Debug.LogError("Set NavMesh Comp");
        }
    }
    #endregion
}
