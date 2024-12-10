using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public Image flashImage; // 반투명 이미지를 참조
    public float fadeDuration = 1.0f; // 화면 복귀에 걸리는 시간

    private void Start()
    {
        // 초기 상태를 투명으로 설정
        if (flashImage != null)
        {
            flashImage.color = new Color(1, 1, 1, 0); // 완전히 투명
        }
    }

    
    public void FlashScreen()
    {
        if (flashImage != null)
        {
            
            // 즉시 반투명 흰색으로 덮기
            flashImage.color = new Color(1, 1, 1, 0.5f);
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private System.Collections.IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        // 점점 투명해지기
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0, elapsedTime / fadeDuration); // 점점 투명
            flashImage.color = new Color(1, 1, 1, alpha); // 투명도 적용
            yield return null;
        }

        // 최종적으로 완전히 투명
        flashImage.color = new Color(1, 1, 1, 0);
    }
}
