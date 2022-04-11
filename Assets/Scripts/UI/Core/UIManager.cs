using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] UIScreen[] screens;
    [SerializeField] Popup[] popups;
    [SerializeField, Space(10)] ScreenType _defaultScreen;
    UIScreen _activeScreen;
    public UIScreen ActiveScreen => _activeScreen;
    Popup _activePopup;

    private void Start()
    {
        Input.multiTouchEnabled = false;
        Init();
    }
    void Init()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].Disable();
        }
        for (int i = 0; i < popups.Length; i++)
        {
            popups[i].Disable();
        }

        if (_defaultScreen != ScreenType.None)
            ChangeScreen(_defaultScreen);
    }

    public static UIScreen GetScreen(ScreenType screenType)
    {
        for (int i = 0; i < _instance.screens.Length; i++)
        {
            if (_instance.screens[i].screenType.Equals(screenType)) return _instance.screens[i];
        }
        return null;
    }

    public static Popup GetPopup(PopupType popupType)
    {
        for (int i = 0; i < _instance.popups.Length; i++)
        {
            if (_instance.popups[i].popupType.Equals(popupType)) return _instance.popups[i];
        }
        return null;
    }

    public static void ChangeScreen(ScreenType toScreenType)
    {
        _instance._activeScreen?.Hide();
        UIScreen toScreen = GetScreen(toScreenType);

        if (toScreen)
            _instance._activeScreen = toScreen;

        _instance._activeScreen?.Show();
    }

    public static void ShowPopup(PopupType popupType)
    {
        _instance._activePopup?.Hide();
        if (popupType == PopupType.None) return;

        Popup popupToOpen = GetPopup(popupType);
        if (popupToOpen) _instance._activePopup = popupToOpen;

        _instance._activePopup.Show();
    }
    public static void ShowOverlayPopup(PopupType popupType)
    {
        if (popupType == PopupType.None) return;

        Popup popupToOpen = GetPopup(popupType);
        if (popupToOpen) _instance._activePopup = popupToOpen;

        _instance._activePopup.Show();
    }

}
