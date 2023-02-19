using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _movementSpeed = 5f;

    private bool _isFacingRight = true;

    private void Update()
    {
        if(GameState.Instance.CurrGameState != GameStates.InComputer)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput != 0)
            {
                Vector2 movement = new Vector2(horizontalInput * _movementSpeed, _rigidbody.velocity.y);
                _rigidbody.velocity = movement;

                if ((_isFacingRight && horizontalInput < 0) || (!_isFacingRight && horizontalInput > 0))
                {
                    Flip();
                }
            }
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
