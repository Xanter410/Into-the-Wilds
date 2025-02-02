# Inventory System — Architecture

---

## Навигация

- [Главная / Overview](01_Overview.md)  
- **[Архитектура / Architecture](02_Architecture.md)**
- [Возможные Улучшения / Feature Requests](03_Feature_Requests.md)  

---

## Основные компоненты

### Данные:

- **ItemData <sup>ScriptableObject</sup>**  - информация о ресурсе, включая префаб, иконку, ID, максимальный стак и `ResourceType`
- **ItemsDatabase <sup>[Singleton](../UDF_Tools/01_Overview.md)</sup>** - централизированный доступ к информации о ресурсах по их ID

### Уровень Model:

- **ItemSlot <sup>C#</sup>** - модель отдельного слота, в котором может находиться какой-либо предмет
- **Inventory <sup>C#</sup>**  - модель инвентаря, включает список `ItemSlot` и управляет ими

### Уровень View:

- **Inventory <sup>uxml</sup>** - элемент UI Toolkit. Отображает инвентаря главного героя в формате Heads-Up Display
- **DraggableItem <sup>uxml</sup>** - представление перетаскиваемого объекта
    
### Уровень Presenter:

- **PlayerInventory <sup>C#</sup>** - объявление инвентаря главного героя, взаимодействие с `DragAndDropHandler` 
- **DropItemInstance <sup>MonoBehaviour</sup>** - представление `ItemSlot` в формате лежашего на земле предмета
- **InventoryHud <sup>MonoBehaviour</sup>** - трансляция данных из уровня *Model* в *View* 
- **DragAndDropHandler <sup>MonoBehaviour</sup>** - реализация Drag and Drop функционала

### Дополнительные компоненты:

- **DropItem <sup>C#</sup>** - описание отдельно взятого предмета для выпадения, включая минимальное и максимальное количество предметов
- **DropComponent <sup>MonoBehaviour</sup>** - определяет какие предметы и с каким шансом выпадут с игрового объекта
- **DropItemTrigger <sup>MonoBehaviour</sup>** - считывает и пытается взять лежащие предметы в инвентарь 
    
---

> [Далее: Возможные Улучшения](03_Feature_Requests.md)