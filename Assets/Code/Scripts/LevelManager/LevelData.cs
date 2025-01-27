using IntoTheWilds.Quest;
using System.Collections.Generic;
using UnityEngine;

namespace IntoTheWilds
{

    [CreateAssetMenu(fileName = "NewLevelData", menuName = "Data/Level Data")]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public List<QuestData> Quests;
        public int NextLevelSceneBuildIndex;
    }
}