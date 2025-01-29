using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace IntoTheWilds.Quest
{
    public class QuestHudPresenter : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _questPanel;
        [SerializeField] private VisualTreeAsset _objectiveElement;

        private GroupBox _questBoxGroup;

        private UIDocument _uiDocument;
        private QuestSystem _questSystem;

        private List<QuestHudModel> _questModelsHud = new();

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _questSystem = GetComponent<QuestSystem>();

            _questBoxGroup = _uiDocument.rootVisualElement.Q<GroupBox>("QuestList");

            _questSystem.OnNewActiveQuestAdded += NewQuestAdded;
        }

        private void NewQuestAdded(IQuest quest)
        {
            VisualElement questPanel = _questPanel.CloneTree();

            QuestHudModel questHudModel = new QuestHudModel(quest, questPanel, _objectiveElement);

            questHudModel.OnQuestComplited += QuestComplited;

            _questModelsHud.Add(questHudModel);

            _questBoxGroup.Add(questPanel);
        }

        private void QuestComplited(QuestHudModel questHudModel)
        {
            questHudModel._questPanel.AddToClassList("questComplite");
        }
    }
}