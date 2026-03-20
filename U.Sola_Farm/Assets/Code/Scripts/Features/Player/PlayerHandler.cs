using UnityEngine;
using Zenject;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private PlayerModel _model;
    
    private IInputService _inputService;
    private PlayerMovement _movement;


    [Inject]
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
