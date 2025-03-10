using EPOOutline;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private GameObject _outlineTarget;
    private InputSystem_Actions _input;
    private NavMeshAgent _agent;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isMoving = false;
    private bool _isAutoMoving = false;
    private void Awake()
    {
        _input = new InputSystem_Actions();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.ClickLeftButton.performed += ctx => OnClickInteractNPC();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        // 플레이어가 직접 조작할 때 이동 처리
        if (_isMoving == true)
        {
            Vector3 targetPosition = transform.position + _moveDirection * _speed * Time.deltaTime;
            _agent.SetDestination(targetPosition);
        }

        // NPC 클릭후 자동 이동
        if (_isAutoMoving == true && _agent.pathPending == false && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _isAutoMoving = false;
            _agent.ResetPath(); // 목표 도착 후 이동 종료
        }

        // 아웃라인 처리
        OutlineMethod();
    }

    private void OnClickInteractNPC()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                Vector3 InteractionPosition = hit.collider.GetComponent<NPC_Information>().DestinationVector3;

                _isMoving = false;
                _isAutoMoving = true;
                _agent.SetDestination(InteractionPosition);
            }
        }
    }

    private void OutlineMethod()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                _outlineTarget = hit.collider.gameObject;
                CanSeeOutline(_outlineTarget);
            }
            else if (_outlineTarget != null)
            {
                CannotSeeOutline(_outlineTarget);
                _outlineTarget = null;
            }
        }
        else
        {
            if (_outlineTarget != null)
            {
                CannotSeeOutline(_outlineTarget);
                _outlineTarget = null;
            }
        }
    }

    public void MoveRight()
    {
        _moveDirection = Vector3.right;
        _isMoving = true;
        _isAutoMoving = false;
    }

    public void MoveLeft()
    {
        _moveDirection = Vector3.left;
        _isMoving = true;
        _isAutoMoving = false;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    public void CanSeeOutline(GameObject targetObject)
    {
        Outlinable outlinable = targetObject.GetComponent<Outlinable>();
        outlinable.OutlineLayer = 1;
    }

    public void CannotSeeOutline(GameObject targetObject)
    {
        Outlinable outlinable = targetObject.GetComponent<Outlinable>();
        outlinable.OutlineLayer = 0;
    }
}
