using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Shell : MonoBehaviour
{
    public bool isOpen = true;
    public Image baseObject;
    public Image innerObject;
    public Image cover;

    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = Vector3.zero;
    }

    public void OnClick()
    {
        if (!isOpen)
        {
            Game3.Instance.CheckShell(this);
            Game3.Instance.hand.SetActive(false);
        }
    }

    public void OpenShell(bool playSound = false)
    {
        isOpen = true;
        transform.DOScaleX(0f, 0.5f).OnComplete(() =>
        {
            cover.gameObject.SetActive(false);
            transform.DOScaleX(1f, 0.5f);
        });

        if (playSound)
            SoundManager.instance.PlaySound(0);
    }

    public void CloseShell(bool playSound = false)
    {
        transform.DOScaleX(0f, 0.5f).SetEase(Ease.Flash).OnComplete(() =>
        {
            cover.gameObject.SetActive(true);
            transform.DOScaleX(1f, 0.5f).SetEase(Ease.Flash).OnComplete(() =>
            {
                isOpen = false;
            });

            if (playSound)
                SoundManager.instance.PlaySound(0);
        });
    }

    public void ShakeShell()
    {
        transform.DOShakePosition(0.7f, 8f).SetEase(Ease.Flash);
    }
}