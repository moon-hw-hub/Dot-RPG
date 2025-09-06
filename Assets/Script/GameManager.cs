using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //public GameObject talkPanel;
    public Animator talkPanel;
    public Image portraitImg;
    public Animator portraitAnim;
    public Sprite prevPortrait;
    //public TextMeshProUGUI talkText;
    public TypeEffect talk;
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

    void Start()
    {
        Debug.Log(QuestManager.instance.CheckQuest());
        
    }

    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>(); // 오브젝트의 데이터를 가져옴
        Talk(objData.id, objData.isNpc); // 대화
        //Debug.Log(objData.id + ", " + objData.isNpc);

        // Visible Talk for Action
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        // Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        //talk.SetMsg("");
        else
        {
            questTalkIndex = QuestManager.instance.GetQuestTalkIndex(id); // 퀘스트번호를 가져옴
            talkData = TalkManager.instance.GetTalk(id + questTalkIndex, talkIndex); // npc id + 퀘스트 번호 = 퀘스트 대화 데이터 ID
        }

        //int questTalkIndex = QuestManager.instance.GetQuestTalkIndex(id); // 퀘스트번호를 가져옴
        //string talkData = TalkManager.instance.GetTalk(id + questTalkIndex, talkIndex); // npc id + 퀘스트 번호 = 퀘스트 대화 데이터 ID

        //End Talk
        if (talkData == null) 
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(QuestManager.instance.CheckQuest(id)); // 퀘스트 진행 상황 체크
            return; // 대화 끝
        }

        // Continue Talk
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]); // 수정

            portraitImg.sprite = TalkManager.instance.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1); // 알파값 1로 변경
            //Animation Portrait
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite; //갱신
            }
        }
        else
        {
            //talkText.text = talkData;
            talk.SetMsg(talkData); // 수정

            //Hide Portrait
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

}
