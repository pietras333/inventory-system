using UnityEngine;

public class ItemContainerItemUI
{
    public ItemContainerSlotUI ItemContainerSlotUI;
    public ItemDefinition ItemDefinition;
    public int Amount;

    public ItemContainerItemUI(ItemDefinition itemDefinition, int amount, ItemContainerSlotUI itemContainerSlotUI)
    {
        ItemDefinition = itemDefinition;
        Amount = amount;
        ItemContainerSlotUI = itemContainerSlotUI;
    }

    public ItemContainerItemUI Clone()
    {
        return new ItemContainerItemUI(
            ItemDefinition, // Safe to reuse: assumed immutable or shared reference
            Amount, // Copy current amount
            null // New clone is not assigned to any slot yet
        );
    }
}
