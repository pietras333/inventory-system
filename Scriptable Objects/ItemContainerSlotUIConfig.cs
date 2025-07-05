using UnityEngine;

[CreateAssetMenu(fileName = "Item Container Slot UI Config",
    menuName = "Inventory System/Item Container Slot UI Config")]
public class ItemContainerSlotUIConfig : ScriptableObject
{
    [SerializeField] private Sprite passiveSlotIcon;
    public Sprite PassiveSlotIcon => passiveSlotIcon;
    [SerializeField] private Sprite activeSlotIcon;
    public Sprite ActiveSlotIcon => activeSlotIcon;
}
