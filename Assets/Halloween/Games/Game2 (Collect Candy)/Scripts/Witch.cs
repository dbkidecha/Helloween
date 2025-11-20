using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField] private GameObject tapEffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Candy"))
        {
            TapEffect();
            Destroy(collision.transform.parent.gameObject);
            Game2.instance.catchCandy++;
            SoundManager.instance.PlaySound(0);
        }
        else if (collision.gameObject.name.Equals("Finish"))
        {
            Game2.instance.ShowGameOver(true);
        }
    }

    private void TapEffect()
    {
        _ = Instantiate(tapEffect, transform.position, Quaternion.identity, null);
    }
}