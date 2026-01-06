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
    ""version"": 1,
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""47fa3e31-d34d-472c-9062-ef1e37dc9407"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""2acbf401-dbc0-4c51-803c-6e162d652258"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""0e71e004-f704-4f92-b3c1-0f12da95de61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""08c72477-4fdf-46b4-8d37-f55cd47e22e5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f3627e39-a76d-4c9c-a7a5-f98e5399f407"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2ce31086-d088-44df-b15c-a0745de39708"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8149cd94-525c-495d-a78d-ac439e607c8e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ef122bc-7f8c-4800-9db8-28ddf9739bd6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b009c4a9-f192-4b99-96f3-471db747d02e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");


        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);

        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);

        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);

    }


    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }


    public InputBinding? bindingMask
    {
        get
        {
            return asset.bindingMask;
        }
        set
        {
            asset.bindingMask = value;
        }
    }


    public ReadOnlyArray<InputDevice>? devices
    {
        get
        {
            return asset.devices;
        }
        set
        {
            asset.devices = value;
        }
    }


    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get
        {
            return asset.controlSchemes;
        }
    }


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


    public IEnumerable<InputBinding> bindings
    {
        get
        {
            return asset.bindings;
        }
    }


    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }


    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }


    // Player
    private readonly InputActionMap m_Player;

    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();

    private readonly InputAction m_Player_Move;

    private readonly InputAction m_Player_Jump;


    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;

        public PlayerActions(@PlayerControls wrapper)
        {
            m_Wrapper = wrapper;
        }

        public InputAction @Move
        {
            get
            {
                return m_Wrapper.m_Player_Move;
            }
        }

        public InputAction @Jump
        {
            get
            {
                return m_Wrapper.m_Player_Jump;
            }
        }

        public InputActionMap Get()
        {
            return m_Wrapper.m_Player;
        }

        public void Enable()
        {
            Get().Enable();
        }

        public void Disable()
        {
            Get().Disable();
        }

        public bool enabled
        {
            get
            {
                return Get().enabled;
            }
        }

        public static implicit operator InputActionMap(PlayerActions set)
        {
            return set.Get();
        }

        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;

            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);

            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;

            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;

            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);

            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();

            AddCallbacks(instance);
        }
    }


    public PlayerActions @Player
    {
        get
        {
            return new PlayerActions(this);
        }
    }


    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }

}