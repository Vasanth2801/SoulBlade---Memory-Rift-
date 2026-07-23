using UnityEngine;
using UnityEngine.InputSystem;

public class MiniMapToggle : MonoBehaviour
{
    [SerializeField] private GameObject miniMap;
    [SerializeField] private InputActionReference menuToogle;

    private void Awake()
    {
        miniMap.SetActive(false);
    }

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
        miniMap.SetActive(!miniMap.activeSelf);
    }
}
