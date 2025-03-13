using UnityEngine;
using DG.Tweening;

public class DialogueScrollBar : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _goalBottomY = 40f;
    [SerializeField] private float _duration = 0.5f;
    private bool _isScrolling = false;

    private void Update()
    {
        if (_isScrolling == true)
        {
            OnScroll();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isScrolling = true;
        }
    }

    private void OnScroll()
    {
        float adjustedRectY = _rectTransform.sizeDelta.y - (_goalBottomY);

        if (_rectTransform.anchoredPosition.y != adjustedRectY)
        {
            _rectTransform.DOAnchorPosY(adjustedRectY, _duration).SetEase(Ease.OutQuad);
            _isScrolling = false;
        }
    }
}
