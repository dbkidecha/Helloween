using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game1 : MonoBehaviour
{
    public static Game1 instance;
    public Canvas canvas;
    public Pumpkin[] pLevels;
    public Image[] completedItem;
    public GameObject masterConfetti;
    public GameObject tapParticle;
    public GameObject starParticle;

    private int _level = 0;
    public int level
    {
        get { return _level; }
        set { _level = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalScript.instance.CloseLoading();
        AdsManager.instance.ChangeBannerView(GoogleMobileAds.Api.AdPosition.Top);

        //InvokeRepeating(nameof(PlayHaHaSound), 1f, 30f);
    }

    public void Home()
    {
        GlobalScript.instance.LoadScreen(1);
    }

    public void ShowPumpkinDecoration(bool isKnife, string no)
    {
        if (isKnife)
        {
            pLevels[level].knife.SetActive(false);
            pLevels[level].knifeBtn.SetActive(false);

            pLevels[level].props.SetActive(true);
            pLevels[level].PropsScale(false);
        }
        else
        {
            pLevels[level].dItems[int.Parse(no)].SetActive(true);
            StartCoroutine(CheckLevelComplete(pLevels[level].dItems));
        }
    }

    private IEnumerator CheckLevelComplete(GameObject[] dItems)
    {
        int count = 0;
        for (int i = 0; i < dItems.Length; i++)
        {
            if (dItems[i].activeSelf)
                count++;
        }
        if (count >= dItems.Length)
        {
            level++;
            pLevels[level - 1].PropsScale(true);
            StarEffect(new Vector2(0f, -1.6f));
            SoundManager.instance.PlaySound(7);
            pLevels[level - 1].pumpkin.SetActive(false);
            pLevels[level - 1].finishAnim.SetActive(true);
            SoundManager.instance.PlaySound(9);

            yield return new WaitForSeconds(4f);
            pLevels[level - 1].transform.GetChild(0).DOScale(0.14f, 0.5f);
            completedItem[level - 1].gameObject.SetActive(true);
            SoundManager.instance.PlaySound(5);
            SoundManager.instance.PlaySound(11);
            pLevels[level - 1].transform.GetChild(0).DOMove(completedItem[level - 1].transform.position, 1.5f).OnComplete(() =>
            {
                pLevels[level - 1].transform.GetChild(0).SetParent(completedItem[level - 1].transform);

                pLevels[level - 1].gameObject.SetActive(false);
                pLevels[level >= pLevels.Length ? pLevels.Length - 1 : level].gameObject.SetActive(true);

                if (level >= pLevels.Length)
                {
                    SetToCenter();
                    StartCoroutine(ShowMasterConfetti());
                }
            });
        }
    }

    private void SetToCenter()
    {
        for (int i = 0; i < completedItem.Length; i++)
        {
            completedItem[i].rectTransform.DOSizeDelta(new Vector2(300, 300), 1f);
            completedItem[i].transform.GetChild(0).DOScale(0.21f, 1f);

            if (i == 0)
                completedItem[i].transform.DOLocalMove(new Vector3(-700, 0, 0), 1f);
            else if (i == 1)
                completedItem[i].transform.DOLocalMove(new Vector3(-350, 0, 0), 1f);
            else if (i == 2)
                completedItem[i].transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
            else if (i == 3)
                completedItem[i].transform.DOLocalMove(new Vector3(350, 0, 0), 1f);
            else if (i == 4)
                completedItem[i].transform.DOLocalMove(new Vector3(700, 0, 0), 1f);
        }

        AdsManager.instance?.ShowInterstitialAd();
    }

    private IEnumerator ShowMasterConfetti()
    {
        SoundManager.instance.PlaySound(8);
        yield return new WaitForSeconds(1f);
        masterConfetti.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        GlobalScript.instance.LoadScreen(1);
    }

    public void TapEffect(Vector2 pos)
    {
        _ = Instantiate(tapParticle, pos, Quaternion.identity, null);
    }

    public void StarEffect(Vector2 pos)
    {
        _ = Instantiate(starParticle, pos, Quaternion.identity, null);
    }

    private void PlayHaHaSound()
    {
        SoundManager.instance.PlaySound(level < 4 ? 9 : 10);
    }
}