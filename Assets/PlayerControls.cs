// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Battle"",
            ""id"": ""0c9baf4d-3251-473b-8367-85632241437b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6fb3a9ec-769a-4a81-a382-93192844a797"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f038c5ee-6c2d-4108-bcb1-6de035def9de"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell1"",
                    ""type"": ""Button"",
                    ""id"": ""5051e8dd-5165-4aad-924d-f8775422de2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell2"",
                    ""type"": ""Button"",
                    ""id"": ""289f9890-0122-4137-b5c0-a01a44cde8fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell3"",
                    ""type"": ""Button"",
                    ""id"": ""efd2b622-12e5-4477-a525-2845777aef74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spell4"",
                    ""type"": ""Button"",
                    ""id"": ""d0e39c88-96f8-4a14-8af7-6e5a78221df8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d71e4b47-0796-40a8-a260-52115745c45d"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b90bbba-30f8-4ff8-9162-284c938cf990"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""790a4794-a708-4c09-a029-29abff75009f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c929b0f-73f9-43d8-b348-d854bd7a08ec"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f253cfda-bfd5-4ebb-a49b-ef48d5900d5c"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92a12828-bc7d-4498-965a-c50da9a16ff4"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spell3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CharSelection"",
            ""id"": ""ea08e204-2ea7-4349-87fa-b4c9db48ba8c"",
            ""actions"": [
                {
                    ""name"": ""JoinPlayer"",
                    ""type"": ""Button"",
                    ""id"": ""e1131421-ed9c-4a8d-929d-0e992152e895"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""c4044925-e2a4-47b0-b2bf-727faf56774b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""d3d334b7-3d1b-4835-b081-5d3ba031eb79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ready"",
                    ""type"": ""Button"",
                    ""id"": ""036550cb-a6f8-4c23-b1ba-1c27aeadd2aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fbfcb79c-8f33-4c5b-a607-648dc35809dc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoinPlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03325085-48d3-44be-aded-f3d852503372"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""839588e0-d25c-4088-ba99-c661b6af9c4b"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04ae0133-4ae3-4dcd-b601-776ee7e0e7d0"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ready"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SpellShop"",
            ""id"": ""95fdb182-0e67-4cca-ac22-801398c78317"",
            ""actions"": [
                {
                    ""name"": ""LeftButton"",
                    ""type"": ""Button"",
                    ""id"": ""fd088bca-2309-4e15-a6ed-4130bcf828ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightButton"",
                    ""type"": ""Button"",
                    ""id"": ""fd75c372-7374-43b7-af85-277b1ca2725e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UpButton"",
                    ""type"": ""Button"",
                    ""id"": ""b69937c3-5939-4609-8173-7dcf7a9d6663"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DownButton"",
                    ""type"": ""Button"",
                    ""id"": ""9c23a2eb-328c-453c-81c0-c47d6ed19733"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonA"",
                    ""type"": ""Button"",
                    ""id"": ""f924c55a-c75e-49fb-878e-d8011aa16ecb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonB"",
                    ""type"": ""Button"",
                    ""id"": ""b83b9316-4689-40d2-98a6-306c20c22c5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonY"",
                    ""type"": ""Button"",
                    ""id"": ""73a8f3dd-ea07-4e02-be0e-6b971cb0b05b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonStart"",
                    ""type"": ""Button"",
                    ""id"": ""137a53b9-0737-4027-ad87-ae0991f8fb6c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e9d715fc-1c87-4a25-b34a-d330c0ca0c9c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28a797f1-1cab-412d-b2e0-1e3484fb0eff"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c5e1371-7a41-48fd-b7a1-3a5706046f4f"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa2d2449-12e6-421c-a39f-77ea96a16e0f"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae7a713c-ac16-439d-ad21-fb88831dbbaa"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19e13ed3-eef9-4b3d-b051-3b883781a7a6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2e1950f-ba23-4082-ad4f-b50ef6cc76ed"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98fbda15-7cf0-4eaa-9ee1-ace44de7577b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainMenu"",
            ""id"": ""f422a7e4-9b7d-4b6a-a69a-83e7014fa3ba"",
            ""actions"": [
                {
                    ""name"": ""PreviousButton"",
                    ""type"": ""Button"",
                    ""id"": ""4e1c54c5-2e19-4717-a996-d70dc1f7ece0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextButton"",
                    ""type"": ""Button"",
                    ""id"": ""f9f9b5b8-c2eb-4670-8c64-116cb18d531c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClickButton"",
                    ""type"": ""Button"",
                    ""id"": ""9e8c701f-ac07-484a-838b-8f33cfb13e60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextPage"",
                    ""type"": ""Button"",
                    ""id"": ""4ab021c8-c09b-45b6-a8c0-26c5a31beeff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousPage"",
                    ""type"": ""Button"",
                    ""id"": ""89010769-775e-4155-ae9f-ab1bb0742c00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CloseHelp"",
                    ""type"": ""Button"",
                    ""id"": ""f9b2d4ee-f1dc-4a37-aac4-6f5a4e034a6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""93d08e9c-a10a-489e-8718-fba391bb0673"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""053fc407-bce5-4d3e-9801-b3313e7d51e8"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00ca8882-eb9c-4ba7-9380-765b0b86a38c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca5c972a-b4c2-4d48-9248-85600830c7c1"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextPage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdcee0e7-c99e-40bd-a58f-e966c981758d"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousPage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f85bb39-809c-4aa5-8039-13be925ae7ec"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseHelp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Battle
        m_Battle = asset.FindActionMap("Battle", throwIfNotFound: true);
        m_Battle_Move = m_Battle.FindAction("Move", throwIfNotFound: true);
        m_Battle_Aim = m_Battle.FindAction("Aim", throwIfNotFound: true);
        m_Battle_Spell1 = m_Battle.FindAction("Spell1", throwIfNotFound: true);
        m_Battle_Spell2 = m_Battle.FindAction("Spell2", throwIfNotFound: true);
        m_Battle_Spell3 = m_Battle.FindAction("Spell3", throwIfNotFound: true);
        m_Battle_Spell4 = m_Battle.FindAction("Spell4", throwIfNotFound: true);
        // CharSelection
        m_CharSelection = asset.FindActionMap("CharSelection", throwIfNotFound: true);
        m_CharSelection_JoinPlayer = m_CharSelection.FindAction("JoinPlayer", throwIfNotFound: true);
        m_CharSelection_PreviousCharacter = m_CharSelection.FindAction("PreviousCharacter", throwIfNotFound: true);
        m_CharSelection_NextCharacter = m_CharSelection.FindAction("NextCharacter", throwIfNotFound: true);
        m_CharSelection_Ready = m_CharSelection.FindAction("Ready", throwIfNotFound: true);
        // SpellShop
        m_SpellShop = asset.FindActionMap("SpellShop", throwIfNotFound: true);
        m_SpellShop_LeftButton = m_SpellShop.FindAction("LeftButton", throwIfNotFound: true);
        m_SpellShop_RightButton = m_SpellShop.FindAction("RightButton", throwIfNotFound: true);
        m_SpellShop_UpButton = m_SpellShop.FindAction("UpButton", throwIfNotFound: true);
        m_SpellShop_DownButton = m_SpellShop.FindAction("DownButton", throwIfNotFound: true);
        m_SpellShop_ButtonA = m_SpellShop.FindAction("ButtonA", throwIfNotFound: true);
        m_SpellShop_ButtonB = m_SpellShop.FindAction("ButtonB", throwIfNotFound: true);
        m_SpellShop_ButtonY = m_SpellShop.FindAction("ButtonY", throwIfNotFound: true);
        m_SpellShop_ButtonStart = m_SpellShop.FindAction("ButtonStart", throwIfNotFound: true);
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_PreviousButton = m_MainMenu.FindAction("PreviousButton", throwIfNotFound: true);
        m_MainMenu_NextButton = m_MainMenu.FindAction("NextButton", throwIfNotFound: true);
        m_MainMenu_ClickButton = m_MainMenu.FindAction("ClickButton", throwIfNotFound: true);
        m_MainMenu_NextPage = m_MainMenu.FindAction("NextPage", throwIfNotFound: true);
        m_MainMenu_PreviousPage = m_MainMenu.FindAction("PreviousPage", throwIfNotFound: true);
        m_MainMenu_CloseHelp = m_MainMenu.FindAction("CloseHelp", throwIfNotFound: true);
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

    // Battle
    private readonly InputActionMap m_Battle;
    private IBattleActions m_BattleActionsCallbackInterface;
    private readonly InputAction m_Battle_Move;
    private readonly InputAction m_Battle_Aim;
    private readonly InputAction m_Battle_Spell1;
    private readonly InputAction m_Battle_Spell2;
    private readonly InputAction m_Battle_Spell3;
    private readonly InputAction m_Battle_Spell4;
    public struct BattleActions
    {
        private @PlayerControls m_Wrapper;
        public BattleActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Battle_Move;
        public InputAction @Aim => m_Wrapper.m_Battle_Aim;
        public InputAction @Spell1 => m_Wrapper.m_Battle_Spell1;
        public InputAction @Spell2 => m_Wrapper.m_Battle_Spell2;
        public InputAction @Spell3 => m_Wrapper.m_Battle_Spell3;
        public InputAction @Spell4 => m_Wrapper.m_Battle_Spell4;
        public InputActionMap Get() { return m_Wrapper.m_Battle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleActions set) { return set.Get(); }
        public void SetCallbacks(IBattleActions instance)
        {
            if (m_Wrapper.m_BattleActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnMove;
                @Aim.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnAim;
                @Spell1.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell1;
                @Spell1.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell1;
                @Spell1.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell1;
                @Spell2.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell2;
                @Spell2.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell2;
                @Spell2.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell2;
                @Spell3.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell3;
                @Spell3.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell3;
                @Spell3.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell3;
                @Spell4.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell4;
                @Spell4.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell4;
                @Spell4.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSpell4;
            }
            m_Wrapper.m_BattleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Spell1.started += instance.OnSpell1;
                @Spell1.performed += instance.OnSpell1;
                @Spell1.canceled += instance.OnSpell1;
                @Spell2.started += instance.OnSpell2;
                @Spell2.performed += instance.OnSpell2;
                @Spell2.canceled += instance.OnSpell2;
                @Spell3.started += instance.OnSpell3;
                @Spell3.performed += instance.OnSpell3;
                @Spell3.canceled += instance.OnSpell3;
                @Spell4.started += instance.OnSpell4;
                @Spell4.performed += instance.OnSpell4;
                @Spell4.canceled += instance.OnSpell4;
            }
        }
    }
    public BattleActions @Battle => new BattleActions(this);

    // CharSelection
    private readonly InputActionMap m_CharSelection;
    private ICharSelectionActions m_CharSelectionActionsCallbackInterface;
    private readonly InputAction m_CharSelection_JoinPlayer;
    private readonly InputAction m_CharSelection_PreviousCharacter;
    private readonly InputAction m_CharSelection_NextCharacter;
    private readonly InputAction m_CharSelection_Ready;
    public struct CharSelectionActions
    {
        private @PlayerControls m_Wrapper;
        public CharSelectionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @JoinPlayer => m_Wrapper.m_CharSelection_JoinPlayer;
        public InputAction @PreviousCharacter => m_Wrapper.m_CharSelection_PreviousCharacter;
        public InputAction @NextCharacter => m_Wrapper.m_CharSelection_NextCharacter;
        public InputAction @Ready => m_Wrapper.m_CharSelection_Ready;
        public InputActionMap Get() { return m_Wrapper.m_CharSelection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharSelectionActions set) { return set.Get(); }
        public void SetCallbacks(ICharSelectionActions instance)
        {
            if (m_Wrapper.m_CharSelectionActionsCallbackInterface != null)
            {
                @JoinPlayer.started -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnJoinPlayer;
                @JoinPlayer.performed -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnJoinPlayer;
                @JoinPlayer.canceled -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnJoinPlayer;
                @PreviousCharacter.started -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnPreviousCharacter;
                @PreviousCharacter.performed -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnPreviousCharacter;
                @PreviousCharacter.canceled -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnPreviousCharacter;
                @NextCharacter.started -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnNextCharacter;
                @NextCharacter.performed -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnNextCharacter;
                @NextCharacter.canceled -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnNextCharacter;
                @Ready.started -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnReady;
                @Ready.performed -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnReady;
                @Ready.canceled -= m_Wrapper.m_CharSelectionActionsCallbackInterface.OnReady;
            }
            m_Wrapper.m_CharSelectionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @JoinPlayer.started += instance.OnJoinPlayer;
                @JoinPlayer.performed += instance.OnJoinPlayer;
                @JoinPlayer.canceled += instance.OnJoinPlayer;
                @PreviousCharacter.started += instance.OnPreviousCharacter;
                @PreviousCharacter.performed += instance.OnPreviousCharacter;
                @PreviousCharacter.canceled += instance.OnPreviousCharacter;
                @NextCharacter.started += instance.OnNextCharacter;
                @NextCharacter.performed += instance.OnNextCharacter;
                @NextCharacter.canceled += instance.OnNextCharacter;
                @Ready.started += instance.OnReady;
                @Ready.performed += instance.OnReady;
                @Ready.canceled += instance.OnReady;
            }
        }
    }
    public CharSelectionActions @CharSelection => new CharSelectionActions(this);

    // SpellShop
    private readonly InputActionMap m_SpellShop;
    private ISpellShopActions m_SpellShopActionsCallbackInterface;
    private readonly InputAction m_SpellShop_LeftButton;
    private readonly InputAction m_SpellShop_RightButton;
    private readonly InputAction m_SpellShop_UpButton;
    private readonly InputAction m_SpellShop_DownButton;
    private readonly InputAction m_SpellShop_ButtonA;
    private readonly InputAction m_SpellShop_ButtonB;
    private readonly InputAction m_SpellShop_ButtonY;
    private readonly InputAction m_SpellShop_ButtonStart;
    public struct SpellShopActions
    {
        private @PlayerControls m_Wrapper;
        public SpellShopActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftButton => m_Wrapper.m_SpellShop_LeftButton;
        public InputAction @RightButton => m_Wrapper.m_SpellShop_RightButton;
        public InputAction @UpButton => m_Wrapper.m_SpellShop_UpButton;
        public InputAction @DownButton => m_Wrapper.m_SpellShop_DownButton;
        public InputAction @ButtonA => m_Wrapper.m_SpellShop_ButtonA;
        public InputAction @ButtonB => m_Wrapper.m_SpellShop_ButtonB;
        public InputAction @ButtonY => m_Wrapper.m_SpellShop_ButtonY;
        public InputAction @ButtonStart => m_Wrapper.m_SpellShop_ButtonStart;
        public InputActionMap Get() { return m_Wrapper.m_SpellShop; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpellShopActions set) { return set.Get(); }
        public void SetCallbacks(ISpellShopActions instance)
        {
            if (m_Wrapper.m_SpellShopActionsCallbackInterface != null)
            {
                @LeftButton.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnLeftButton;
                @LeftButton.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnLeftButton;
                @LeftButton.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnLeftButton;
                @RightButton.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnRightButton;
                @RightButton.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnRightButton;
                @RightButton.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnRightButton;
                @UpButton.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnUpButton;
                @UpButton.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnUpButton;
                @UpButton.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnUpButton;
                @DownButton.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnDownButton;
                @DownButton.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnDownButton;
                @DownButton.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnDownButton;
                @ButtonA.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonA;
                @ButtonA.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonA;
                @ButtonA.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonA;
                @ButtonB.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonB;
                @ButtonB.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonB;
                @ButtonB.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonB;
                @ButtonY.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonY;
                @ButtonY.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonY;
                @ButtonY.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonY;
                @ButtonStart.started -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonStart;
                @ButtonStart.performed -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonStart;
                @ButtonStart.canceled -= m_Wrapper.m_SpellShopActionsCallbackInterface.OnButtonStart;
            }
            m_Wrapper.m_SpellShopActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftButton.started += instance.OnLeftButton;
                @LeftButton.performed += instance.OnLeftButton;
                @LeftButton.canceled += instance.OnLeftButton;
                @RightButton.started += instance.OnRightButton;
                @RightButton.performed += instance.OnRightButton;
                @RightButton.canceled += instance.OnRightButton;
                @UpButton.started += instance.OnUpButton;
                @UpButton.performed += instance.OnUpButton;
                @UpButton.canceled += instance.OnUpButton;
                @DownButton.started += instance.OnDownButton;
                @DownButton.performed += instance.OnDownButton;
                @DownButton.canceled += instance.OnDownButton;
                @ButtonA.started += instance.OnButtonA;
                @ButtonA.performed += instance.OnButtonA;
                @ButtonA.canceled += instance.OnButtonA;
                @ButtonB.started += instance.OnButtonB;
                @ButtonB.performed += instance.OnButtonB;
                @ButtonB.canceled += instance.OnButtonB;
                @ButtonY.started += instance.OnButtonY;
                @ButtonY.performed += instance.OnButtonY;
                @ButtonY.canceled += instance.OnButtonY;
                @ButtonStart.started += instance.OnButtonStart;
                @ButtonStart.performed += instance.OnButtonStart;
                @ButtonStart.canceled += instance.OnButtonStart;
            }
        }
    }
    public SpellShopActions @SpellShop => new SpellShopActions(this);

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private IMainMenuActions m_MainMenuActionsCallbackInterface;
    private readonly InputAction m_MainMenu_PreviousButton;
    private readonly InputAction m_MainMenu_NextButton;
    private readonly InputAction m_MainMenu_ClickButton;
    private readonly InputAction m_MainMenu_NextPage;
    private readonly InputAction m_MainMenu_PreviousPage;
    private readonly InputAction m_MainMenu_CloseHelp;
    public struct MainMenuActions
    {
        private @PlayerControls m_Wrapper;
        public MainMenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PreviousButton => m_Wrapper.m_MainMenu_PreviousButton;
        public InputAction @NextButton => m_Wrapper.m_MainMenu_NextButton;
        public InputAction @ClickButton => m_Wrapper.m_MainMenu_ClickButton;
        public InputAction @NextPage => m_Wrapper.m_MainMenu_NextPage;
        public InputAction @PreviousPage => m_Wrapper.m_MainMenu_PreviousPage;
        public InputAction @CloseHelp => m_Wrapper.m_MainMenu_CloseHelp;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void SetCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterface != null)
            {
                @PreviousButton.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPreviousButton;
                @PreviousButton.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPreviousButton;
                @PreviousButton.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPreviousButton;
                @NextButton.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNextButton;
                @NextButton.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNextButton;
                @NextButton.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNextButton;
                @ClickButton.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnClickButton;
                @ClickButton.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnClickButton;
                @ClickButton.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnClickButton;
                @NextPage.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNextPage;
                @NextPage.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNextPage;
                @NextPage.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnNextPage;
                @PreviousPage.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPreviousPage;
                @PreviousPage.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPreviousPage;
                @PreviousPage.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnPreviousPage;
                @CloseHelp.started -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnCloseHelp;
                @CloseHelp.performed -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnCloseHelp;
                @CloseHelp.canceled -= m_Wrapper.m_MainMenuActionsCallbackInterface.OnCloseHelp;
            }
            m_Wrapper.m_MainMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PreviousButton.started += instance.OnPreviousButton;
                @PreviousButton.performed += instance.OnPreviousButton;
                @PreviousButton.canceled += instance.OnPreviousButton;
                @NextButton.started += instance.OnNextButton;
                @NextButton.performed += instance.OnNextButton;
                @NextButton.canceled += instance.OnNextButton;
                @ClickButton.started += instance.OnClickButton;
                @ClickButton.performed += instance.OnClickButton;
                @ClickButton.canceled += instance.OnClickButton;
                @NextPage.started += instance.OnNextPage;
                @NextPage.performed += instance.OnNextPage;
                @NextPage.canceled += instance.OnNextPage;
                @PreviousPage.started += instance.OnPreviousPage;
                @PreviousPage.performed += instance.OnPreviousPage;
                @PreviousPage.canceled += instance.OnPreviousPage;
                @CloseHelp.started += instance.OnCloseHelp;
                @CloseHelp.performed += instance.OnCloseHelp;
                @CloseHelp.canceled += instance.OnCloseHelp;
            }
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);
    public interface IBattleActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSpell1(InputAction.CallbackContext context);
        void OnSpell2(InputAction.CallbackContext context);
        void OnSpell3(InputAction.CallbackContext context);
        void OnSpell4(InputAction.CallbackContext context);
    }
    public interface ICharSelectionActions
    {
        void OnJoinPlayer(InputAction.CallbackContext context);
        void OnPreviousCharacter(InputAction.CallbackContext context);
        void OnNextCharacter(InputAction.CallbackContext context);
        void OnReady(InputAction.CallbackContext context);
    }
    public interface ISpellShopActions
    {
        void OnLeftButton(InputAction.CallbackContext context);
        void OnRightButton(InputAction.CallbackContext context);
        void OnUpButton(InputAction.CallbackContext context);
        void OnDownButton(InputAction.CallbackContext context);
        void OnButtonA(InputAction.CallbackContext context);
        void OnButtonB(InputAction.CallbackContext context);
        void OnButtonY(InputAction.CallbackContext context);
        void OnButtonStart(InputAction.CallbackContext context);
    }
    public interface IMainMenuActions
    {
        void OnPreviousButton(InputAction.CallbackContext context);
        void OnNextButton(InputAction.CallbackContext context);
        void OnClickButton(InputAction.CallbackContext context);
        void OnNextPage(InputAction.CallbackContext context);
        void OnPreviousPage(InputAction.CallbackContext context);
        void OnCloseHelp(InputAction.CallbackContext context);
    }
}
