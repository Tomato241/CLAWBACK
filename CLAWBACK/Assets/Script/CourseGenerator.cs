using UnityEngine;

public class CourseGenerator : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField]
    private Transform player;

    [Header("コースPrefab")]
    [SerializeField]
    private GameObject[] coursePrefabs;

    [Header("コース設定")]
    [SerializeField]
    private float courseLength = 15.0f;

    [SerializeField]
    private int initialCourseCount = 5;

    [Header("生成設定")]
    [SerializeField]
    private float generateDistance = 30.0f;

    private Vector3 nextPosition;
    private float nextGenerateZ;

    void Start()
    {
        // 最初のコース生成位置
        nextPosition = transform.position;

        // 最初に複数生成
        for (int i = 0; i < initialCourseCount; i++)
        {
            GenerateCourse();
        }

        // 次に生成する基準位置
        nextGenerateZ = nextPosition.z - generateDistance;
    }

    void Update()
    {
        // プレイヤーが一定距離進んだら生成
        if (player.position.z >= nextGenerateZ)
        {
            GenerateCourse();

            // 次の生成タイミングを更新
            nextGenerateZ += courseLength;
        }
    }

    /// <summary>
    /// コースを1つ生成
    /// </summary>
    private void GenerateCourse()
    {
        // ランダムにPrefabを選択
        int randomIndex = Random.Range(0, coursePrefabs.Length);

        // コースを生成
        Instantiate(
            coursePrefabs[randomIndex],
            nextPosition,
            Quaternion.identity
        );

        // 次の生成位置を3マス分進める
        nextPosition += Vector3.forward * courseLength;
    }
}