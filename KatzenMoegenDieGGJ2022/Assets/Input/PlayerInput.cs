// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""d4a27765-3ce1-4195-9640-088ec63d1183"",
            ""actions"": [
                {
                    ""name"": ""MouseMovement"",
                    ""type"": ""Value"",
                    ""id"": ""9416b4f2-2efc-4fed-8eb5-cc089054182c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Button"",
                    ""id"": ""9906bb75-19ce-4388-8679-b8e0f73c0635"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Button"",
                    ""id"": ""32e9a6a3-877f-4b95-b35a-a67c369f2a00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""52beda25-1e30-4812-acb2-9ad9ab5f242a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""5e3f4a79-7dc4-4534-a6aa-b41c36290b67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""c9c081a2-d977-4fca-b8ab-6ce54fd1873b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ReturnBall"",
                    ""type"": ""Button"",
                    ""id"": ""378ade6a-ba19-49ef-a05c-beba18ea6422"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Push"",
                    ""type"": ""Button"",
                    ""id"": ""51b06f6b-17d1-4467-b290-d03f5c5ef0fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""ba208a69-02ff-47c8-939a-71a4016327ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bb1d3652-4c28-4d0f-9bdf-ca5ff1e9f533"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""387afb35-64c7-4b77-ae36-34f6ee2e480a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e4c99532-1a2d-4b86-9c17-6fda3a6ddcd4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1bb9f6f6-c48d-4b78-9706-9dc33ebf2d1a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""7a6983be-fdde-4d62-8e9c-405c7303a6e1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2910d85d-d1b3-4411-9756-133e64d6ef1f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f6029c18-cd37-453b-8942-7c31245a2385"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bcd8b11a-dbff-4f84-b482-1eb5a0ca36e6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba09d892-2881-4af4-b54d-92edd34b2378"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0ef9a2e-1360-4fbd-899d-cbe39338e0be"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4ea2fb5-a82b-4ef0-8ca2-a7d5affc9ed2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReturnBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5359a29a-d590-40e7-bd0e-4c07be5c8461"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Push"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04ed6b1b-fc29-47ed-b05a-2fba10b02e13"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17dcdb16-23f9-4d5d-a2c9-349b49c59db7"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_MouseMovement = m_Keyboard.FindAction("MouseMovement", throwIfNotFound: true);
        m_Keyboard_Horizontal = m_Keyboard.FindAction("Horizontal", throwIfNotFound: true);
        m_Keyboard_Vertical = m_Keyboard.FindAction("Vertical", throwIfNotFound: true);
        m_Keyboard_Jump = m_Keyboard.FindAction("Jump", throwIfNotFound: true);
        m_Keyboard_Shift = m_Keyboard.FindAction("Shift", throwIfNotFound: true);
        m_Keyboard_Throw = m_Keyboard.FindAction("Throw", throwIfNotFound: true);
        m_Keyboard_ReturnBall = m_Keyboard.FindAction("ReturnBall", throwIfNotFound: true);
        m_Keyboard_Push = m_Keyboard.FindAction("Push", throwIfNotFound: true);
        m_Keyboard_Pause = m_Keyboard.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_MouseMovement;
    private readonly InputAction m_Keyboard_Horizontal;
    private readonly InputAction m_Keyboard_Vertical;
    private readonly InputAction m_Keyboard_Jump;
    private readonly InputAction m_Keyboard_Shift;
    private readonly InputAction m_Keyboard_Throw;
    private readonly InputAction m_Keyboard_ReturnBall;
    private readonly InputAction m_Keyboard_Push;
    private readonly InputAction m_Keyboard_Pause;
    public struct KeyboardActions
    {
        private @PlayerInput m_Wrapper;
        public KeyboardActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMovement => m_Wrapper.m_Keyboard_MouseMovement;
        public InputAction @Horizontal => m_Wrapper.m_Keyboard_Horizontal;
        public InputAction @Vertical => m_Wrapper.m_Keyboard_Vertical;
        public InputAction @Jump => m_Wrapper.m_Keyboard_Jump;
        public InputAction @Shift => m_Wrapper.m_Keyboard_Shift;
        public InputAction @Throw => m_Wrapper.m_Keyboard_Throw;
        public InputAction @ReturnBall => m_Wrapper.m_Keyboard_ReturnBall;
        public InputAction @Push => m_Wrapper.m_Keyboard_Push;
        public InputAction @Pause => m_Wrapper.m_Keyboard_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @MouseMovement.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMouseMovement;
                @MouseMovement.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMouseMovement;
                @MouseMovement.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnMouseMovement;
                @Horizontal.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHorizontal;
                @Horizontal.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHorizontal;
                @Horizontal.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnHorizontal;
                @Vertical.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnVertical;
                @Vertical.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnVertical;
                @Vertical.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnVertical;
                @Jump.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnJump;
                @Shift.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnShift;
                @Throw.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnThrow;
                @ReturnBall.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnReturnBall;
                @ReturnBall.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnReturnBall;
                @ReturnBall.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnReturnBall;
                @Push.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPush;
                @Push.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPush;
                @Push.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPush;
                @Pause.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseMovement.started += instance.OnMouseMovement;
                @MouseMovement.performed += instance.OnMouseMovement;
                @MouseMovement.canceled += instance.OnMouseMovement;
                @Horizontal.started += instance.OnHorizontal;
                @Horizontal.performed += instance.OnHorizontal;
                @Horizontal.canceled += instance.OnHorizontal;
                @Vertical.started += instance.OnVertical;
                @Vertical.performed += instance.OnVertical;
                @Vertical.canceled += instance.OnVertical;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @ReturnBall.started += instance.OnReturnBall;
                @ReturnBall.performed += instance.OnReturnBall;
                @ReturnBall.canceled += instance.OnReturnBall;
                @Push.started += instance.OnPush;
                @Push.performed += instance.OnPush;
                @Push.canceled += instance.OnPush;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnMouseMovement(InputAction.CallbackContext context);
        void OnHorizontal(InputAction.CallbackContext context);
        void OnVertical(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShift(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnReturnBall(InputAction.CallbackContext context);
        void OnPush(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
