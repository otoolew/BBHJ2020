using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : Character 
{
    [SerializeField] private PlayerCharacterInput inputActions;
    public PlayerCharacterInput InputActions { get => inputActions; set => inputActions = value; }

    [SerializeField] private CharacterController characterController;
    public CharacterController CharacterController { get => characterController; set => characterController = value; }

    [SerializeField] private Animator animator;
    public override Animator Animator { get => animator; set => animator = value; }

    [SerializeField] private float moveSpeed = 10;
    public override float MoveSpeed { get => moveSpeed; }

    //[SerializeField] private float _maxSpeed = 10;
    //public override float MaxSpeed { get => _maxSpeed; }

    [SerializeField] private float rotationSpeed = 30;
    public override float RotationSpeed { get => rotationSpeed; }

    #region Monobehaviour
    protected void Awake() 
    {
        inputActions = new PlayerCharacterInput();
        inputActions.PlayerCharacter.Interact.performed += OnInteract;
    }
    private void OnEnable() 
    {
        inputActions.PlayerCharacter.Enable();
    }
    private void OnDisable() 
    {
        inputActions.PlayerCharacter.Disable();
    }
    private void OnDestroy() 
    {
        //inputActions.PlayerCharacter.Movement.performed -= OnMove;
    }

    // Start is called before the first frame update
    void Start() 
    {

    }

    // Update is called once per frame
    protected void Update() 
    {
        Vector2 inputVector = inputActions.PlayerCharacter.Movement.ReadValue<Vector2>();
        Move(new Vector3(inputVector.x, 0.0f, inputVector.y));
    }

    //private void OnTriggerEnter(Collider other) 
    //{
    //    Debug.Log(other.gameObject.name);
    //}
    #endregion


    #region Methods
    /// <summary>
    /// Moves the specified vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    protected virtual void Move(Vector3 vector) 
    {
        Vector3 scaledValue = vector * MoveSpeed;
        characterController.Move(scaledValue * Time.deltaTime);
        if(vector != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(vector), RotationSpeed * Time.deltaTime);
        }
        MoveAnimation(scaledValue.magnitude);
    }

    public override void MoveAnimation(float value)
    {
        Animator.SetFloat("MoveRate", value);
    }

    public void RoarAnimation(bool value)
    {
        Animator.SetBool("Roar", value);
    }
    /// <summary>
    /// Called when [interact].
    /// </summary>
    public void OnInteract(InputAction.CallbackContext callbackContext) 
    {
    }
    #endregion
}
