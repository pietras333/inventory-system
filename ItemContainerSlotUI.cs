using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemContainerSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ItemContainerSlotUIConfig Config;

    [Header("Configuration")] [SerializeField]
    private Image SlotRenderer;

    [SerializeField] private Image HeldItemRenderer;
    [SerializeField] private TextMeshProUGUI AmountText;

    private bool isActive;
    private ItemContainerItemUI HeldItem;
    public ItemContainer Container;
    public bool IsEmpty => HeldItem == null;

    public void Initialize(ItemContainer container)
    {
        Container = container;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            ItemDragManager.Instance.HandleLeftClick(this);
        else if (eventData.button == PointerEventData.InputButton.Right)
            ItemDragManager.Instance.HandleRightClick(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemDragManager.Instance.HandleHoverEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDragManager.Instance.HandleHoverExit(this);
    }

    public ItemContainerItemUI GetHeldItem()
    {
        return HeldItem;
    }

    public void SetHeldItem(ItemContainerItemUI item)
    {
        HeldItem = item;
        HeldItem.ItemContainerSlotUI = this;
        Refresh();
    }

    public bool TryToMerge(ItemContainerItemUI item)
    {
        if (!CanMerge(item)) return false;
        HeldItem.Amount += item.Amount;
        Refresh();
        return true;
    }

    public void SetAmount(int amount)
    {
        HeldItem.Amount = amount;
        Refresh();
    }

    public void Refresh()
    {
        HeldItemRenderer.sprite = HeldItem.ItemDefinition.Icon;
        AmountText.text = HeldItem.Amount.ToString();
    }

    public void Clear()
    {
        HeldItem = null;
        HeldItemRenderer.sprite = null;
        AmountText.text = "";
    }

    public bool IsSameItem(ItemContainerItemUI item)
    {
        return item.ItemDefinition.ID == HeldItem.ItemDefinition.ID;
    }

    public bool CanMerge(ItemContainerItemUI item)
    {
        var amount = HeldItem.Amount + item.Amount;
        if (amount > HeldItem.ItemDefinition.MaxStackSize) return false;
        if (!IsSameItem(item)) return false;
        return true;
    }

    public void SetActive(bool active)
    {
        isActive = active;
        SlotRenderer.sprite = active ? Config.ActiveSlotIcon : Config.PassiveSlotIcon;
    }
}
