using TMPro;
using UnityEngine;

public class SettingsDialog : MonoBehaviour
{
    [SerializeField] private GameObject offMusic;
    [SerializeField] private TextMeshProUGUI appVersion;
    [SerializeField] private GameObject restoreButton;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        restoreButton.SetActive(false);    
#endif

        appVersion.text = "Application Version " + Application.version;
        offMusic.SetActive(Container.music.Equals(0));
    }

    public void SetMusic()
    {
        Container.music = Container.music.Equals(0) ? 1 : 0;
        offMusic.SetActive(Container.music.Equals(0));

        CheckMusic();
    }

    private void CheckMusic()
    {
        if (Container.music.Equals(0))
            SoundManager.instance.PauseMusic();
        else
            SoundManager.instance.PlayMusic();
    }

    public void Feedback()
    {
        string feedbackEmail = "support@appletreeappstudio.com";
        Application.OpenURL($"mailto:{feedbackEmail}?subject={Application.productName} Feedback");
    }

    public void Restore()
    {
        Purchaser.instance.RestorePurchases();
    }

    public void PrivacyPolicy()
    {
        Container.PrivacyPolicy();
    }

    public void TermOfUse()
    {
        Container.TermsOfUse();
    }
}