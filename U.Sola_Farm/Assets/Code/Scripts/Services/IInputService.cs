using UnityEngine;

public interface IInputService
{
    Vector2 MoveInput { get; }
    void Enable();
    void Disable();
}
