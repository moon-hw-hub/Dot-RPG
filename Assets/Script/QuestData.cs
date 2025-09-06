using UnityEngine;

public class QuestData 
{
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc) // ±¸Á¶Ã¼
    {
        questName = name;
        npcId = npc;
    }
}

