using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    public static TalkManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;

        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?:2", "�� ���� ó�� �Ա���?:1" });
        talkData.Add(2000, new string[] { "����.:1", "�� ȣ���� ���� �Ƹ�����?:0", "��� �� ȣ������ ������ ����� �������ִٰ� ��.:1" });
        talkData.Add(100, new string[] { "����� �������ڴ�." });
        talkData.Add(200, new string[] { "������ ����ߴ� ������ �ִ� å���̴�." });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null; // ���̻� �����ִ� ������ ����
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
