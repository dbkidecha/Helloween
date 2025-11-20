using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (Time.deltaTime * speed), transform.position.y, -10f);
    }
}