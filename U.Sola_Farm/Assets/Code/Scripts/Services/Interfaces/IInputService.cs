using UnityEngine;
using System;

public interface IInputService
{
    Vector2 PointPosition { get; }
    Vector2 DragStartPosition { get; }
    bool IsDragging { get; }
    event Action OnDragStarted;
    event Action OnDragStoped;
    void Enable();
    void Disable();
}
