//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/InputSystem/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PC"",
            ""id"": ""f4340ef6-a24f-41cc-bb99-53fb41d43411"",
            ""actions"": [
                {
                    ""name"": ""VerticalMove"",
                    ""type"": ""Button"",
                    ""id"": ""095af603-a3c8-44a2-92c5-8e1bbcd44c34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HorizontalMove"",
                    ""type"": ""Button"",
                    ""id"": ""cd562f1b-becd-43d2-b5ca-5037e3e4c6fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""dcb2b76a-9f22-4eb9-b7e0-d3b51c9cba5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""587b673b-eaae-4041-946d-0537ba38fbfa"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""284f0ddc-0c26-408f-9434-cfa4b698e313"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""11116867-4806-4cfd-a00e-34b3e521aba4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""be5065bb-1df6-48c7-9dcf-33c9150db0c5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3f38de7f-0ab6-4211-982f-0b016d66ea63"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b2d9e34e-8864-44c5-a84a-7ce1ab0d3e21"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""13158be7-ab52-455b-b193-c19e2386c956"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PC
        m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
        m_PC_VerticalMove = m_PC.FindAction("VerticalMove", throwIfNotFound: true);
        m_PC_HorizontalMove = m_PC.FindAction("HorizontalMove", throwIfNotFound: true);
        m_PC_Jump = m_PC.FindAction("Jump", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PC
    private readonly InputActionMap m_PC;
    private List<IPCActions> m_PCActionsCallbackInterfaces = new List<IPCActions>();
    private readonly InputAction m_PC_VerticalMove;
    private readonly InputAction m_PC_HorizontalMove;
    private readonly InputAction m_PC_Jump;
    public struct PCActions
    {
        private @PlayerInput m_Wrapper;
        public PCActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @VerticalMove => m_Wrapper.m_PC_VerticalMove;
        public InputAction @HorizontalMove => m_Wrapper.m_PC_HorizontalMove;
        public InputAction @Jump => m_Wrapper.m_PC_Jump;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void AddCallbacks(IPCActions instance)
        {
            if (instance == null || m_Wrapper.m_PCActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCActionsCallbackInterfaces.Add(instance);
            @VerticalMove.started += instance.OnVerticalMove;
            @VerticalMove.performed += instance.OnVerticalMove;
            @VerticalMove.canceled += instance.OnVerticalMove;
            @HorizontalMove.started += instance.OnHorizontalMove;
            @HorizontalMove.performed += instance.OnHorizontalMove;
            @HorizontalMove.canceled += instance.OnHorizontalMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IPCActions instance)
        {
            @VerticalMove.started -= instance.OnVerticalMove;
            @VerticalMove.performed -= instance.OnVerticalMove;
            @VerticalMove.canceled -= instance.OnVerticalMove;
            @HorizontalMove.started -= instance.OnHorizontalMove;
            @HorizontalMove.performed -= instance.OnHorizontalMove;
            @HorizontalMove.canceled -= instance.OnHorizontalMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IPCActions instance)
        {
            if (m_Wrapper.m_PCActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPCActions instance)
        {
            foreach (var item in m_Wrapper.m_PCActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PCActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PCActions @PC => new PCActions(this);
    public interface IPCActions
    {
        void OnVerticalMove(InputAction.CallbackContext context);
        void OnHorizontalMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
