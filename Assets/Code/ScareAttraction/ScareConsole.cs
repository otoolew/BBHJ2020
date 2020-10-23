using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScareConsole : MonoBehaviour
{
    [SerializeField] private PlayerCharacterInput inputActions;
    public PlayerCharacterInput InputActions { get => inputActions; set => inputActions = value; }

    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    [SerializeField] private Light consoleLight;
    public Light ConsoleLight { get => consoleLight; set => consoleLight = value; }

    [SerializeField] private ScareAttraction scareAttraction;
    public ScareAttraction ScareAttraction { get => scareAttraction; set => scareAttraction = value; }

    private void Awake()
    {
        inputActions = new PlayerCharacterInput();
        inputActions.ScareConsole.Fire.performed += OnFire;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator.enabled = false;
        consoleLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnFire(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(gameObject.name + " Interaction!");
    }
    private void OnTriggerEnter(Collider other)
    {
        animator.enabled = true;
        consoleLight.enabled = true;
        inputActions.ScareConsole.Enable();
    }

    private void OnTriggerExit(Collider other)
    {
        animator.enabled = false;
        consoleLight.enabled = false;
        inputActions.ScareConsole.Disable();
    }
}
