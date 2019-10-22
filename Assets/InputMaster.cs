// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputMaster : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f9ddc1f0-f746-4906-b0c9-ebb386f8e99d"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""7ed57d84-0b57-4835-8d70-dd8aafd14d57"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""46d17f3f-5119-402b-893e-1702f5cdf771"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""39ae3e0a-e7e3-478f-a47f-67460faa0f14"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""02e99726-e206-4983-a1ed-07e7dcdc6c58"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Horizontal"",
                    ""id"": ""56b31d09-7404-422a-acc1-c288126a89c7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2bd4dadd-47cc-49ec-ba57-037f7ea9b665"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""11cf8d1b-5893-4d75-853e-4cd47bb25f72"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2ef5dc3d-bcfd-4fc0-95b3-755d7db55e9f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard and Mouse"",
            ""bindingGroup"": ""KeyBoard and Mouse"",
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
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    public struct PlayerActions
    {
        private InputMaster m_Wrapper;
        public PlayerActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Shoot.started += instance.OnShoot;
                Shoot.performed += instance.OnShoot;
                Shoot.canceled += instance.OnShoot;
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyBoardandMouseSchemeIndex = -1;
    public InputControlScheme KeyBoardandMouseScheme
    {
        get
        {
            if (m_KeyBoardandMouseSchemeIndex == -1) m_KeyBoardandMouseSchemeIndex = asset.FindControlSchemeIndex("KeyBoard and Mouse");
            return asset.controlSchemes[m_KeyBoardandMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
