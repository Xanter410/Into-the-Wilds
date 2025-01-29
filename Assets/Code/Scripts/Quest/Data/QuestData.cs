using System.Collections.Generic;
using UnityEngine;

namespace IntoTheWilds.Quest
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Data/Quest")]
    public class QuestData : ScriptableObject
    {
        public string QuestName;
        public string Description;
        public List<ObjectiveData> Objectives;
        public List<string> ConditionsActivation;
    }

    [System.Serializable]
    public class ObjectiveData
    {
        public string Description;
        public int TargetAmount;
        public ObjectiveType Type;
        public ResourceType ResourceType;
    }

    public enum ObjectiveType { Kill, Collect, Find }
}
