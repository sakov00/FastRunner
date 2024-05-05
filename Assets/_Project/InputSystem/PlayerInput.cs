//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Project/InputSystem/PlayerInput.inputactions
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
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""095af603-a3c8-44a2-92c5-8e1bbcd44c34"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FirstAbility"",
                    ""type"": ""Button"",
                    ""id"": ""2ba4ec82-d3e2-4e68-98b6-9b7203a4a53d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondAbility"",
                    ""type"": ""Button"",
                    ""id"": ""14005f9f-c1f2-438d-93dc-0d7ff74847e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThirdAbility"",
                    ""type"": ""Button"",
                    ""id"": ""24a493da-2199-4747-957b-e31194f41082"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""3D Vector"",
                    ""id"": ""587b673b-eaae-4041-946d-0537ba38fbfa"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""284f0ddc-0c26-408f-9434-cfa4b698e313"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""16784e10-c49e-4ba5-9deb-b370b906e8b9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""c051b1a0-5c40-4c9e-b7c9-a2181206db38"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Forward"",
                    ""id"": ""51f95e20-4872-4bd1-917c-7f53c92da569"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Backward"",
                    ""id"": ""38bf0bd7-9f8c-42db-bcf6-ed635ce02a9c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e22b917b-d287-4e82-aee8-51c6c01334a5"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43177dd2-947e-4082-8537-d72ebdb3edc9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a7ec54b-90d8-4785-9c2a-e2fecc7f8413"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThirdAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""id"": ""6ba0c297-5c93-4549-9d48-80a1c25668e4"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""1a37601f-4617-4128-a12a-b23cadf354c8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FirstAbility"",
                    ""type"": ""Button"",
                    ""id"": ""91fd42a5-91a7-45ea-ac11-f7d0dfea3483"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondAbility"",
                    ""type"": ""Button"",
                    ""id"": ""2c0fee2c-5917-41a7-80ad-0a894e6854ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThirdAbility"",
                    ""type"": ""Button"",
                    ""id"": ""81d3be3c-5e5e-4744-8599-bd897c3bda3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""65521aa0-3032-483d-a551-179e959d9d84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0dd90fd7-0ced-4a2b-abb8-fdbaa60df256"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FirstAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""708def7f-3556-451e-b3bc-4c9962e018fc"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""610fea75-98ab-4561-8749-6b753d770b5c"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThirdAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4f38741-8893-4263-af94-f7683cd0ea77"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""571d0208-2247-43c5-b898-9f6dba49a4f3"",
                    ""path"": ""<Gamepad>/buttonWest"",
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
        m_PC_Movement = m_PC.FindAction("Movement", throwIfNotFound: true);
        m_PC_FirstAbility = m_PC.FindAction("FirstAbility", throwIfNotFound: true);
        m_PC_SecondAbility = m_PC.FindAction("SecondAbility", throwIfNotFound: true);
        m_PC_ThirdAbility = m_PC.FindAction("ThirdAbility", throwIfNotFound: true);
        // Mobile
        m_Mobile = asset.FindActionMap("Mobile", throwIfNotFound: true);
        m_Mobile_Movement = m_Mobile.FindAction("Movement", throwIfNotFound: true);
        m_Mobile_FirstAbility = m_Mobile.FindAction("FirstAbility", throwIfNotFound: true);
        m_Mobile_SecondAbility = m_Mobile.FindAction("SecondAbility", throwIfNotFound: true);
        m_Mobile_ThirdAbility = m_Mobile.FindAction("ThirdAbility", throwIfNotFound: true);
        m_Mobile_Jump = m_Mobile.FindAction("Jump", throwIfNotFound: true);
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
    private readonly InputAction m_PC_Movement;
    private readonly InputAction m_PC_FirstAbility;
    private readonly InputAction m_PC_SecondAbility;
    private readonly InputAction m_PC_ThirdAbility;
    public struct PCActions
    {
        private @PlayerInput m_Wrapper;
        public PCActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PC_Movement;
        public InputAction @FirstAbility => m_Wrapper.m_PC_FirstAbility;
        public InputAction @SecondAbility => m_Wrapper.m_PC_SecondAbility;
        public InputAction @ThirdAbility => m_Wrapper.m_PC_ThirdAbility;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void AddCallbacks(IPCActions instance)
        {
            if (instance == null || m_Wrapper.m_PCActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @FirstAbility.started += instance.OnFirstAbility;
            @FirstAbility.performed += instance.OnFirstAbility;
            @FirstAbility.canceled += instance.OnFirstAbility;
            @SecondAbility.started += instance.OnSecondAbility;
            @SecondAbility.performed += instance.OnSecondAbility;
            @SecondAbility.canceled += instance.OnSecondAbility;
            @ThirdAbility.started += instance.OnThirdAbility;
            @ThirdAbility.performed += instance.OnThirdAbility;
            @ThirdAbility.canceled += instance.OnThirdAbility;
        }

        private void UnregisterCallbacks(IPCActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @FirstAbility.started -= instance.OnFirstAbility;
            @FirstAbility.performed -= instance.OnFirstAbility;
            @FirstAbility.canceled -= instance.OnFirstAbility;
            @SecondAbility.started -= instance.OnSecondAbility;
            @SecondAbility.performed -= instance.OnSecondAbility;
            @SecondAbility.canceled -= instance.OnSecondAbility;
            @ThirdAbility.started -= instance.OnThirdAbility;
            @ThirdAbility.performed -= instance.OnThirdAbility;
            @ThirdAbility.canceled -= instance.OnThirdAbility;
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

    // Mobile
    private readonly InputActionMap m_Mobile;
    private List<IMobileActions> m_MobileActionsCallbackInterfaces = new List<IMobileActions>();
    private readonly InputAction m_Mobile_Movement;
    private readonly InputAction m_Mobile_FirstAbility;
    private readonly InputAction m_Mobile_SecondAbility;
    private readonly InputAction m_Mobile_ThirdAbility;
    private readonly InputAction m_Mobile_Jump;
    public struct MobileActions
    {
        private @PlayerInput m_Wrapper;
        public MobileActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Mobile_Movement;
        public InputAction @FirstAbility => m_Wrapper.m_Mobile_FirstAbility;
        public InputAction @SecondAbility => m_Wrapper.m_Mobile_SecondAbility;
        public InputAction @ThirdAbility => m_Wrapper.m_Mobile_ThirdAbility;
        public InputAction @Jump => m_Wrapper.m_Mobile_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Mobile; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MobileActions set) { return set.Get(); }
        public void AddCallbacks(IMobileActions instance)
        {
            if (instance == null || m_Wrapper.m_MobileActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MobileActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @FirstAbility.started += instance.OnFirstAbility;
            @FirstAbility.performed += instance.OnFirstAbility;
            @FirstAbility.canceled += instance.OnFirstAbility;
            @SecondAbility.started += instance.OnSecondAbility;
            @SecondAbility.performed += instance.OnSecondAbility;
            @SecondAbility.canceled += instance.OnSecondAbility;
            @ThirdAbility.started += instance.OnThirdAbility;
            @ThirdAbility.performed += instance.OnThirdAbility;
            @ThirdAbility.canceled += instance.OnThirdAbility;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IMobileActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @FirstAbility.started -= instance.OnFirstAbility;
            @FirstAbility.performed -= instance.OnFirstAbility;
            @FirstAbility.canceled -= instance.OnFirstAbility;
            @SecondAbility.started -= instance.OnSecondAbility;
            @SecondAbility.performed -= instance.OnSecondAbility;
            @SecondAbility.canceled -= instance.OnSecondAbility;
            @ThirdAbility.started -= instance.OnThirdAbility;
            @ThirdAbility.performed -= instance.OnThirdAbility;
            @ThirdAbility.canceled -= instance.OnThirdAbility;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IMobileActions instance)
        {
            if (m_Wrapper.m_MobileActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMobileActions instance)
        {
            foreach (var item in m_Wrapper.m_MobileActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MobileActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MobileActions @Mobile => new MobileActions(this);
    public interface IPCActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFirstAbility(InputAction.CallbackContext context);
        void OnSecondAbility(InputAction.CallbackContext context);
        void OnThirdAbility(InputAction.CallbackContext context);
    }
    public interface IMobileActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFirstAbility(InputAction.CallbackContext context);
        void OnSecondAbility(InputAction.CallbackContext context);
        void OnThirdAbility(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
