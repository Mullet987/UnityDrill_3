using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Singleton;
    public GameObject itemPrefab;

    [SerializeField] private Transform _draggablesTransform;
    [SerializeField] private GraphicRaycaster _raycaster;

    private InputSystem_Actions _inputActions;
    private Transform _parentTransform;
    private Transform _draggingItem;
    private Vector2 _lastPointerPosition;
    private bool _isDragging = false;

    private void Awake()
    {
        Singleton = this;
        _inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        _inputActions.Drag.Enable();
        _inputActions.Drag.Drag.performed += OnDragStart;
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
        InventorySlot slot = GetSlot();
        if (slot == null || slot._carriedItem == null)
        {
            //드래그할 수 있는 슬롯이 아님
            return;
        }

        SetCarriedItem(slot);

        _isDragging = true;
        _lastPointerPosition = Pointer.current.position.ReadValue();

        //Dragging과 DragEnd 이벤트 등록
        _inputActions.Drag.Dragging.performed += OnDrag;
        _inputActions.Drag.Drag.canceled += OnDragEnd;
    }

    private void OnDrag(InputAction.CallbackContext context)
    {
        if (!_isDragging) return;

        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        Vector2 delta = pointerPosition - _lastPointerPosition;

        _lastPointerPosition = pointerPosition;
        _draggingItem.position = _lastPointerPosition;
    }

    private void OnDragEnd(InputAction.CallbackContext context)
    {
        InventorySlot slot = GetSlot();

        if (slot != null)
        {
            var sourceSlot = _parentTransform.GetComponent<InventorySlot>();

            if (slot._carriedItem != null)
            {
                Item temp1 = sourceSlot._carriedItem;
                Item temp2 = slot._carriedItem;

                // 서로 위치 교환
                _draggingItem.SetParent(slot.transform);
                _draggingItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
                slot.transform.GetChild(0).SetParent(_parentTransform);

                slot._carriedItem = temp1;
                sourceSlot._carriedItem = temp2;
            }
            else
            {
                _draggingItem.SetParent(slot.transform);
                _draggingItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
                slot._carriedItem = sourceSlot._carriedItem;
                sourceSlot._carriedItem = null;
            }
        }
        else
        {
            if (_isDragging)
            {
                Debug.Log("slot이 없네");
                // 원래 위치로 되돌리기
                _draggingItem.SetParent(_parentTransform);
                _draggingItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }

        // 상태 초기화
        _isDragging = false;
        _draggingItem = null;
        _parentTransform = null;
        _lastPointerPosition = Vector2.zero;

        // Drag, Dragging 이벤트 해제
        _inputActions.Drag.Dragging.performed -= OnDrag;
        _inputActions.Drag.Drag.canceled -= OnDragEnd;
    }

    private InventorySlot GetSlot()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Pointer.current.position.ReadValue()
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            InventorySlot slot = result.gameObject.GetComponent<InventorySlot>();
            if (slot != null)
                return slot;
        }

        return null;
    }

    public void SetCarriedItem(InventorySlot itemSlot)
    {
        if (itemSlot.transform.childCount != 0)
        {
            _parentTransform = itemSlot.transform;
            _draggingItem = itemSlot.transform.GetChild(0);
            _draggingItem.SetParent(_draggablesTransform);
            _draggingItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
