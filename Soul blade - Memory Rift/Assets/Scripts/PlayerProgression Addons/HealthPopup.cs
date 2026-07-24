using UnityEngine;
using TMPro;

public class HealthPopup : MonoBehaviour
{
    public TMP_Text popupText;
    public float lifeTime = 1.5f;
    public float speed = 2f;

    private float timer;
    private Color color;

    public void Setup(int amount)
    {
        popupText.text = Mathf.Abs(amount).ToString();

        if(amount < 0)
        {
            color = Color.red;
        }
        else
        {
            color = Color.green;
        }

        popupText.color = color;
    }

    private void Update()
    {
        // Move Upward
        transform.position += Vector3.up * (speed * Time.deltaTime);

        //Timer + fade
        timer += Time.deltaTime;

        float alpha = 1f - (timer / lifeTime);

        popupText.color = new Color(color.r, color.g, color.b, alpha);

        if (timer >= lifeTime) 
        {
            Destroy(gameObject);
        }
    }
}