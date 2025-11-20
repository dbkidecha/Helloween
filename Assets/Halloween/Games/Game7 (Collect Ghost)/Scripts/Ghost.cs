using UnityEngine;
using DG.Tweening;

public class Ghost : MonoBehaviour
{
    public int inx;
    public Rigidbody2D rb;
    public CircleCollider2D cc;

    private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("GhostCatch") && !collected)
        {
            DOTween.Kill(gameObject.name);

            rb.gravityScale = 0.5f;
            transform.DOScale(0.4f, 0.2f);
            transform.SetParent(collision.transform);
            cc.isTrigger = false;
            rb.mass = 0f;
            collected = true;

            Game7.instance.ghostCollected++;
            //SoundManager.instance.PlaySound(1);
            SoundManager.instance.PlaySound(inx);
        }
    }
}