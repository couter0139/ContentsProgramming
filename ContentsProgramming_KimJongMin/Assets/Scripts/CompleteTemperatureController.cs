using UnityEngine;

public class CompleteTemperatureController : MonoBehaviour
{
    [Header("타겟(자식 bar)")]
    public Transform bar; // ← 여기에 자식 큐브(Bar) 드래그

    [Header("온도 설정")]
    public float temperature = 25.0f;
    public float maxHeight = 3.0f;

    [Header("디버깅")]
    public bool showDebugInfo = true;

    private Renderer objectRenderer;
    private float nextDebugTime = 0f;

    void Start()
    {
        if (bar == null)
        {
            Debug.LogError("bar(자식 큐브)를 Inspector에 할당하세요!");
            enabled = false;
            return;
        }

        // ✅ 자식(bar)에서 Renderer 가져오기 (원래: GetComponent<Renderer>())
        objectRenderer = bar.GetComponentInChildren<Renderer>();
        if (objectRenderer == null) Debug.LogError("bar 아래에서 Renderer를 찾지 못했습니다.");

        Debug.Log("온도계 시작! 초기 온도: " + temperature + "도");
    }

    void Update()
    {
        // ✅ bar에만 높이 적용 (원래: transform.localScale)
        float height = Mathf.Max(0.1f, (temperature / 50.0f) * maxHeight);
        Vector3 s = bar.localScale; s.y = height; bar.localScale = s;

        // ✅ 피벗이 중앙인 큐브를 '아래 고정'으로 보정
        Vector3 p = bar.localPosition; p.y = height * 0.5f; bar.localPosition = p;

        // 색상은 기존 로직 그대로, 단 대상은 objectRenderer
        if (objectRenderer != null)
        {
            if (temperature < 15.0f)       objectRenderer.material.color = Color.blue;
            else if (temperature < 30.0f)  objectRenderer.material.color = Color.green;
            else                           objectRenderer.material.color = Color.red;
        }

        if (showDebugInfo && Time.time >= nextDebugTime)
        {
            Debug.Log($"[{gameObject.name}] 온도: {temperature}도, 높이: {height:F2}");
            nextDebugTime = Time.time + 1.0f;
        }
    }
}
