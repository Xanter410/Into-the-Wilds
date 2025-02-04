using System.Collections.Generic;

namespace IntoTheWilds.Quest
{
    public class QuestFactory
    {
        public static IQuest CreateQuest(QuestData questData)
        {
            var objectives = new List<IObjective>();

            foreach (ObjectiveData objData in questData.Objectives)
            {
                switch (objData.Type)
                {
                    case ObjectiveType.Kill:
                        objectives.Add(new KillObjective(objData.Description, objData.TargetAmount));
                        break;
                    case ObjectiveType.Collect:
                        objectives.Add(new CollectObjective(objData.Description, objData.TargetAmount, objData.ResourceType));
                        break;
                    case ObjectiveType.Find:
                        objectives.Add(new FindObjective(objData.Description, objData.TargetAmount));
                        break;
                    default:
                        throw new System.NotImplementedException($"Объект квеста типа: {objData.Type} не поддерживается!");
                }
            }

            return new QuestBase(questData.QuestName, questData.Description, objectives, questData.ConditionsActivation);
        }
    }
}
