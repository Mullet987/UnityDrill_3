using UnityEngine;
using UnityEngine.UI;

public class ColliderEvent : MonoBehaviour
{
    [SerializeField] private RectTransform _uiElement; // UI ��ư (RectTransform)
    [SerializeField] private Vector3 _offset = new Vector3(0, 3, 0); // UI ��ġ ����

    private Transform _targetObject; // UI�� ǥ���� ��� (��: ��, ������)
    private bool _isEntered = true;

    private void Awake()
    {
        _targetObject = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_isEntered == true)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(_targetObject.position + _offset);
            _uiElement.position = screenPosition;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isEntered = true;
            _uiElement.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isEntered = false;
            _uiElement.gameObject.SetActive(false);
        }
    }
}
