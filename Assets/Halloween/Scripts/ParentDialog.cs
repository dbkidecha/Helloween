using TMPro;
using UnityEngine;

public class ParentDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    //[SerializeField] private TextMeshProUGUI operationSign;

    [SerializeField] private TextMeshProUGUI[] answerTexts;
    private int answer = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        GenerateQuestion();
    }

    private void GenerateQuestion()
    {
        int d1 = Random.Range(1, 9);
        int d2 = Random.Range(0, 9);
        answer = d1 * d2;

        text1.text = d1.ToString();
        text2.text = d2.ToString();

        int a1 = Random.Range(0, 99);
        int a2 = Random.Range(0, 99);
        int a3 = Random.Range(0, 99);

        answerTexts[0].text = a1.ToString();
        answerTexts[1].text = a2.ToString();
        answerTexts[2].text = a3.ToString();

        answerTexts[Random.Range(0, 3)].text = answer.ToString();
    }

    public void SelectAnswer(int index)
    {
        string text = answerTexts[index].text;
        if (text.Equals(answer.ToString()))
        {
            if (Container.requestedDialog.Equals(RequestDialog.Settings))
                HomeManager.instance.settingPopup.SetActive(true);
            else if (Container.requestedDialog.Equals(RequestDialog.RemoveAds))
                HomeManager.instance.removeAdsPopup.SetActive(true);
            else if (Container.requestedDialog.Equals(RequestDialog.RateUs))
                HomeManager.instance._ShowRateUs();
        }
        gameObject.SetActive(false);
    }
}