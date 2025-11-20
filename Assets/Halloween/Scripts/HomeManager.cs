using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public static HomeManager instance;

    [Header("RateUs")]
    [SerializeField] private Transform stars;

    [Space(5)]
    [SerializeField] private GameObject rateUsPopup;
    [SerializeField] private GameObject agreePrivacyPopup;
    [SerializeField] private GameObject parentPopup;
    public GameObject settingPopup;
    public GameObject removeAdsPopup;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalScript.instance.CloseLoading();

        if (!Container.privacyAgreed)
            agreePrivacyPopup?.SetActive(true);

        CheckMusic();

        AdsManager.instance.ShowBannerView();
        AdsManager.instance.ChangeBannerView(GoogleMobileAds.Api.AdPosition.Bottom);

        if (Container.rateCount >= 3 && !Container.rated)
            Invoke(nameof(_ShowRateUs), 1f);
    }

    private void CheckMusic()
    {
        if (Container.music.Equals(0))
            SoundManager.instance.PauseMusic();
    }

    public void ShowRateUs()
    {
        Container.requestedDialog = RequestDialog.RateUs;
        parentPopup?.SetActive(true);
    }

    public void _ShowRateUs()
    {
        rateUsPopup?.SetActive(true);
        Container.rateCount = 0;
    }

    public void SelectStar(int index)
    {
        for (int i = 0; i < stars.childCount; i++)
        {
            if (index >= i)
                stars.GetChild(i).GetChild(0).gameObject.SetActive(true);
            else
                stars.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }

    public void OnRate()
    {
        Container.rated = true;
        Container.RateUs();
        rateUsPopup?.SetActive(false);
    }

    public void PrivacyURL()
    {
        Container.PrivacyPolicy();
    }

    public void AgreePrivacy()
    {
        Container.privacyAgreed = true;
    }

    public void ShowSettings()
    {
        Container.requestedDialog = RequestDialog.Settings;
        parentPopup?.SetActive(true);
    }

    public void ShowRemoveAds()
    {
        Container.requestedDialog = RequestDialog.RemoveAds;
        parentPopup?.SetActive(true);
    }

    public void SelectGame(int gameNo)
    {
        GlobalScript.instance.LoadScreen(gameNo + 1);
        Container.rateCount++;
    }
}