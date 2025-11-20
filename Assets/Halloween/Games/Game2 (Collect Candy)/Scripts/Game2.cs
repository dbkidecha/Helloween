using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2 : MonoBehaviour
{
    public static Game2 instance;

    public Rigidbody2D witch;
    public MoveCamera moveCamera;
    public TextMeshProUGUI totalCandyText;
    public TextMeshProUGUI catchCandyText;

    [Header("GameOver")]
    public GameObject introPanel;
    public GameObject gameOver;
    public TextMeshProUGUI g_catchCandyText;
    public TextMeshProUGUI g_missedCandyText;
    public TextMeshProUGUI g_messageText;
    public Transform g_stars;
    public GameObject g_fishEffect;

    public GameObject hand;

    private int totalCandy = 30;
    private int _catchCandy = 0;
    public int catchCandy
    {
        get { return _catchCandy; }
        set
        {
            _catchCandy = value;
            catchCandyText.text = value.ToString();
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
        totalCandyText.text = totalCandy.ToString();

        moveCamera.enabled = true;
        //introPanel.SetActive(true);
        AdsManager.instance.ChangeBannerView(GoogleMobileAds.Api.AdPosition.Top);
    }

    public void HideIntro()
    {
        introPanel.SetActive(false);
    }

    public void Tap()
    {
        if (witch.transform.localPosition.y < 400)
        {
            if (hand != null)
                hand.SetActive(false);

            witch.gravityScale = 0.8f;
            witch.velocity = Vector2.zero;
            witch.AddForce(Vector2.up * 200);
            SoundManager.instance.PlaySound(1);
        }
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameOver(bool isFinish = false)
    {
        if (!isFinish && !gameOver.activeSelf)
            SoundManager.instance.PlaySound(2);

        SetWinData(isFinish);
        //witch.gravityScale = 1f;
        g_catchCandyText.text = catchCandy.ToString();
        g_missedCandyText.text = (totalCandy - catchCandy).ToString();

        gameOver.SetActive(true);
        moveCamera.enabled = false;

        AdsManager.instance?.ShowInterstitialAd();
    }

    private void SetWinData(bool isFinish = false)
    {
        int length = 1;
        if (catchCandy <= 10)
        {
            g_messageText.text = "Retry";
            length = 1;
        }
        else if (catchCandy >= 11 && catchCandy <= 15)
        {
            g_messageText.text = "Retry";
            length = 2;
        }
        else if (catchCandy >= 16 && catchCandy <= 20)
        {
            g_messageText.text = "Good";
            length = 3;
        }
        else if (catchCandy >= 21 && catchCandy <= 25)
        {
            g_messageText.text = "Superb";
            length = 4;
        }
        else if (catchCandy >= 26)
        {
            g_messageText.text = "You Win";
            length = 5;
        }

        for (int i = 0; i < length; i++)
        {
            g_stars.GetChild(i).gameObject.SetActive(true);
        }
        if (length >= 3)
        {
            g_fishEffect.gameObject.SetActive(length >= 3);
            SoundManager.instance.PlaySound(4);
        }
    }
}