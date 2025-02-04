using IntoTheWilds.Quest;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class QuestHudModel
{
    public Action<QuestHudModel> OnQuestComplited;

    private readonly List<ObjectiveHudModel> _objectiveHudModels = new();
    private readonly List<VisualElement> _objectiveElements = new();

    public VisualElement _questPanel;
    private readonly Label _nameLabel;
    private readonly Label _descriptionLabel;

    private readonly IQuest _targetModel;

    public QuestHudModel(IQuest quest, VisualElement questPanel, VisualTreeAsset objectiveElement)
    {
        _questPanel = questPanel;
        _targetModel = quest;

        _nameLabel = questPanel.Q<Label>("QuestName");
        _descriptionLabel = questPanel.Q<Label>("QuestDisctiption");

        foreach (IObjective objective in quest.Objectives)
        {
            VisualElement objectiveElementHud = objectiveElement.CloneTree();
            _objectiveHudModels.Add(new ObjectiveHudModel(objective, objectiveElementHud));

            _objectiveElements.Add(objectiveElementHud);
        }

        AddListToVisualTree();

        _nameLabel.text = _targetModel.QuestName;
        _descriptionLabel.text = _targetModel.Description;

        _targetModel.OnQuestCompleted += QuestCoplited;
    }

    private void AddListToVisualTree()
    {
        GroupBox objectiveBox = _questPanel.Q<GroupBox>("QuestObjectives");

        foreach (VisualElement objective in _objectiveElements)
        {
            objectiveBox.Add(objective);
        }
    }

    private void QuestCoplited(IQuest _)
    {
        OnQuestComplited?.Invoke(this);
    }
}
