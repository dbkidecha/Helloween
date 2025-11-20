using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Round1 : MonoBehaviour
{
    public static Round1 instance;

    public Transform bowl;
    public GameObject round1Hand;
    public GameObject[] targets;
    public GameObject[] inBowlObj;

    public Canvas coverBowl;
    public GameObject mixerObj;
    public Transform uperMixer;
    public Animator mixingAnim;
    public Animator blendar;
    public Sprite mixerBlade;
    public GameObject bowlHand;

    public GameObject powerBtn;

    private void Awake()
    {
        instance = this;
    }

    public void PowerBtn()
    {
        powerBtn.SetActive(false);

        SoundManager.instance.PlaySound(1);
        coverBowl.enabled = true;
        uperMixer.DORotate(Vector3.zero, 0.5f).OnComplete(() =>
        {
            mixingAnim.gameObject.SetActive(true);
        });
        blendar.enabled = true;

        Invoke(nameof(StopBlendar), 3.5f);
    }

    private void StopBlendar()
    {
        blendar.enabled = false;
        mixingAnim.enabled = false;
        blendar.GetComponent<Image>().sprite = mixerBlade;

        for (int i = 0; i < 4; i++)
        {
            inBowlObj[i].gameObject.SetActive(false);
        }
        uperMixer.DORotate(new Vector3(0f, 0f, -30f), 0.5f).OnComplete(() =>
        {
            coverBowl.enabled = false;
            bowl.DOLocalMove(new Vector3(0f, -60f, 0f), 0.5f);
            SoundManager.instance.PlaySound(3);
            mixerObj.transform.DOLocalMoveX(1500f, 1.5f).SetEase(Ease.InFlash).OnComplete(() =>
            {
                ShowParticle();
            });
        });
    }

    private void ShowParticle()
    {
        StartCoroutine(Game6.instance.ShowParticle(0));
    }

    public void ShowMixer()
    {
        bowl.DOLocalMoveX(-430f, 0.8f);
        SoundManager.instance.PlaySound(3);
        mixerObj.transform.DOLocalMoveX(144f, 1.5f).OnComplete(() =>
        {
            bowlHand.SetActive(true);
        });
    }

    public void ShowPowerBtn()
    {
        powerBtn.SetActive(true);
    }
}