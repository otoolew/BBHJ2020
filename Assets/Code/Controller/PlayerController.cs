using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    [SerializeField] private PlayerCharacterInput inputActions;
    public PlayerCharacterInput InputActions { get => inputActions; set => inputActions = value; }

    [SerializeField] private PlayerCharacter character;
    public PlayerCharacter Character { get => character; set => character = value; }

    public override void Possess(Character character)
    {
        
    }
    #region Monobehaviour
    private void Awake()
    {
        inputActions = new PlayerCharacterInput();
    }
    // Start is called before the first frame update
    void Start()
    {
        //playerInput.ActivateInput();
        inputActions.UI.Enable();
        Character.SetUpPlayerInput(inputActions);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnValidate()
    {
        if(Character == null)
        {
            Character = FindObjectOfType<PlayerCharacter>();
        }
    }
    #endregion

}
