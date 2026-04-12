using System;
using UnityEngine;
using Zenject;

public class BackPackHandler : MonoBehaviour
{
    IInventoryService backPack;

    [Inject]

    public void Initialized(IInventoryService _backPack)
    {
        this.backPack = _backPack;

    }

    public IInventoryService AccessBackPack() => backPack;
    public BackPack AccessBackPackAsClass() 
    {
        var copy = backPack as BackPack;
        return copy;
    }
    public ResourceWrapper GetBackPackData() => backPack.GetResources();
    public bool IsBackPackFull() 
    {
        var copy = backPack as BackPack;
        return copy.IsBackPackFull;
    } 
    public bool AddResource(ResourceModel resource) => backPack.AddResource(resource);
    public int GetCountResource(string _name) => backPack.GetResourceCount(_name);

    public void RemoveResource(ResourceModel resource) => backPack.RemoveResource(resource);
}
