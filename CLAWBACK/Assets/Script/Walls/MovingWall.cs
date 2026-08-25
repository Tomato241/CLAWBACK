using UnityEngine;

// 指定した範囲を往復移動する壁
public class MovingWall : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Vector3 moveDirection = Vector3.right;

    [SerializeField]
    private float moveRange = 5.0f;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        startPosition = transform.position;
        moveDirection.Normalize();
    }

    void Update()
    {
        timer += moveSpeed * Time.deltaTime;

        float distance = Mathf.PingPong(timer, moveRange);

        transform.position =
            startPosition + moveDirection * distance;
    }
}