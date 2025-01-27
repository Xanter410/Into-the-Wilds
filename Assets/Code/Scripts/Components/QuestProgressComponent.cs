using IntoTheWilds.Quest;
using UnityEngine;

public class QuestProgressComponent : MonoBehaviour
{
    [SerializeField] private GameEventQuestProgress _gameEventQuestProgress;
    [SerializeField] private ObjectiveType _objectiveType;
    [SerializeField] private int _amount;

    public void ToCountProgress()
    {
        _gameEventQuestProgress.TriggerEvent(new QuestProgressData
        {
            ObjectiveType = _objectiveType,
            ResourceType = IntoTheWilds.ResourceType.None,
            Amount = _amount
        });
    }
}
