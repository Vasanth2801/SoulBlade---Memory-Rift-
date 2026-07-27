using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputActionReference menuToogle;
    [SerializeField] private MenuPanel defaultPanel;
    [SerializeField] private float fadeDuration;
    private MenuPanel currentMenu;
    private MenuPanel lastOpenedMenu;

    private void OnEnable()
    {
        menuToogle.action.performed += OnCancel;
        menuToogle.action.Enable();
    }

    private void OnDisable()
    {
        menuToogle.action.performed -= OnCancel;
        menuToogle.action.Disable();
    }


    private void OnCancel(InputAction.CallbackContext context)
    {
        if(currentMenu != null)
        {
            CloseAll();
        }
        else
        {
            MenuPanel menuToOpen = lastOpenedMenu != null ? lastOpenedMenu : defaultPanel;
            StartCoroutine(SwitchRoutine(menuToOpen));
        }
    }

    public void ToggleMenu(MenuPanel panel)
    {
        if(currentMenu == panel)
        {
            StartCoroutine(SwitchRoutine(null));
            return;
        }

        StartCoroutine(SwitchRoutine(panel));
    }

    private void ApplyTimeState(MenuPanel panel)
    {
        if(panel != null && panel.pausedGame)
        {
            Time.timeScale = 0f;    
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public  void CloseAll()
    {
        StartCoroutine(SwitchRoutine(null));
    }

    IEnumerator SwitchRoutine(MenuPanel panel)
    {
        // Close old Menu
        if (currentMenu != null)
        {
            yield return Fade(currentMenu.canvasGroup, currentMenu.canvasGroup.alpha, 0);
            currentMenu.canvasGroup.interactable = false;
            currentMenu.canvasGroup.blocksRaycasts = false;
            currentMenu.Close();
        }

        //Open New Menu
        currentMenu = panel;

        if (currentMenu != null)
        {
            currentMenu.Open();
            lastOpenedMenu = currentMenu;   
            CanvasGroup newGroup = currentMenu.canvasGroup;
            newGroup.interactable = true;
            newGroup.blocksRaycasts = true;
            yield return Fade(newGroup, newGroup.alpha, 1);
        }
        ApplyTimeState(currentMenu);
    }

    IEnumerator Fade(CanvasGroup canvasGroup, float from, float to)
    {
        float time = 0;
        while(time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, time/fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}