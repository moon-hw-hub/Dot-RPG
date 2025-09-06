using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TypeEffect : MonoBehaviour
{
    string targetMsg;
    public float CPS; // ���� ��� �ӵ�
    public GameObject EndCursor;
    TextMeshProUGUI msgText;
    AudioSource audioSource;
    int index;
    float interval;
    public bool isAnim; //�ִϸ��̼� ���� �Ǵ��� ���� �÷��� ���� ����

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim) // Interupt
        {
            msgText.text = targetMsg; // �� ä���
            CancelInvoke(); // �κ�ũ �Լ� ����
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

    void Effecting() // ���
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
            Debug.Log("���� ���");
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
