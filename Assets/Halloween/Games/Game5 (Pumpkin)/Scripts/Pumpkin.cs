using DG.Tweening;
using UnityEngine;

namespace Game_5
{
    public class Pumpkin : MonoBehaviour
    {
        public Rigidbody2D rb;
        public CapsuleCollider2D collider2d;
        public SpriteRenderer sr;

        public GameObject cutAnim, landAnim;
        public GameObject hand;

        private bool box, cutter, ghost, land;

        // Start is called before the first frame update
        void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(0.47f, 0.5f);
        }

        public void OnMouseDown()
        {
            if (Game5.instance.isGameOver)
                return;

            rb.gravityScale = 2f;
            rb.constraints = RigidbodyConstraints2D.None;
            StartCoroutine(Game5.instance.GeneratePumpkin(transform.parent));

            transform.SetParent(null);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            SoundManager.instance.PlaySound(0);
            SoundManager.instance.PlaySound(1);

            if (hand != null)
                hand.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.CompareTag("Box") && !box)
                {
                    //Destroy(gameObject, 0.4f);
                    Game5.instance.catchCandy += 2;
                    SoundManager.instance.PlaySound(4);
                    box = true;
                }
                else if (collision.gameObject.CompareTag("Cutter") && !cutter)
                {
                    CutAnimation();
                    SoundManager.instance.PlaySound(2);
                    Game5.instance.GameOver(1.5f);
                    cutter = true;
                }
                else if (collision.gameObject.CompareTag("Ghost") && !ghost)
                {
                    transform.DOShakePosition(0.5f, 0.2f, 10);
                    rb.simulated = false;
                    Game5.instance.GameOver(1);
                    ghost = true;
                }
                else if (collision.gameObject.CompareTag("Land") && !land)
                {
                    rb.freezeRotation = true;
                    SoundManager.instance.PlaySound(3);
                    land = true;

                    if (box)
                        Game5.instance.catchCandy--;
                    else
                        Invoke(nameof(LandAnimation), 0.3f);
                }
                else if (collision.gameObject.CompareTag("Finish"))
                {
                    Destroy(gameObject, 0.1f);
                    Game5.instance.GameWin();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.CompareTag("Box") && !box)
                {
                    //Destroy(gameObject, 0.4f);
                    Game5.instance.catchCandy += 2;
                    SoundManager.instance.PlaySound(4);
                    box = true;
                }
                else if (collision.gameObject.CompareTag("Cutter") && !cutter)
                {
                    CutAnimation();
                    SoundManager.instance.PlaySound(2);
                    Game5.instance.GameOver(1.5f);
                    cutter = true;
                }
                else if (collision.gameObject.CompareTag("Ghost") && !ghost)
                {
                    transform.DOShakePosition(0.5f, 0.2f, 10);
                    rb.simulated = false;
                    Game5.instance.GameOver(1);
                    ghost = true;
                }
                else if (collision.gameObject.CompareTag("Land") && !land)
                {
                    rb.freezeRotation = true;
                    SoundManager.instance.PlaySound(3);
                    land = true;

                    if (box)
                        Game5.instance.catchCandy--;
                    else
                        Invoke(nameof(LandAnimation), 0.3f);
                }
                else if (collision.gameObject.CompareTag("Finish"))
                {
                    Destroy(gameObject, 0.1f);
                    Game5.instance.GameWin();
                }
            }
        }

        private void CutAnimation()
        {
            collider2d.enabled = false;
            sr.enabled = false;
            cutAnim.SetActive(true);
            rb.gravityScale = 0f;

            Destroy(gameObject, 1f);
        }

        private void LandAnimation()
        {
            //collider2d.enabled = false;
            sr.enabled = false;
            landAnim.SetActive(true);

            Destroy(gameObject, 1f);
        }
    }
}