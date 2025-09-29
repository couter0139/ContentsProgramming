using UnityEngine;

public class CompleteTemperatureController : MonoBehaviour
{
    [Header("온도 설정")]
    public float temperature = 25.0f;     // 온도
    public float maxHeight = 3.0f;        // 최대 높이
    
    [Header("디버깅")]
    public bool showDebugInfo = true;     // 디버그 정보 표시
    
    private Renderer objectRenderer;       // Renderer 컴포넌트
    private float nextDebugTime = 0f;      // 다음 디버그 출력 시간
    
    void Start()
    {
        // Renderer 컴포넌트 가져오기
        objectRenderer = GetComponent<Renderer>();
        
        if (objectRenderer == null)
        {
            Debug.LogError("이 GameObject에는 Renderer가 없습니다!");
        }
        
        Debug.Log("온도계 시작! 초기 온도: " + temperature + "도");
    }
    
    void Update()
    {
        // 1. 높이 제어 (온도에 비례)
        // 온도를 높이로 변환 (0~50도 범위)
        float height = temperature / 50.0f * maxHeight;
        if (height < 0.1f) height = 0.1f;  // 최소 높이 보장
        transform.localScale = new Vector3(1, height, 1);
        
        // 2. 색상 제어 (온도 구간별)
        if (objectRenderer != null)
        {
            if (temperature < 15.0f)
            {
                // 추운 날씨 - 파란색
                objectRenderer.material.color = Color.blue;
            }
            else if (temperature >= 15.0f && temperature < 30.0f)
            {
                // 적당한 날씨 - 녹색
                objectRenderer.material.color = Color.green;
            }
            else
            {
                // 더운 날씨 - 빨간색
                objectRenderer.material.color = Color.red;
            }
        }
        
        // 3. 디버그 정보 (1초마다 한 번씩)
        if (showDebugInfo && Time.time >= nextDebugTime)
        {
            Debug.Log("[" + gameObject.name + "] 온도: " + temperature + "도, 높이: " + height.ToString("F2"));
            nextDebugTime = Time.time + 1.0f;  // 1초 후 다시 출력
        }
    }
}
