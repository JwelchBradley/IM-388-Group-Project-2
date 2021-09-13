// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/MasterInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MasterInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MasterInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MasterInput"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""9c59630d-27b4-427b-aa13-937877cc3676"",
            ""actions"": [
                {
                    ""name"": ""Pause Game"",
                    ""type"": ""Button"",
                    ""id"": ""5f85a334-f9de-43bc-afff-378ca56e371a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Test"",
                    ""type"": ""Button"",
                    ""id"": ""d67f3256-288a-4e3e-9aa1-15eb4b76e476"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space Test"",
                    ""type"": ""Button"",
                    ""id"": ""f07754b6-73ee-4e8b-993f-530e41ec67cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1fe61faf-ce7d-4f8f-bcb2-85b945a9f9bb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Pause Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1da84778-1484-4c7b-acf6-6380fc108496"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Mouse Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb2644b0-1d69-43e3-adde-206bb30597c7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Space Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_PauseGame = m_Menu.FindAction("Pause Game", throwIfNotFound: true);
        m_Menu_MouseTest = m_Menu.FindAction("Mouse Test", throwIfNotFound: true);
        m_Menu_SpaceTest = m_Menu.FindAction("Space Test", throwIfNotFound: true);
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

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_PauseGame;
    private readonly InputAction m_Menu_MouseTest;
    private readonly InputAction m_Menu_SpaceTest;
    public struct MenuActions
    {
        private @MasterInput m_Wrapper;
        public MenuActions(@MasterInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseGame => m_Wrapper.m_Menu_PauseGame;
        public InputAction @MouseTest => m_Wrapper.m_Menu_MouseTest;
        public InputAction @SpaceTest => m_Wrapper.m_Menu_SpaceTest;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @PauseGame.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPauseGame;
                @MouseTest.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseTest;
                @MouseTest.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseTest;
                @MouseTest.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMouseTest;
                @SpaceTest.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSpaceTest;
                @SpaceTest.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSpaceTest;
                @SpaceTest.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSpaceTest;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
                @MouseTest.started += instance.OnMouseTest;
                @MouseTest.performed += instance.OnMouseTest;
                @MouseTest.canceled += instance.OnMouseTest;
                @SpaceTest.started += instance.OnSpaceTest;
                @SpaceTest.performed += instance.OnSpaceTest;
                @SpaceTest.canceled += instance.OnSpaceTest;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IMenuActions
    {
        void OnPauseGame(InputAction.CallbackContext context);
        void OnMouseTest(InputAction.CallbackContext context);
        void OnSpaceTest(InputAction.CallbackContext context);
    }
}
