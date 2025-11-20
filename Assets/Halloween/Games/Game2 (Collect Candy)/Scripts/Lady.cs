using UnityEngine;

public class Lady : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle")|| collision.gameObject.name.Equals("Bottom"))
        {
            Game2.instance.ShowGameOver();
        }
    }
}