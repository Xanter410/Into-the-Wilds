using Tools.GameEvent;
using UnityEngine;

namespace IntoTheWilds.Quest
{
    public struct QuestProgressData
    {
        public ObjectiveType ObjectiveType;
        public ResourceType ResourceType;
        public int Amount;
    }
    [CreateAssetMenu(fileName = "GameEvent_", menuName = "Game System/Game Event/Quest Progress")]
    public class GameEventQuestProgress : GameEvent<QuestProgressData>
    {}
}