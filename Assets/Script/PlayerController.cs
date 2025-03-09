using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Rigidbody _rb;
    private Vector3 _moveDirection = Vector3.zero;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.linearVelocity = _moveDirection * _speed;
    }

    public void MoveRight()
    {
        _moveDirection = new Vector3(1, 0, 0);
    }

    public void MoveLeft()
    {
        _moveDirection = new Vector3(-1, 0, 0);
    }

    public void StopMoving()
    {
        _moveDirection = Vector3.zero;
    }
}
