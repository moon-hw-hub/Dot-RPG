using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction; // ��ȣ�ۿ� ������ �ƴ��� �Ǵ�

    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Action(GameObject scanObj)
    {
        if (isAction) // Enter Action
        {
            isAction = false;
        }
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "�̰��� �̸��� " + scanObject.name + "�̶�� �Ѵ�.";
        }

        talkPanel.SetActive(isAction);
    }

}
