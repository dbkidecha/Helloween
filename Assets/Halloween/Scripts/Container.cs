using UnityEngine;

public static class Container
{
    public static int rateCount = 0;
    public static RequestDialog requestedDialog;

    public static bool rated
    {
        get { return PlayerPrefs.GetInt("rated", 0) == 0 ? false : true; }
        set { PlayerPrefs.SetInt("rated", value ? 1 : 0); }
    }

    public static bool privacyAgreed
    {
        get { return PlayerPrefs.GetInt("privacyAgreed", 0) == 0 ? false : true; }
        set { PlayerPrefs.SetInt("privacyAgreed", value ? 1 : 0); }
    }

    public static int music
    {
        get { return PlayerPrefs.GetInt("music", 1); }
        set { PlayerPrefs.SetInt("music", value); }
    }

    public static int noAds
    {
        get { return PlayerPrefs.GetInt("noAds", 0); }
        set
        {
            PlayerPrefs.SetInt("noAds", value);

            if (value.Equals(1) && AdsManager.instance != null)
                AdsManager.instance.DestroyBannerView();
        }
    }

    public static void RateUs()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#elif UNITY_IOS
        Application.OpenURL("https://apps.apple.com/us/app/spooky-halloween-games/id6740204883");
#endif
    }

    public static void PrivacyPolicy()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://www.appletreeappstudio.com/privacyPolicy.html");
#elif UNITY_IOS
        Application.OpenURL("https://www.appletreeappstudio.com/privacyPolicy.html");
#endif
    }

    public static void TermsOfUse()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://www.appletreeappstudio.com/termsOfUse.html");
#elif UNITY_IOS
        Application.OpenURL("https://www.appletreeappstudio.com/termsOfUse.html");
#endif
    }
}

public enum Ingredients
{
    Flour,
    Milk,
    BakingPowder,
    Sugar,
    Butter,
    Egg
}

public enum RequestDialog
{
    None = 0,
    Settings = 1,
    RemoveAds = 2,
    RateUs = 3
}