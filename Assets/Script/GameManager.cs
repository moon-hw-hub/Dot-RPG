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

    void Start()
    {
        Debug.Log(QuestManager.instance.CheckQuest());
        
    }

    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>(); // ������Ʈ�� �����͸� ������
        Talk(objData.id, objData.isNpc); // ��ȭ
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
            questTalkIndex = QuestManager.instance.GetQuestTalkIndex(id); // ����Ʈ��ȣ�� ������
            talkData = TalkManager.instance.GetTalk(id + questTalkIndex, talkIndex); // npc id + ����Ʈ ��ȣ = ����Ʈ ��ȭ ������ ID
        }

        //int questTalkIndex = QuestManager.instance.GetQuestTalkIndex(id); // ����Ʈ��ȣ�� ������
        //string talkData = TalkManager.instance.GetTalk(id + questTalkIndex, talkIndex); // npc id + ����Ʈ ��ȣ = ����Ʈ ��ȭ ������ ID

        //End Talk
        if (talkData == null) 
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(QuestManager.instance.CheckQuest(id)); // ����Ʈ ���� ��Ȳ üũ
            return; // ��ȭ ��
        }

        // Continue Talk
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]); // ����

            portraitImg.sprite = TalkManager.instance.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1); // ���İ� 1�� ����
            //Animation Portrait
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite; //����
            }
        }
        else
        {
            //talkText.text = talkData;
            talk.SetMsg(talkData); // ����

            //Hide Portrait
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

}
