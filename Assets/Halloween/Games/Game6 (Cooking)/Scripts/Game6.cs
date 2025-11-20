using System.Collections;
using UnityEngine;

public class Game6 : MonoBehaviour
{
    public static Game6 instance;
    public Canvas canvas;
    public int shapeIndex = 0;
    public GameObject[] rounds;
    public GameObject[] round3Games;
    public GameObject roundParticle, finalParticle;
    public GameObject fadeScreen;

    private int _round;
    public int round
    {
        get { return _round; }
        set
        {
            _round = value;
            rounds[value - 1].SetActive(false);
            rounds[value].SetActive(true);

            if (value >= 2)
                round3Games[shapeIndex].SetActive(true);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalScript.instance.CloseLoading();

        AdsManager.instance.HideBannerView();
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public IEnumerator ShowParticle(int game)
    {
        SoundManager.instance.PlaySound(game < 2 ? 2 : 9);
        yield return new WaitForSeconds(1f);

        if (game < 2)
        {
            roundParticle.SetActive(true);
            StartCoroutine(FadeScreen());
        }
        else
        {
            AdsManager.instance?.ShowInterstitialAd();
            finalParticle.SetActive(true);
            Invoke(nameof(Home), 6f);
        }
    }

    private IEnumerator FadeScreen()
    {
        yield return new WaitForSeconds(4f);
        roundParticle.SetActive(false);
        fadeScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        round++;
        //SoundManager.instance.StopSound();
        yield return new WaitForSeconds(0.5f);
        fadeScreen.SetActive(false);
    }
}