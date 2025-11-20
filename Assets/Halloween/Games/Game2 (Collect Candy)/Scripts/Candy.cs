using UnityEngine;
using UnityEngine.UI;

public class Candy : MonoBehaviour
{
    public Sprite[] candyImgs;
    public Image candyImg;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        candyImg.sprite = candyImgs[Random.Range(0, candyImgs.Length)];
    }

    public void RandomGravity()
    {
        rb.gravityScale = Random.Range(1.4f, 2.7f);
    }

    public void Collect()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Bottom"))
        {
            SoundManager.instance.PlaySound(1);
            Destroy(gameObject);
        }
    }
}