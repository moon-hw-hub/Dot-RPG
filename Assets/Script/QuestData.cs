using UnityEngine;

public class QuestData 
{
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc) // ����ü
    {
        questName = name;
        npcId = npc;
    }
}

