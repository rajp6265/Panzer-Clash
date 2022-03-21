using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private Canvas _canvas;
    private bool _isActive;
    public bool IsActive => _isActive;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();

        // Show();
    }

    public virtual void Enable()
    {
        _isActive = _canvas.enabled = true;
    }

    public virtual void Disable()
    {
        _isActive = _canvas.enabled = false;
    }

    public void Show()
    {
        if (_isActive) return;
        Enable();
    }
    public void Hide()
    {

        Disable();
    }
    private void OnDestroy()
    {
        _canvas = null;
    }
}
