using UnityEngine;
using UnityEngine.UI;

namespace Game_4
{
    public class Candy : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite[] candys;

        // Start is called before the first frame update
        void Start()
        {
            image.sprite = candys[Random.Range(0, candys.Length)];
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name.StartsWith("InBucket"))
            {
                Game4.instance.TapEffect(collision.transform.position);
                Destroy(gameObject);
                SoundManager.instance.PlaySound(5);

                Game4.instance.catchCandy++;
            }
        }
    }
}