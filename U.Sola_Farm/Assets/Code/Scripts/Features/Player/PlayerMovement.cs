using UnityEngine;

public class PlayerMovement
{
    private PlayerModel _model;
    private Transform _transform;

    public PlayerMovement(PlayerModel model, Transform transform)
    {
        _model = model;
        _transform = transform;
    }

    public void Move(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, 0, input.y);
        _transform.position += direction * _model.MoveSpeed * Time.deltaTime;
    }
}
