using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("GhostCatch"))
        {
            transform.SetParent(collision.transform);
            SoundManager.instance.PlaySound(2);
            Invoke(nameof(BurnNet), 0.1f);
        }
    }

    private void BurnNet()
    {
        Game7.instance.BurnNet();
        Destroy(gameObject);
    }
}