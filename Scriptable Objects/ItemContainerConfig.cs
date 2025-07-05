using UnityEngine;

[CreateAssetMenu(fileName = "Item Container Config", menuName = "Inventory System/Item Container Config")]
public class ItemContainerConfig : ScriptableObject
{
    [SerializeField] private bool isNetworkSynced = false;
    public bool IsNetworkSynced => isNetworkSynced;
}
