using System;
using UnityEngine;

public class BackPackHandler : MonoBehaviour
{
    BackPack backPack;

    void Awake()
    {
        if (backPack == null)
            backPack = new BackPack();
        backPack.LoadInventory();
    }

    public BackPack AccessBackPack()
    {
        if(backPack == null)
            backPack = new BackPack();
        return backPack;
    }
    public bool IsBackPackFull() => backPack.IsBackPackFull;
    public bool AddResource(ResourceModel resource) => backPack.AddResource(resource);

    public int GetCountResource(string _name) => backPack.GetResourceCount(_name);

    public void RemoveResource(ResourceModel resource) => backPack.RemoveResource(resource);
}
