using IntoTheWilds.Quest;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class QuestHudModel
{
    public Action<QuestHudModel> OnQuestComplited;

    private List<ObjectiveHudModel> _objectiveHudModels = new();
    private List<VisualElement> _objectiveElements = new();

    public VisualElement _questPanel;
    private Label _nameLabel;
    private Label _descriptionLabel;

    private IQuest _targetModel;

    public QuestHudModel(IQuest quest, VisualElement questPanel, VisualTreeAsset objectiveElement)
    {
        _questPanel = questPanel;
        _targetModel = quest;

        _nameLabel = questPanel.Q<Label>("QuestName");
        _descriptionLabel = questPanel.Q<Label>("QuestDisctiption");

        foreach (var objective in quest.Objectives)
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

        foreach (var objective in _objectiveElements)
        {
            objectiveBox.Add(objective);
        }
    }

    private void QuestCoplited(IQuest _)
    {
        OnQuestComplited?.Invoke(this);
    }
}
