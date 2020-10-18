using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character
{
    [SerializeField] private float moveSpeed;
    public override float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField] private float rotationSpeed;
    public override float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    #region Monobehaviour
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region PlayerControls
    public void SetUpPlayerInput(PlayerCharacterInput inputActions)
    {
        Debug.LogWarning("SetUpPlayerInput...");
        inputActions.PlayerCharacter.Movement.performed += OnMove;
        inputActions.PlayerCharacter.Enable();
    }
    #endregion

    #region Methods

    private void OnMove(InputAction.CallbackContext context)
    {
        Move(context.ReadValue<Vector2>());
    }

    public override void Move(Vector3 vector)
    {
        if (vector.sqrMagnitude < 0.01)
            return;
        transform.position += new Vector3(vector.x, 0, vector.y) * (moveSpeed * Time.deltaTime);
    }

    #endregion


}
