using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Item item;
    [SerializeField] public int count = 1;
    static Slot draggingSlot;
    internal int id;
    internal Image slotIcon;
    TMP_Text slotCount;
    Image eventTarget;
    Button btn;
    Transform childrenSlot;
    Transform bodySlot;
    Transform parent;
    GridLayoutGroup gridLayoutGroup;
    RectTransform rectTransform;
    private void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnClick()
    {
    }

    private void DeleteSlot()
    {
        item = null;
        slotIcon.sprite = null;
        slotCount.text = null;
    }
    internal void LoadComponent()
    {
        eventTarget = transform.GetComponent<Image>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        bodySlot = transform.GetChild(0);
        childrenSlot = bodySlot.GetChild(0);
        slotIcon = childrenSlot.GetChild(0).GetComponent<Image>();
        slotCount = childrenSlot.GetChild(1).GetComponent<TMP_Text>();
        if (item != null)
            UpdateSlot();
    }
    internal void UpdateSlot(Item newItem)
    {
        item = newItem;
        UpdateSlot();
    }

    private void SetCount()
    {
        if (item?.IsStackable ?? false)
            slotCount.text = count.ToString();
        else
            slotCount.text = "";
    }

    internal void UpdateSlot()
    {
        SetIcon();
        SetCount();
    }
    private void SetIcon() => slotIcon.sprite = item?.Icon ?? null;

    public void OnDrag(PointerEventData eventData)
    {
        if (item == null) return;
        bodySlot.position = Input.mousePosition;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null) return;
        draggingSlot = this;
        parent = bodySlot.parent;
        bodySlot.SetParent(StorageSetting.CanvasForDraggingItem);
        eventTarget.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item == null) return;
        Slot drop = eventData.pointerEnter?.GetComponent<Slot>() ?? null;
        if (drop != null)
        {
            ActionWithDraggingSlot(drop);
        }
        StopDragging();
    }

    private void ActionWithDraggingSlot(Slot drop)
    {
        DraggingInStorage(drop);
    }

    private void DraggingInStorage(Slot drop)
    {
        int index = transform.GetSiblingIndex();
        transform.SetSiblingIndex(drop.transform.GetSiblingIndex());
        drop.transform.SetSiblingIndex(index);
    }

    internal static void StopDragging()
    {
        try
        {
            draggingSlot.bodySlot.SetParent(draggingSlot.parent);
            draggingSlot.eventTarget.raycastTarget = true;
        }
        catch (Exception)
        {
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;
        StorageSetting.SlotTooltip.ShowTooltip(item.Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StorageSetting.SlotTooltip.HideTooltip();
    }
}
