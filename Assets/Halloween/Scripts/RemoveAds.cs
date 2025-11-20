using UnityEngine;
using UnityEngine.UI;

public class RemoveAds : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject restoreButton;

    private void OnEnable()
    {
        buyButton.interactable = Container.noAds.Equals(0);
    }

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        restoreButton.SetActive(false);    
#endif
        SetLocalPrice();
    }

    private void SetLocalPrice()
    {
        if (Purchaser.instance.m_StoreController == null || Purchaser.instance.m_StoreExtensionProvider == null)
            return;

        priceText.text = Purchaser.instance.GetLocalizedPriceString();
    }

    public void Buy()
    {
        Purchaser.instance.BuyNoAds();
    }

    public void Restore()
    {
        Purchaser.instance.RestorePurchases();
    }
}