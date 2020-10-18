using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Character
{
    public class CharacterController : MonoBehaviour
    {
        //[SerializeField] private CharacterInput characterInput;
        //public CharacterInput CharacterInput { get => characterInput; set => characterInput = value; }


        private void Awake()
        {
            //characterInput = new CharacterInput();
        }

        private void OnEnable()
        {
            //characterInput.CharacterAction.Use.performed += OnUseButton;
        }

        private void OnDisable()
        {
            //characterInput.CharacterAction.Use.performed -= OnUseButton;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        //public void Use()
        //{

        //}
        public void OnUseButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            Debug.Log("Use Button Pressed " + value);
        }
    }
}

