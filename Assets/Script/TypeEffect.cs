using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    public float CPS; // 글자 재생 속도
    public GameObject EndCursor;
    TextMeshProUGUI msgText;
    AudioSource audioSource;
    int index;
    float interval;
    public bool isAnim; //애니메이션 실행 판단을 위한 플래그 변수 생성

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim) // Interupt
        {
            msgText.text = targetMsg; // 다 채우기
            CancelInvoke(); // 인보크 함수 꺼짐
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        // Start Animation
        interval = 1.0f/CPS;
        Debug.Log(interval);

        isAnim = true;

        Invoke("Effecting", 1/CPS);
    }

    void Effecting() // 재귀
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        
        //Sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
        {
            audioSource.Play();
            Debug.Log("사운드 재생");
        }
        Invoke("Effecting", 1 / CPS);
        index++;
    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
