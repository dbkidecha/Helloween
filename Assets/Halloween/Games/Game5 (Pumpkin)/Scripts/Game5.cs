using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game5 : MonoBehaviour
{
    public static Game5 instance;

    public MoveCamera moveCamera;

    public Transform pumpkinParent;
    public Transform[] wall;
    public Animation pumpkinBoard;
    public GameObject pumpkinPrefab;

    public GameObject gameOver;
    public bool isGameOver = false;

    [Header("GameWin")]
    public GameObject gameWin;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI messageText;
    public Transform stars;
    public GameObject fishEffect;

    public TextMeshProUGUI catchCandyText;
    private int _catchCandy = 0;
    public int catchCandy
    {
        get { return _catchCandy; }
        set
        {
            _catchCandy = value;
            catchCandyText.text = value.ToString();
            scoreText.text = value.ToString();
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

        Vector2 edge = Camera.main.ScreenToWorldPoint(Vector2.zero);

        wall[0].position = new Vector3(-edge.x, 0f, 0f);
        wall[1].position = new Vector3(edge.x, 0f, 0f);

        wall[0].SetParent(moveCamera.transform);
        wall[1].SetParent(moveCamera.transform);

        AdsManager.instance.ChangeBannerView(GoogleMobileAds.Api.AdPosition.Bottom);
    }

    public IEnumerator GeneratePumpkin(Transform parent)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(pumpkinPrefab, parent);
    }

    public void GameOver(float wait)
    {
        isGameOver = true;
        moveCamera.enabled = false;
        pumpkinBoard.Stop();

        Invoke(nameof(ShowGameOver), wait);
    }

    private void ShowGameOver()
    {
        gameOver.SetActive(true);        

        AdsManager.instance?.ShowInterstitialAd();
    }

    public void GameWin()
    {
        SetWinData();
        moveCamera.enabled = false;
        gameWin.SetActive(true);

        AdsManager.instance?.ShowInterstitialAd();
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public void Retry()
    {
        GlobalScript.instance.LoadScreen(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetWinData()
    {
        int length = 1;
        if (catchCandy <= 8)
        {
            messageText.text = "Retry";
            length = 1;
        }
        else if (catchCandy >= 9 && catchCandy <= 12)
        {
            messageText.text = "Good";
            length = 2;
        }
        else if (catchCandy >= 13 && catchCandy <= 18)
        {
            messageText.text = "Awesome";
            length = 3;
        }
        else if (catchCandy >= 19 && catchCandy <= 30)
        {
            messageText.text = "Superb";
            length = 4;
        }
        else if (catchCandy >= 31)
        {
            messageText.text = "You Win";
            length = 5;
        }

        for (int i = 0; i < length; i++)
        {
            stars.GetChild(i).gameObject.SetActive(true);
        }
        if (length >= 3)
        {
            fishEffect.gameObject.SetActive(length >= 3);
            SoundManager.instance.PlaySound(5);
        }
    }
}