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

    public void Move(Vector2 direction, float strength)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            Vector3 moveDir = new Vector3(direction.x, 0, direction.y);
            moveDir.Normalize();
            float speed = _model.MoveSpeed * Mathf.Clamp01(strength);
            _transform.position += moveDir * speed * Time.deltaTime;
        }
    }
}
