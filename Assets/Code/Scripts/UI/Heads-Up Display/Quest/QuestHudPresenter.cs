using IntoTheWilds;
using IntoTheWilds.Quest;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestHudPresenter : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _questPanel;
    [SerializeField] private VisualTreeAsset _objectiveElement;

    private UIDocument _uiDocument;
    private LevelManager _levelManager;

    private List<QuestHudModel> _questModelsHud = new();
    private List<VisualElement> _questPanels = new();

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _levelManager = GetComponent<LevelManager>();
    }

    private void Start()
    {
        List<IQuest> quests = _levelManager.QuestSystem.GetListActiveQuests();

        foreach (IQuest quest in quests)
        {
            VisualElement questPanel = _questPanel.CloneTree();

            QuestHudModel questHudModel = new QuestHudModel(quest, questPanel, _objectiveElement);

            questHudModel.OnQuestComplited += QuestComplited;

            _questModelsHud.Add(questHudModel);

            _questPanels.Add(questPanel);
        }

        AddListToVisualTree();
    }

    private void AddListToVisualTree()
    {
        GroupBox questBox = _uiDocument.rootVisualElement.Q<GroupBox>("QuestList");

        foreach (var quest in _questPanels)
        {
            questBox.Add(quest);
        }
    }

    private void QuestComplited(QuestHudModel questHudModel)
    {
        questHudModel._questPanel.AddToClassList("questComplite");
    }
}
