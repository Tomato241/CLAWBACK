using UnityEngine;
using UnityEngine.InputSystem;

public class MoveArm : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private int life = 3;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Gamepad.current == null)
            return;

        Vector2 input = Gamepad.current.leftStick.ReadValue();

        Vector3 move = new Vector3(
            input.x,
            0.0f,
            input.y
        );

        rb.MovePosition(
            rb.position + move * moveSpeed * Time.fixedDeltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(
            "Triggerに入りました : " + other.gameObject.name
        );

        if (other.CompareTag("Wall"))
        {
            life--;

            Debug.Log(
                "壁に当たった！ 残りライフ : " + life
            );
        }
    }
}