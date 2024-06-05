This is a simple tap-and-blast game in progress.

Game Basics:
- A blast occurs when at least 2 adjacent matching cubes or a TNT is tapped.
- New random cubes fall from atop the grid upon blast.
- There are four types (colors) of Cubes.
- TNT are created when the blasted cube group count is bigger than or equal to 5 and will explode in a 5x5 area.
- Obstacles are goals of levels. Users should clear all obstacles to win the level within a given move count. There are three types of obstacles. The level will be won if all obstacles are cleared within a given move count.

Notes:

Please start the game from the scene labeled as "StartScene" and make sure that GameObject "GameManager" is active in scene before doing so.

Couple of current bugs that the user should be aware of:

1) To progress to the next level, user should restart application after winning.

2) User should attempt another blast after victory move for victory to be recognized.

Victory and level progression are possible, given that they're performed as instructed above.
