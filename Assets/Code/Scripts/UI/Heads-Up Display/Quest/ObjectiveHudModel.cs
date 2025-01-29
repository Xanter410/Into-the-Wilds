using IntoTheWilds.Quest;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectiveHudModel
{
    private Label _descriptionLabel;
    private Label _amount;

    private IObjective _targetObjective;

    public ObjectiveHudModel(IObjective objective, VisualElement objectiveElementHud)
    {
        _targetObjective = objective;

        _descriptionLabel = objectiveElementHud.Q<Label>("ObjectiveDescription");
        _amount = objectiveElementHud.Q<Label>("ObjectiveAmount");

        _targetObjective.OnObjectiveAmountChanged += ObjectiveAmountChanged;

        _descriptionLabel.text = _targetObjective.Description;
        _amount.text = $" {_targetObjective.CurrentAmount} / {_targetObjective.TargetAmount}";
    }

    private void ObjectiveAmountChanged()
    {
        _amount.text = $" {_targetObjective.CurrentAmount} / {_targetObjective.TargetAmount}";
    }
}
