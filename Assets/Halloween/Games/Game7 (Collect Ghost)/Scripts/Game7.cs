using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game7 : MonoBehaviour
{
    public static Game7 instance;

    public Transform top, bottom, left, right;
    public Ghost[] ghosts;
    public Transform playingArea;
    public NetBucket net;

    private int maxGhost = 20;
    private int _ghostCount = 0;
    private int ghostCount
    {
        get { return _ghostCount; }
        set
        {
            _ghostCount = value;
            if (value >= maxGhost)
            {
                CancelInvoke(nameof(GenerateGhost));
                if (!gameOver)
                    Invoke(nameof(ShowWinPanel), 3f);
            }
        }
    }
    public TextMeshProUGUI ghostCollectedText;
    private int _ghostCollected;
    public int ghostCollected
    {
        get { return _ghostCollected; }
        set
        {
            if (!gameOver)
            {
                _ghostCollected = value;
                ghostCollectedText.text = value + "/" + maxGhost.ToString();
            }
        }
    }

    public GameObject fireBall;
    private bool gameOver = false;
    public GameObject gameOverPanel;

    [Header("Win")]
    public GameObject winPanel;
    public TextMeshProUGUI g_catchCandyText;
    public TextMeshProUGUI g_missedCandyText;
    public TextMeshProUGUI g_messageText;
    public Transform g_stars;
    public GameObject g_winEffect;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalScript.instance.CloseLoading();
        InvokeRepeating(nameof(GenerateGhost), 0f, 2f);
        InvokeRepeating(nameof(GenerateFireBall), 1f, 3f);
        InvokeRepeating(nameof(RepeatGhostVoice), 1f, 25f);
    }

    private void GenerateGhost()
    {
        int dir = Random.Range(0, 4);

        if (dir.Equals(0)) //top
        {
            Ghost ghost = Instantiate(ghosts[Random.Range(0, 4)], top.position, Quaternion.identity, playingArea);
            Vector3 pos = new Vector3(bottom.position.x + Random.Range(-8f, 8f), bottom.position.y, 0f);
            ghost.transform.DOMove(pos, 4f).SetEase(Ease.Flash).SetId(ghost.name).OnComplete(() =>
            {
                Destroy(ghost.gameObject);
            });
            ghostCount++;
        }
        else if (dir.Equals(1)) //bottom
        {
            Ghost ghost = Instantiate(ghosts[Random.Range(4, 9)], bottom.position, Quaternion.identity, playingArea);
            Vector3 pos = new Vector3(top.position.x + Random.Range(-8f, 8f), top.position.y, 0f);
            ghost.transform.DOMove(pos, 4f).SetEase(Ease.Flash).SetId(ghost.name).OnComplete(() =>
            {
                Destroy(ghost.gameObject);
            });
            ghostCount++;
        }
        else if (dir.Equals(2)) //left
        {
            Ghost ghost = Instantiate(ghosts[Random.Range(0, 9)], left.position, Quaternion.identity, playingArea);
            Vector3 pos = new Vector3(right.position.x, right.position.y + Random.Range(-5f, 5f), 0f);
            ghost.transform.DOMove(pos, 5f).SetEase(Ease.Flash).SetId(ghost.name).OnComplete(() =>
            {
                Destroy(ghost.gameObject);
            });
            ghostCount++;
        }
        else if (dir.Equals(3)) //right
        {
            Ghost ghost = Instantiate(ghosts[Random.Range(0, 9)], right.position, Quaternion.identity, playingArea);
            Vector3 pos = new Vector3(left.position.x, left.position.y + Random.Range(-5f, 5f), 0f);
            ghost.transform.DOMove(pos, 5f).SetEase(Ease.Flash).SetId(ghost.name).OnComplete(() =>
            {
                Destroy(ghost.gameObject);
            });
            ghostCount++;
        }
    }

    public void GenerateFireBall()
    {
        Vector3 pos = new Vector3(Random.Range(-6.5f, 6.5f), 6f, 0f);
        _ = Instantiate(fireBall, pos, Quaternion.identity, playingArea);
    }

    public void BurnNet()
    {
        gameOver = true;

        net.burnNet.SetActive(true);
        net.netImage.SetActive(false);
        net.baseObj.SetActive(false);

        CancelInvoke(nameof(GenerateGhost));
        CancelInvoke(nameof(GenerateFireBall));
        CancelInvoke(nameof(RepeatGhostVoice));

        Invoke(nameof(ShowGameOver), 1.5f);
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        AdsManager.instance?.ShowInterstitialAd();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public void ShowWinPanel()
    {
        SetWinData();
        g_catchCandyText.text = ghostCollected.ToString();
        g_missedCandyText.text = (maxGhost - ghostCollected).ToString();

        winPanel.SetActive(true);

        AdsManager.instance?.ShowInterstitialAd();
    }

    private void SetWinData()
    {
        int length = 1;
        if (ghostCollected <= 8)
        {
            g_messageText.text = "Retry";
            length = 1;
        }
        else if (ghostCollected >= 9 && ghostCollected <= 12)
        {
            g_messageText.text = "Retry";
            length = 2;
        }
        else if (ghostCollected >= 13 && ghostCollected <= 15)
        {
            g_messageText.text = "Good";
            length = 3;
        }
        else if (ghostCollected >= 16 && ghostCollected <= 18)
        {
            g_messageText.text = "Superb";
            length = 4;
        }
        else if (ghostCollected >= 19)
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
            g_winEffect.gameObject.SetActive(length >= 3);
            SoundManager.instance.PlaySound(11);
        }
    }

    private void RepeatGhostVoice()
    {
        SoundManager.instance.PlaySound(0);
    }
}