# TurnBased
game made on unity


1. transition duration  and transition offset allow for smooth blending of different animations
2. going to Edit-> ScriptExecutionOrder we can change the order of execution of script so when we need a script that is crucial to be run before default time we just drag it
3. Events have publishers and subscribers where publisher is not aware of what is subscribed to it. debloats code and is ex: of observer pattern
![alt text](image.png)
4. Prefabs cannot reference objects in the scene for which reason we can't use [SerializeField] on prefabs but will have to use singleton pattern instead 
to reference out Event action
5. need to invest more time learning events and the singleton pattern
6. references to different scripts made inside classes to make refactoring code easy

![2025-08-26 22-37-21](https://github.com/user-attachments/assets/da1e27f4-25ba-40d0-a739-1637de7968ad)
