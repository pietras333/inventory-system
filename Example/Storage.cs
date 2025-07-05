using System;
using UnityEngine;

public class Storage : ItemContainer
{
    [SerializeField] private ItemDefinition TestItem;
    [SerializeField] private ItemDefinition TestItem2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) TryAddItem(TestItem, 1);
        if (Input.GetKeyDown(KeyCode.Y)) TryAddItem(TestItem2, 1);
    }
}
