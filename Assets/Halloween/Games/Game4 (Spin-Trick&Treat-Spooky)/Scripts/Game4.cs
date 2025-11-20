using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game4 : MonoBehaviour
{
    public static Game4 instance;

    public Canvas canvas;
    public GameObject tapParticle;
    public FortuneWheel wheel;

    [Header("Roulette")]
    public GameObject treatGame;
    public GameOptions[] trickGames;
    public GameObject spooky;
    public GameObject[] spookyGames;

    [Space(5)]
    public int spookyIndex = -1;
    public int trickIndex = -1;
    public GameObject masterConfetti;

    public static List<int> spookyNo = new List<int>() { 0, 1, 2, 3 };
    public static List<int> trickGameNo = new List<int>() { 0, 1, 2, 3, 4, 5 };

    private int _doneSteps;
    public int doneSteps
    {
        get { return _doneSteps; }
        set
        {
            _doneSteps = value;
            if (trickIndex.Equals(0) || trickIndex.Equals(3))
            {
                if (value >= 4)
                {
                    StartCoroutine(ResetTrickGame());
                }
            }
            else if (trickIndex.Equals(1) || trickIndex.Equals(4))
            {
                if (value >= 6)
                {
                    StartCoroutine(ResetTrickGame());
                }
            }
            else if (trickIndex.Equals(2) || trickIndex.Equals(5))
            {
                if (value >= 3)
                {
                    StartCoroutine(ResetTrickGame());
                }
            }
        }
    }

    public Image bucket;
    public Sprite[] bucketImgs;
    private int _catchCandy;
    public int catchCandy
    {
        get { return _catchCandy; }
        set
        {
            _catchCandy = value;
            if (value >= 9)
                bucket.sprite = bucketImgs[2];
            else if (value >= 7)
                bucket.sprite = bucketImgs[1];
            else if (value >= 5)
                bucket.sprite = bucketImgs[0];
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GlobalScript.instance.CloseLoading();
        //SoundManager.instance.PlaySound(0);

        AdsManager.instance.ChangeBannerView(GoogleMobileAds.Api.AdPosition.Top);
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
        SoundManager.instance.PlaySound(8);
    }

    public void TapEffect(Vector2 pos)
    {
        _ = Instantiate(tapParticle, pos, Quaternion.identity, null);
    }

    public void ActiveSpin()
    {
        if (!wheel.allDone)
        {
            doneSteps = 0;
            wheel.spinBtn.interactable = true;
            wheel.transform.GetChild(0).DOScale(1f, 0.5f);

            wheel.ShowHand();
        }
        else
        {
            GlobalScript.instance.LoadScreen(1);
        }
    }

    public IEnumerator ShowTreatGame()
    {
        catchCandy = 0;
        bucket.sprite = bucketImgs[3];

        yield return new WaitForSeconds(2f);
        wheel.transform.GetChild(0).DOScale(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        treatGame.SetActive(true);
    }

    public IEnumerator DisableTreatGame()
    {
        yield return new WaitForSeconds(1f);
        ShowConfetti();
        yield return new WaitForSeconds(3f);

        treatGame.SetActive(false);
        ActiveSpin();
    }

    private void ShowConfetti()
    {
        masterConfetti.SetActive(true);
        Invoke(nameof(StopConfetti), 2f);

        AdsManager.instance?.ShowInterstitialAd();
    }

    private void StopConfetti()
    {
        masterConfetti.SetActive(false);
    }

    public void DisableGame()
    {
        trickGames[trickIndex].gameObject.SetActive(false);
    }

    private IEnumerator ResetTrickGame()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < trickGames[trickIndex].options.Length; i++)
        {
            SoundManager.instance.PlaySound(9);
            trickGames[trickIndex].options[i].transform.DOScale(0, 0.25f);
            yield return new WaitForSeconds(0.25f);
        }

        SoundManager.instance.PlaySound(11);
        yield return new WaitForSeconds(1f);
        ShowConfetti();
        yield return new WaitForSeconds(3f);

        DisableGame();
        ActiveSpin();
    }

    public void GetTrickGameNo()
    {
        int no = Random.Range(0, trickGameNo.Count);
        trickIndex = trickGameNo[no];
        trickGameNo.RemoveAt(no);
    }

    public IEnumerator ShowTrickGame()
    {
        GetTrickGameNo();
        yield return new WaitForSeconds(2f);
        wheel.transform.GetChild(0).DOScale(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        trickGames[trickIndex].gameObject.SetActive(true);
    }

    private void GetSpookyNo()
    {
        int no = Random.Range(0, spookyNo.Count);
        spookyIndex = spookyNo[no];
        spookyNo.RemoveAt(no);
    }

    public IEnumerator ShowSpookyGame()
    {
        GetSpookyNo();
        yield return new WaitForSeconds(2f);
        wheel.transform.GetChild(0).DOScale(0, 0.5f);
        yield return new WaitForSeconds(1f);
        spooky.SetActive(true);
        spookyGames[spookyIndex].SetActive(true);

        SoundManager.instance.PlaySpookySound(spookyIndex);
    }

    public void DisableSpooky(int index)
    {
        spooky.SetActive(false);
        spookyGames[index].SetActive(false);
        ActiveSpin();
    }
}