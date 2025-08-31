using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Image portraitImg;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction; // ��ȣ�ۿ� ������ �ƴ��� �Ǵ�
    public int talkIndex; // ��ȭ �ε���

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
        isAction = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>(); // ������Ʈ�� �����͸� ������
        Talk(objData.id, objData.isNpc); // ��ȭ
        //Debug.Log(objData.id + ", " + objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = TalkManager.instance.GetTalk(id, talkIndex);

        if (talkData == null) // ���̻� �����ִ� ������ ����
        {
            isAction = false;
            talkIndex = 0;
            return; // ��ȭ ��
        }
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = TalkManager.instance.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1); // ���İ� 1�� ����
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

}
