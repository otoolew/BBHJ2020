using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character
{
    [SerializeField] private PlayerCharacterInput inputActions;
    public PlayerCharacterInput InputActions { get => inputActions; set => inputActions = value; }


    [SerializeField] private CharacterController characterController;
    public CharacterController CharacterController { get => characterController; set => characterController = value; }

    [SerializeField] private float moveSpeed;
    public override float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField] private float rotationSpeed;
    public override float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

    #region Monobehaviour
    private void Awake()
    {
        inputActions = new PlayerCharacterInput();
        //inputActions.PlayerCharacter.Movement.performed += OnMove;
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
    void Update()
    {
        Vector2 inputVector = inputActions.PlayerCharacter.Movement.ReadValue<Vector2>();
        if(inputVector != Vector2.zero)
        {
            Move(new Vector3(inputVector.x, 0.0f, inputVector.y));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
    #endregion


    #region Methods

    public override void Move(Vector3 vector)
    {
        if (vector.sqrMagnitude < 0.01)
            return;
        characterController.Move(vector * (moveSpeed * Time.deltaTime));
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(vector), rotationSpeed * Time.deltaTime);
    }

    public void OnInteract()
    {
        
    }
    #endregion


}
