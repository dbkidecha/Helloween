using UnityEngine;

public class SplashManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadHome), 4f);
    }

    private void LoadHome()
    {
        StartCoroutine(GlobalScript.instance.ChangeScene(1));
    }
}