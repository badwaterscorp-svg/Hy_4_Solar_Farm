using System;
using UnityEngine;
using UnityEngine.Pool;

public interface IResourcePoolService
{
    ResourceHandler Get();
    void Release(ResourceHandler handler);
    int CountActive { get; }
    int CountInactive { get; }
}