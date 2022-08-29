# HumanLevel

Dialogue Manager: change implementation of ReadNext() from coroutine to void method. 
There is no need for coroutine. It might have been used to fix some bugs in the older version which no longer exists.

Game Event: change onFreezeMovement and onRestoreMovement to onOpenUI and onCloseUI which are called by DialogueManager, 
NarrativeManager(similar to DialogueManager but without speaker) and InventoryUI.

To implement the UI, simply drag UI and GameManager, and Kizuna from Prefab into the hierarchy. (Kizuna is needed as the method FreezeMovement() in Kizuna is subscribed to onOpenUI() in GameEvent.)
