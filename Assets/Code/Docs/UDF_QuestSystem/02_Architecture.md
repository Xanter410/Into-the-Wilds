# Quest System — Architecture

---

## Навигация

- [Главная / Overview](01_Overview.md)  
- **[Архитектура / Architecture](02_Architecture.md)**
- [Возможные Улучшения / Feature Requests](03_Feature_Requests.md)  

---

## Основные компоненты

### Данные:

- **QuestData <sup>ScriptableObject</sup>**  - информация о квесте, в том числе список целей `ObjectiveData`
- **QuestFactory <sup>C#</sup>**  - статическая фабрика по созданию экземпляров квестов из данных

### Уровень Model:

- **QuestBase <sup>C#</sup>** - базовая реализация квеста. Интерфейс взаимодействия определен в `IQuest`
- **ObjectiveBase <sup>C#</sup>**  - базовая реализация отдельной цели квеста. Интерфейс взаимодействия определен в `IObjective`
- **QuestSystem <sup>MonoBehaviour</sup>** - перевод квестов из ожидания в активные, обновление целей активных квестов
- Реализация специализированных видов целей:
    - **CollectObjective <sup>C#</sup>** - сбор предметов определенного типа
    - **FindObjective <sup>C#</sup>** - поиск определенной точки на уровне
    - **KillObjective <sup>C#</sup>** - цель по убийству противников

### Уровень View:

- **ObjectiveElement <sup>uxml</sup>** - цель квеста
- **QuestPanel <sup>uxml</sup>** - панель квеста, включающая название, описание и цели
- **QuestGrid <sup>uxml</sup>** - сетка активных квестов
    
### Уровень Presenter:

- **QuestHudModel <sup>C#</sup>** - модель компонента Hud. Трансляция данных из уровня *Model* в *View*
- **ObjectiveHudModel <sup>C#</sup>** - модель компонента Hud. Отслеживание обновления целей из уровня *Model*
- **QuestHudPresenter <sup>MonoBehaviour</sup>** - трансляция активные квесты из QuestSystem в *View*, объявляя экземпляры QuestHudModel

### Дополнительные компоненты:

- **GameEventQuestProgress <sup>[GameEvent](../UDF_Tools/01_Overview.md)</sup>**  - событие на основе ScriptableObject, для отслеживания прогресса квестов. 
- **QuestProgressComponent <sup>MonoBehaviour</sup>** - считывание прогресса определенных типов квестов (Find, Kill)
    
---

>  [Далее: Возможные Улучшения](03_Feature_Requests.md)