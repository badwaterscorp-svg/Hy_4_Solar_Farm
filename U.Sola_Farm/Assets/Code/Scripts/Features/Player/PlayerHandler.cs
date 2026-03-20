using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private PlayerModel _model;
    
    private IInputService _inputService;
    private PlayerMovement _movement;

    public void Initialize(IInputService inputService)
    {
        _inputService = inputService;
        _movement = new PlayerMovement(_model, transform);
    }

    private void Update()
    {
        _movement.Move(_inputService.MoveInput);
    }
}
