using UnityEngine;
using UnityEngine.InputSystem;
//
//アームの動きを制御するスクリプト
//
public class MoveArm : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.0f; //アームの移動速度
    [SerializeField] private int life = 3; //アームのライフ
    private void Start()
    {
        
    }
    private void Update()
    {
        //スティックの入力を取得
        if(Gamepad.current.leftStick.ReadValue().x != 0 || Gamepad.current.leftStick.ReadValue().y  != 0)
        {
            armMove();
        }
    }
    private void armMove()
    {
        //スティックの入力値を取得
        Vector2 stickInput = Gamepad.current.leftStick.ReadValue();
        //アームの移動量を計算
        Vector3 moveAmount = new Vector3(stickInput.x, 0, stickInput.y) * moveSpeed * Time.deltaTime;
        //アームの位置を更新
        transform.position += moveAmount;
    }
    //壁に当たったらライフを減らす処理
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //ライフを減らす処理
            life--;
            Debug.Log("ライフが減りました");
        }
    }
}
