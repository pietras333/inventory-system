using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour
{
    [SerializeField] public ItemContainerConfig Config;

    [Header("Configuration")] [SerializeField]
    public List<ItemContainerSlotUI> Slots;

    public void Start()
    {
        InitializeSlots();
    }

    public void InitializeSlots()
    {
        foreach (var slot in Slots) slot.Initialize(this);
    }

    public bool TryAddItem(ItemDefinition itemDefinition, int amount)
    {
        if (TryAddItemToStack(itemDefinition, amount))
            return true;
        if (TryAddItemToEmptySlot(itemDefinition, amount))
            return true;
        return false;
    }

    public bool TryAddItemToEmptySlot(ItemDefinition itemDefinition, int amount)
    {
        if (!HasEmptySlot(out var slotIndex)) return false;

        Slots[slotIndex].SetHeldItem(new ItemContainerItemUI(itemDefinition, amount, Slots[slotIndex]));
        return true;
    }

    public bool TryAddItemToStack(ItemDefinition itemDefinition, int amount)
    {
        if (!HasAvailableStack(itemDefinition, amount, out var slotIndex))
            return false;

        var heldItem = Slots[slotIndex].GetHeldItem();
        Slots[slotIndex].SetAmount(heldItem.Amount + amount);
        return true;
    }

    public bool HasAvailableStack(ItemDefinition itemDefinition, int amount, out int slotIndex)
    {
        slotIndex = -1;
        foreach (var slot in Slots)
        {
            if (slot.IsEmpty) continue;
            if (!slot.CanMerge(new ItemContainerItemUI(itemDefinition, amount, slot)))
                continue;
            slotIndex = Slots.IndexOf(slot);
            return true;
        }

        return false;
    }

    public bool HasEmptySlot(out int slotIndex)
    {
        slotIndex = -1;
        foreach (var slot in Slots)
            if (slot.IsEmpty)
            {
                slotIndex = Slots.IndexOf(slot);
                return true;
            }

        return false;
    }
}
