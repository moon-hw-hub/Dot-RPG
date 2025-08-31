using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Image portraitImg;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;
    public bool isAction; // 상호작용 중인지 아닌지 판단
    public int talkIndex; // 대화 인덱스

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
        ObjData objData = scanObject.GetComponent<ObjData>(); // 오브젝트의 데이터를 가져옴
        Talk(objData.id, objData.isNpc); // 대화
        //Debug.Log(objData.id + ", " + objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = TalkManager.instance.GetTalk(id, talkIndex);

        if (talkData == null) // 더이상 남아있는 문장이 없음
        {
            isAction = false;
            talkIndex = 0;
            return; // 대화 끝
        }
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            portraitImg.sprite = TalkManager.instance.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1); // 알파값 1로 변경
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
