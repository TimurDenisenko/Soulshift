
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

namespace Assets.Scripts.Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] InputAction inventoryWindow;
        [SerializeField] GameObject inventoryCanvas;

        private void Start()
        {
            inventoryWindow.started += InventoryWindow_started;
        }
        void OnEnable()
        {
            inventoryWindow.Enable();
        }
        void OnDisable()
        {
            inventoryWindow.Disable();
        }

        private void InventoryWindow_started(InputAction.CallbackContext obj)
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }
}
