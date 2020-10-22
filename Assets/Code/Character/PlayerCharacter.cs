using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character {
    [SerializeField] private PlayerCharacterInput inputActions;
    public PlayerCharacterInput InputActions { get => inputActions; set => inputActions = value; }

    [SerializeField] protected CharacterController _characterController;
    public CharacterController CharacterController { get => _characterController; set => _characterController = value; }

    [SerializeField] private float _moveSpeed = 10;
    public override float MoveSpeed { get => _moveSpeed; }

    [SerializeField] private float _maxSpeed = 10;
    public override float MaxSpeed { get => _maxSpeed; }

    [SerializeField] private float _rotationSpeed = 30;
    public override float RotationSpeed { get => _rotationSpeed; }

    #region Monobehaviour
    protected override void Awake() {
        base.Awake();

        inputActions = new PlayerCharacterInput();
        //inputActions.PlayerCharacter.Movement.performed += OnMove;
    }
    private void OnEnable() {
        inputActions.PlayerCharacter.Enable();
    }
    private void OnDisable() {
        inputActions.PlayerCharacter.Disable();
    }
    private void OnDestroy() {
        //inputActions.PlayerCharacter.Movement.performed -= OnMove;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();

        Vector2 inputVector = inputActions.PlayerCharacter.Movement.ReadValue<Vector2>();
        if (inputVector != Vector2.zero) {
            Move(new Vector3(inputVector.x, 0.0f, inputVector.y));
        }

        UpdateAnimation();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
    }
    #endregion


    #region Methods
    /// <summary>
    /// Moves the specified vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    protected virtual void Move(Vector3 vector) {
        if (vector.sqrMagnitude < 0.01) {
            Velocity = Vector3.zero;
            ActiveSpeed = 0;
            ActiveMoveRate = 0;
            return;
        }

        Velocity = vector * MoveSpeed;
        ActiveSpeed = Velocity.magnitude;
        if (ActiveSpeed > MaxSpeed) {
            float reduction = MaxSpeed / ActiveSpeed;
            Velocity *= reduction;
            ActiveSpeed = MaxSpeed;
        }
        ActiveMoveRate = Mathf.Clamp(ActiveSpeed / MaxSpeed, 0f, 1f);

        _characterController.Move(Velocity * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(vector), RotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Called when [interact].
    /// </summary>
    public void OnInteract() {

    }
    #endregion
}
