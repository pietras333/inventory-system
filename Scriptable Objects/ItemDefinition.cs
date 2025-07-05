using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Item")]
public class ItemDefinition : ScriptableObject
{
    [SerializeField] private string id;
    public string ID => id;
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    [SerializeField] private string itemDescription;
    public string ItemDescription => itemDescription;
    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;
    [SerializeField] private int maxStackSize;
    public int MaxStackSize => maxStackSize;
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
}
