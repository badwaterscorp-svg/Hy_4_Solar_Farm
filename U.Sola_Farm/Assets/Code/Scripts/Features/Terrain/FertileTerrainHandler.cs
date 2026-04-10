using DG.Tweening;
using UnityEngine;

public class FertileTerrainHandler : MonoBehaviour
{
    [SerializeField] private Vector2 _expandSize = new Vector2(1f, 1f);
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private bool startExpanded = false;

    private Vector3 _originalScale;
    private bool _isExpanded;

    private void Awake()
    {
        _originalScale = transform.localScale;
        if(startExpanded)
            Expand();
    }

    [ContextMenu("Expand")]
    public void Expand()
    {
        if (_isExpanded) return;
        
        Vector3 targetScale = new Vector3(
            _originalScale.x + _expandSize.x,
            _originalScale.y + _expandSize.y,
            _originalScale.z
        );
        
        transform.DOScale(targetScale, _duration).SetEase(Ease.Linear);
        _isExpanded = true;
    }

    [ContextMenu("Reduce")]
    public void Reduce()
    {
        if (!_isExpanded) return;
        
        transform.DOScale(_originalScale, _duration).SetEase(Ease.InBack);
        _isExpanded = false;
    }

    public void ToggleExpand()
    {
        if (_isExpanded)
            Reduce();
        else
            Expand();
    }
}
