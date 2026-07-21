using TMPro;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private Player player;
    [SerializeField] private CollectableSO collectableSO;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text itemMessage;

    public void Initialize(CollectableSO collectableSO)
    {
        this.collectableSO = collectableSO;
        sr.sprite = collectableSO.itemSprite; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if(player == null)
        {
            return;
        }

        CollectItem();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void CollectItem()
    {
        itemMessage.text = "Found " + collectableSO.name;
        anim.Play("Loot");
        collectableSO.Collect(player);
        Destroy(gameObject, 1);
    }
}