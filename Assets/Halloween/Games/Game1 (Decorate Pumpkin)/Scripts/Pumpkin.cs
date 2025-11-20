using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pumpkin : MonoBehaviour
{
    public GameObject knifeBtn;
    public GameObject props;
    public GameObject[] dItems;

    public Image face;
    public GameObject knife;
    public GameObject pumpkin;
    public GameObject finishAnim;
    public GameObject hand;

    public void PropsScale(bool isZero)
    {
        StartCoroutine(Scale(isZero));
    }

    private IEnumerator Scale(bool isZero)
    {
        for (int i = 0; i < props.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.25f);
            props.transform.GetChild(i).transform.DOScale(isZero ? 0 : 1, 0.25f);
        }
        if (!isZero)
        {
            SoundManager.instance.PlaySound(4);
            if (hand != null)
                hand.SetActive(true);
        }
    }
}