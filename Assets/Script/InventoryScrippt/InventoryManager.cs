using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Singleton;
    public GameObject itemPrefab;

    private InputSystem_Actions _inputActions;
    private bool _isDragging = false;
    private Vector2 _lastPointerPosition;

    [SerializeField] private Transform _draggablesTransform;

    private void Awake()
    {
        Singleton = this;
        _inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        _inputActions.Drag.Enable();
        _inputActions.Drag.Drag.performed += OnDragStart;
        _inputActions.Drag.Dragging.performed += OnDrag;
        _inputActions.Drag.Drag.canceled += OnDragEnd;
    }

    void OnDisable()
    {
        _inputActions.Drag.Drag.performed -= OnDragStart;
        _inputActions.Drag.Dragging.performed -= OnDrag;
        _inputActions.Drag.Drag.canceled -= OnDragEnd;
        _inputActions.Drag.Disable();
    }

    private void OnDragStart(InputAction.CallbackContext context)
    {
        _isDragging = true;
        _lastPointerPosition = Pointer.current.position.ReadValue();
        Debug.Log("드래그 시작!");
    }

    private void OnDragEnd(InputAction.CallbackContext context)
    {
        _isDragging = false;
        Debug.Log("드래그 종료!");
    }

    private void OnDrag(InputAction.CallbackContext context)
    {
        if (!_isDragging) return;

        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        Vector2 delta = pointerPosition - _lastPointerPosition;

        _lastPointerPosition = pointerPosition;
        Debug.Log("드래그 중: " + delta);
    }

    public void SetCarriedItem(InventorySlot itemSlot)
    {
        if (itemSlot.transform.childCount != 0)
        {
            Transform itemObject = itemSlot.transform.GetChild(0);
            itemObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            itemObject.transform.SetParent(_draggablesTransform);
        }
    }

}
