using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game3 : MonoBehaviour
{
    public static Game3 Instance;

    [SerializeField] private Shell[] shells;
    [SerializeField] private LevelData[] levelData;
    [SerializeField] private Animation anim;
    [SerializeField] private GameObject tapParticle;
    [SerializeField] private GameObject goodJob;
    [SerializeField] private GameObject completeGame;
    public GameObject hand;
    private bool handShown = false;

    private List<Shell> tapShell = new List<Shell>();
    private bool allowTap = true;

    private int _shapeTaken = 0;
    public int shapeTaken
    {
        get { return _shapeTaken; }
        set
        {
            _shapeTaken = value;
            if (value >= 3)
                gameComplete++;
        }
    }

    private int _gameComplete = 0;
    public int gameComplete
    {
        get { return _gameComplete; }
        set
        {
            _gameComplete = value;
            shapeTaken = 0;
            if (value >= shells.Length)
            {
                // game completed
                StartCoroutine(ShowGameComplete());
            }
            else
            {
                StartCoroutine(ShowGoodJob());
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalScript.instance.CloseLoading();
        StartCoroutine(SetShellData(gameComplete));

        //InvokeRepeating(nameof(PlayWitchSound), 5f, 20f);
        AdsManager.instance.ChangeBannerView(GoogleMobileAds.Api.AdPosition.Top);
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    private IEnumerator ChangeSheet()
    {
        yield return new WaitForSeconds(0.5f);
        anim.Play();
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SetShellData(gameComplete));
    }

    private IEnumerator SetShellData(int level)
    {
        LevelData data = levelData[level];
        for (int i = 0; i < shells.Length; i++)
        {
            shells[i].isOpen = true;
            shells[i].cover.gameObject.SetActive(true);
            shells[i].name = data.levelData[i].name.ToString();
            shells[i].cover.sprite = data.cover;
            shells[i].baseObject.color = data.levelData[i].color;
            shells[i].innerObject.sprite = data.levelData[i].sprite;
        }
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySound(1);
        for (int i = 0; i < shells.Length; i++)
        {
            shells[i].OpenShell();
            shells[i].isOpen = true;
        }
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySound(2);
        for (int i = 0; i < shells.Length; i++)
        {
            shells[i].CloseShell();
            shells[i].isOpen = false;
        }

        if (!handShown)
        {
            hand.SetActive(true);
            handShown = true;
        }
    }

    public void CheckShell(Shell shell)
    {
        if (tapShell.Count < 2 && allowTap)
        {
            SoundManager.instance.PlaySound(0);
            tapShell.Add(shell);
            shell.OpenShell();
            Invoke(nameof(OpenSound), 0.5f);

            if (tapShell.Count >= 2)
            {
                allowTap = false;

                if (tapShell[0].name.Equals(tapShell[1].name))
                {
                    Invoke(nameof(ClearShells), 1f);
                    Invoke(nameof(EnableTap), 1f);
                }
                else
                {
                    StartCoroutine(CloseShells());
                }
            }
        }
    }

    private void OpenSound()
    {
        SoundManager.instance.PlaySound(1);
    }

    private void ClearShells()
    {
        SoundManager.instance.PlaySound(3);
        shapeTaken++;
        PlayTapParticle(tapShell[0].transform.position);
        PlayTapParticle(tapShell[1].transform.position);
    }

    private IEnumerator CloseShells()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySound(4);

        tapShell[0].ShakeShell();
        tapShell[1].ShakeShell();

        yield return new WaitForSeconds(0.7f);

        tapShell[0].CloseShell();
        tapShell[1].CloseShell();
        SoundManager.instance.PlaySound(2);
        yield return new WaitForSeconds(0.5f);
        EnableTap();
    }

    private void EnableTap()
    {
        tapShell.Clear();
        allowTap = true;
    }

    private IEnumerator ShowGoodJob()
    {
        goodJob.SetActive(true);
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySound(5);
        yield return new WaitForSeconds(3f);
        goodJob.SetActive(false);
        StartCoroutine(ChangeSheet());
    }

    private IEnumerator ShowGameComplete()
    {
        yield return new WaitForSeconds(0.5f);
        AdsManager.instance?.ShowInterstitialAd();

        completeGame.SetActive(true);
        SoundManager.instance.PlaySound(6);
        yield return new WaitForSeconds(4f);
        GlobalScript.instance.LoadScreen(1);
    }

    private void PlayWitchSound()
    {
        SoundManager.instance.PlaySound(7);
    }

    private void PlayTapParticle(Vector2 pos)
    {
        _ = Instantiate(tapParticle, pos, Quaternion.identity, null);
    }
}