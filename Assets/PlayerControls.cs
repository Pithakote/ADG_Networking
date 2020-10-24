//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.0
//     from Assets/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""c1cd40b4-53c7-4b14-9d66-bdfc2b86ef7f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2823d281-dd27-4e9b-9710-845913730a57"",
                    ""expectedControlType"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""bb176a79-c752-4379-af60-660ea088e154"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CancelOrBack"",
                    ""type"": ""Button"",
                    ""id"": ""0753243a-767e-432c-b7d5-cf64bf18941d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""5f4a57ae-2f15-4586-8b1c-502747fc8111"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimingController"",
                    ""type"": ""Value"",
                    ""id"": ""c63d49e2-1cc4-4161-86c9-80e45e89cced"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimingMouse"",
                    ""type"": ""Value"",
                    ""id"": ""e291a7c2-3b4b-4bc0-af0d-090c1ba947f3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""61302bdf-1e19-4a56-bdaf-e73445f8b4ef"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CancelOrBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2fe5137-4b5c-40f8-a0c1-10f6f358d1a6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CancelOrBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7e6e015-c539-4008-8c71-2775eea56a70"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bb579bf0-31c2-4ef4-b504-6c4e9da46db5"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8b3b2f3e-cda8-43ac-b861-eb4bf5ef4d1c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a1bd2681-28fe-43b9-b301-115f721c638f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3a080b7a-9478-4814-b7ef-aaf6b9d2487f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3a5ae122-d224-43a9-94bb-72632beb3df7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bed5e461-d24e-4805-82a3-d4bc3e5e1396"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""UI;Keyboard;Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36e0dccd-a553-47f7-b99e-930cc051413c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""UI;Keyboard;Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d9b71ae-706a-459d-978b-4368128225c0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""455b5e7d-f0ee-4b15-a433-3dd35f92bbef"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fee4b691-1bd3-4838-9b58-e033a0637035"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AimingController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6153db3-3f06-4147-851b-50a8d6e256f8"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AimingMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""bindingGroup"": ""UI"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mobile"",
            ""bindingGroup"": ""Mobile"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Select = m_PlayerMovement.FindAction("Select", throwIfNotFound: true);
        m_PlayerMovement_CancelOrBack = m_PlayerMovement.FindAction("CancelOrBack", throwIfNotFound: true);
        m_PlayerMovement_Fire = m_PlayerMovement.FindAction("Fire", throwIfNotFound: true);
        m_PlayerMovement_AimingController = m_PlayerMovement.FindAction("AimingController", throwIfNotFound: true);
        m_PlayerMovement_AimingMouse = m_PlayerMovement.FindAction("AimingMouse", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Select;
    private readonly InputAction m_PlayerMovement_CancelOrBack;
    private readonly InputAction m_PlayerMovement_Fire;
    private readonly InputAction m_PlayerMovement_AimingController;
    private readonly InputAction m_PlayerMovement_AimingMouse;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Select => m_Wrapper.m_PlayerMovement_Select;
        public InputAction @CancelOrBack => m_Wrapper.m_PlayerMovement_CancelOrBack;
        public InputAction @Fire => m_Wrapper.m_PlayerMovement_Fire;
        public InputAction @AimingController => m_Wrapper.m_PlayerMovement_AimingController;
        public InputAction @AimingMouse => m_Wrapper.m_PlayerMovement_AimingMouse;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Select.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnSelect;
                @CancelOrBack.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCancelOrBack;
                @CancelOrBack.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCancelOrBack;
                @CancelOrBack.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCancelOrBack;
                @Fire.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnFire;
                @AimingController.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimingController;
                @AimingController.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimingController;
                @AimingController.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimingController;
                @AimingMouse.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimingMouse;
                @AimingMouse.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimingMouse;
                @AimingMouse.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnAimingMouse;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @CancelOrBack.started += instance.OnCancelOrBack;
                @CancelOrBack.performed += instance.OnCancelOrBack;
                @CancelOrBack.canceled += instance.OnCancelOrBack;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @AimingController.started += instance.OnAimingController;
                @AimingController.performed += instance.OnAimingController;
                @AimingController.canceled += instance.OnAimingController;
                @AimingMouse.started += instance.OnAimingMouse;
                @AimingMouse.performed += instance.OnAimingMouse;
                @AimingMouse.canceled += instance.OnAimingMouse;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_UISchemeIndex = -1;
    public InputControlScheme UIScheme
    {
        get
        {
            if (m_UISchemeIndex == -1) m_UISchemeIndex = asset.FindControlSchemeIndex("UI");
            return asset.controlSchemes[m_UISchemeIndex];
        }
    }
    private int m_MobileSchemeIndex = -1;
    public InputControlScheme MobileScheme
    {
        get
        {
            if (m_MobileSchemeIndex == -1) m_MobileSchemeIndex = asset.FindControlSchemeIndex("Mobile");
            return asset.controlSchemes[m_MobileSchemeIndex];
        }
    }
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnCancelOrBack(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnAimingController(InputAction.CallbackContext context);
        void OnAimingMouse(InputAction.CallbackContext context);
    }
}
