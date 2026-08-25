using UnityEngine;
//回転する壁を制御するスクリプト
public class RotatingWall : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 0.0f; //回転速度
    [SerializeField]
    private float rotationAngle = 0.0f; //回転角度
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(Vector3.up * rotationAngle, rotationSpeed * Time.deltaTime);
    }
}
