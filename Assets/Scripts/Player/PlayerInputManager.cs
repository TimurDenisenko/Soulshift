
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using System.Linq;

namespace Assets.Scripts.Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] InputAction inventoryWindow;
        [SerializeField] GameObject inventoryCanvas;
        [SerializeField] public InputAction attack;
        private InputAction[] inputs;
        private PlayerCombat pc;

        private void Awake()
        {
            pc = GetComponent<PlayerCombat>();
            inputs = new InputAction[] { attack };

            inventoryWindow.started += InventoryWindow_started;
            attack.started += (x) => pc.Attack();
        }
        void OnEnable()
        {
            inventoryWindow.Enable();
            Array.ForEach(inputs, x => x.Enable());
        }
        void OnDisable()
        {
            inventoryWindow.Disable();
            Array.ForEach(inputs, x => x.Disable());
        }

        private void InventoryWindow_started(InputAction.CallbackContext obj)
        {
            if (inventoryCanvas.activeSelf)
            {
                inventoryCanvas.SetActive(false);
                Array.ForEach(inputs, x => x.Enable());
            }
            else
            {
                inventoryCanvas.SetActive(true);
                Array.ForEach(inputs, x => x.Disable());
            }
        }
    }
}
