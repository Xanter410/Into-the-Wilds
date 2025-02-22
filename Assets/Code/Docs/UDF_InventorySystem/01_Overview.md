﻿# Inventory System — Overview

---

## Навигация

- **[Главная / Overview](01_Overview.md)**
- [Архитектура / Architecture](02_Architecture.md)
- [Возможные Улучшения / Feature Requests](03_Feature_Requests.md)  

---

## Описание

Система инвентаря отвечает за взаимодействие, хранения и учет любых подбираемых главным героем ресурсов в игре.

---

## Основные возможности

- Создание предметов в формате Scriptable Objects, настройка всех параметров через Inspector
- Реализация инвентаря главного героя
- Поддержка функционала Drag and Drop
- Настройка выпадения предметов из Противников/Объектов.

---

## Принцип работы

1. Каждый экземпляр предмет представляет из себя объект `ItemSlot` который хранит индентификатор, текущее и максимальное количество элементов в слоте.
2. `ItemSlot` может быть представлен в игре как отдельно существующий предмет, который игрок может подобрать. Также он может быть представлен как отдельный слот в инветаре игрока.
3. Главный герой может подбирать предметы с земли, если для них есть доступное место в инвентаре. Может перекладывать предметы внутри инвентаря и выкидывать их из него.

---

> [Далее: Архитектура системы](02_Architecture.md)
