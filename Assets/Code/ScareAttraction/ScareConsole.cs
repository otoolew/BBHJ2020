using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private Timer cooldownTimer;
    public Timer CooldownTimer { get => cooldownTimer; set => cooldownTimer = value; }

    //[SerializeField] private TMP_Text status_Text;
    //public TMP_Text Status_Text { get => status_Text; set => status_Text = value; }

    //[SerializeField] private TMP_Text count_Text;
    //public TMP_Text Count_Text { get => count_Text; set => count_Text = value; }

    [SerializeField] private ScareAttraction scareAttraction;
    public ScareAttraction ScareAttraction { get => scareAttraction; set => scareAttraction = value; }

    [SerializeField] private bool attractionRunning;
    public bool AttractionRunning { get => attractionRunning; set => attractionRunning = value; }

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
        if (cooldownTimer.Finished)
        {
            scareAttraction.gameObject.SetActive(false);
        }
        else
        {
            scareAttraction.gameObject.SetActive(true);
        }
        //count_Text.text = cooldownTimer.GetIntTime().ToString();
        //if(cooldownTimer.Finished == true)
        //{
        //    status_Text.text = "READY";
        //    count_Text.text = "";
        //}
    }

    private void OnFire(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(gameObject.name + " Interaction!");

        if (cooldownTimer.Finished)
        {
            cooldownTimer.ResetTimer();
        }
        else
        {
            Debug.Log("Not Ready To Fire Scare");
        }

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
