using UnityEngine;

public class PlayerMovement
{
    private PlayerModel _model;
    private Transform _transform;
    private Rigidbody _rb;

    public PlayerMovement(PlayerModel model, Transform transform, Rigidbody rigidbody)
    {
        _model = model;
        _transform = transform;
        _rb = rigidbody;
    }

    public void Move(Vector2 direction, float strength)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            Vector3 moveDir = new Vector3(direction.x, 0, direction.y);
            moveDir.Normalize();
            float speed = _model.MoveSpeed * Mathf.Clamp01(strength);
            Vector3 newPosition = _rb.position + moveDir * speed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        }
    }
}
