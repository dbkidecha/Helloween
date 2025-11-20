using DG.Tweening;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public float wait = 0;
    public float moveValue;
    public float time = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartAnimation), wait);
    }

    private void StartAnimation()
    {
        transform.DOLocalMoveY(transform.localPosition.y + moveValue, time).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Flash);
    }
}