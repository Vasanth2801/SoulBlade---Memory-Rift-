using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private float spawnDelay = 0.2f;
    [SerializeField] private float launchForce = 4f;

    private PlayerInput playerInput;
    private bool isOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerInput>(out var input))
        {
            playerInput = input;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out var input))
        {
            if (input == playerInput)
            {
                playerInput = null;
            }
        }
    }

    private void Update()
    {
        if(isOpened || playerInput == null)
        {
            return;
        }

        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        if(moveInput.y > 0.1f)
        {
            StartCoroutine(OpenChestRoutine());
        }

        if (playerInput.actions["Interact"].WasPressedThisFrame())
        {
            StartCoroutine(OpenChestRoutine());
        }
    }

    private IEnumerator OpenChestRoutine()
    {
        isOpened = true;
        anim.Play("Chest");

        yield return new WaitForSeconds(spawnDelay);

        Loot newLoot = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
        newLoot.Initialize(collectableSO);
    }
}