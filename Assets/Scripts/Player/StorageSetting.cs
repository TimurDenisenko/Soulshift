using UnityEngine;

public class StorageSetting : MonoBehaviour
{
    [SerializeField] Tooltip tooltip;
    [SerializeField] Slot slotPrefab;
    [SerializeField] Transform canvasForDraggingItem;

    public static Tooltip SlotTooltip;
    public static Slot SlotPrefab;
    public static Transform CanvasForDraggingItem;

    private void Awake()
    {
        SlotTooltip = tooltip;
        SlotPrefab = slotPrefab;
        CanvasForDraggingItem = canvasForDraggingItem;
    }
}
