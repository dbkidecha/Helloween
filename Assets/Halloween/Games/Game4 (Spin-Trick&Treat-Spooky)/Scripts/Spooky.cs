using DG.Tweening;
using UnityEngine;

public class Spooky : MonoBehaviour
{
    private void OnEnable()
    {
        SoundManager.instance.PlaySpookySound(transform.GetSiblingIndex());
    }

    public void Finished()
    {
        transform.DOScale(0f, 0.5f).OnComplete(() =>
        {
            Game4.instance.DisableSpooky(transform.GetSiblingIndex());
        });
    }
}