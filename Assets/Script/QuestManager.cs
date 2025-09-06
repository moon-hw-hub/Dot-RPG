using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // ����Ʈ ��ȭ���� ���� ����
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList; // ����Ʈ ���̵�, ����Ʈ ������

    public static QuestManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        questList = new Dictionary<int, QuestData>();
        GenerateData();

    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("���� ������ ��ȭ�ϱ�.", new int[] { 1000, 2000 })); //10 : ����Ʈ ���̵�, �ش� ����Ʈ�� ������ npc ���̵�
        questList.Add(20, new QuestData("�絵�� ���� ã���ֱ�.", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("����Ʈ �� Ŭ����!", new int[] { 0 })); //���� ������ ���̵�����
    }

    public int GetQuestTalkIndex(int id) // NPC Id�� ����
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // Next Talk Target
        if (id == questList[questId].npcId[questActionIndex]) // ������ �°� ��ȭ���� ���� ����Ʈ ��ȭ������ �ø�
            questActionIndex++;

        //Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length) //����Ʈ ��ȭ������ ���� �������� �� ����Ʈ��ȣ ����
            NextQuest();

        //Quest Name
        return questList[questId].questName;
    }

    public string CheckQuest() // �޼��� �����ε� : �Ű������� ���� �Լ� ȣ��
    {
        //Quest Name
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject() //����Ʈ ������Ʈ�� ������ �Լ� ����
    {
        switch (questId)
        {
            case 10:
                if(questActionIndex==2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if(questActionIndex==1)
                    questObject[0].SetActive(false);
                break;
        }
    }

}
