# Character System — Architecture

---

## Навигация

- [Главная / Overview](01_Overview.md)  
- **[Архитектура / Architecture](02_Architecture.md)**
- [Возможные Улучшения / Feature Requests](03_Feature_Requests.md)  

---

## Основные компоненты

Далее описываются компоненты для персонажа главного героя.  
Для создания NPC используется аналогичный набор компонентов.

### Внедрение зависимостей (DI):

- **PlayerLifetimeScope <sup>VContaiter.LifetimeScope</sup>** - регистрация компонентов и точек входа

### Уровень Model:

#### StateMachine

- **PlayerStateMachine <sup>[StateMachine](../UDF_Tools/01_Overview.md)</sup>** - хранит ссылки на возможные состояния, передает управление активному состоянию

> Каждое состояние само отслеживает данные ввода и переключает на соответсвующее новое состояние.  
 Исключением является `...DeadState`, в данном случае оно отслеживает событие `OnDead` из HealthComponent.
 
- **PlayerIdleState <sup>IState</sup>** - состояние бездействия
- **PlayerMoveState <sup>IState</sup>** - состояние движения. Задает движение через `Rigidbody2D.linearVelocity` 
- **PlayerAttackState <sup>IState</sup>** - состояние атаки
- **PlayerDeadState <sup>IState</sup>** - состояние смерти. Выключает систему ввода,  
  фактически останавливая дальнейшие изменения состояний

#### AI

- **GoblinTorchAI <sup>[BehaviorTree](../UDF_Tools/01_Overview.md)</sup>** - Дерево поведения для отдельно взятого противника
- Список универсальных узлов для BehaviorTree:
    - **AI_Attack <sup>Node</sup>** - устанавливает ввод аттаки
    - **AI_MoveToTarget <sup>Node</sup>** - устанавливает ввод движения, работает в связке с системой `Pathfinding` 
    - **AI_CheckDistanceToSpawnPoint <sup>Node</sup>** - проверяет что персонаж не ушел от точки появления дальше положенного
    - **AI_FindPlayerInRange <sup>Node</sup>** - поиск главного героя в указанном радиусе от персонажа
    - **AI_CheckPlayerInRange <sup>Node</sup>** - проверяет что главный герой находится в определенном радиусе от персонажа
    - **AI_ClearTrigger <sup>Node</sup>** - удаляет данные о таргете (например, если он вышел из радиуса агрессии)
    - **AI_FindAndCheckPlayerInRadius <sup>Node</sup>** - Объединение AI_FindPlayerInRange, AI_CheckPlayerInRange, AI_ClearTrigger
    - **AI_IdleWalkNearSpawn <sup>Node</sup>** - базовая реализация бездействия персонажа. Ходит в некотором радиусе вокруг спавна,  
      устанавливает ввод движения, работает в связке с системой `Pathfinding` 

> Для отдельных персонажей могут быть созданы уникальные узлы, для реализации уникальных механик

- **SheepAI_EscapeTask <sup>Node</sup>** - устанавливает ввод движения,  
  в противоположном направлении от главного героя (убегая от него)
- **SheepAI_IdleTask <sup>Node</sup>** - устанавливает ввод движения,  
  во время бездействия ходит в некотором радиусе от текущего положения (а не вокруг спавна)

### Уровень View:

- Встроенный функционал Unity:
    - **SpriteRenderer** - отображение активного спрайта / его отрожение / его материал
    - **Animator** - управление анимациями объекта 
      (на деле просто устанавливается анимация соответствующая активному состоянию `PlayerStateMachine`)
    - **Rigidbody** - основа для взаимодействия персонажа с миром, движение, коллизии и т.д.
    
### Уровень Presenter:

- **UnitSpriteRenderer <sup>MonoBehaviour</sup>** - устанавливает отрожение спрайта в зависимости от направления движения,  
  чтобы спрайт смотрел в правильном направлении.
- **PlayerAnimatorRenderer <sup>MonoBehaviour</sup>** - обновляет параметры Animator в зависимости от смены состояний в `PlayerStateMachine` 
- **PlayerInput <sup>ScriptableObject</sup>** - информация о вводе игрока

### Дополнительные компоненты:

- **AttackTrigger <sup>MonoBehaviour</sup>** - триггер активирующийся в момент аттаки,  
  проверяет коллизию с игровыми объектами с `HealthComponent` и наносит урон.
- **DynamicSortingOrder <sup>MonoBehaviour</sup>** - устанавливает динамическую сортировку спрайта,  
  чтобы он правильно отображался в условия 2D Top-down графики
- **HealthComponent <sup>MonoBehaviour</sup>** - реализация очков здоровья для любого персонажа/объекта, которому может быть нанесен урон.
- **HitAndStunHandler <sup>MonoBehaviour</sup>** - реализация импакта от боевой системы.  
  Реализует ответную реацию в виде мерцания и короткого оглушения цели при получении урона.
    
---

> [Далее: Возможные Улучшения](03_Feature_Requests.md)