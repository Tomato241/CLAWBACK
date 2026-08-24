using UnityEngine;

public class ChainSwing : MonoBehaviour
{
    [Header("Chain")]
    [SerializeField] private Transform m_target;

    [Header("Movement")]
    [SerializeField] private float m_followSpeed = 8.0f; // チェーンがターゲットに追従する速度
    [SerializeField] private float m_swingPower = 15.0f; // チェーンの揺れの強さ
    [SerializeField] private float m_damping = 2.5f; // チェーンの揺れの減衰速度

    [Header("Weight")]
    [SerializeField] private float m_weight = 0.0f; // チェーンの重さ（揺れの強さに影響）

    private Vector3 m_velocity;
    private float m_currentSwing;

    private void Update()
    {
        if (m_target == null)
            return;

        // 前のチェーンについていく
        Vector3 targetPos = m_target.position;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            m_followSpeed * Time.deltaTime
        );

        // 重さによって揺れを強くする
        float weightPower = 1.0f + m_weight;

        // 揺れを徐々に減衰
        m_currentSwing = Mathf.Lerp(
            m_currentSwing,
            0.0f,
            m_damping * Time.deltaTime
        );

        // X方向に揺らす
        transform.position += new Vector3(
            m_currentSwing * m_swingPower * weightPower * Time.deltaTime,
            0.0f,
            0.0f
        );
    }

    public void AddSwing(float power)
    {
        m_currentSwing += power;
    }

    public void SetWeight(float weight)
    {
        m_weight = weight;
    }
}