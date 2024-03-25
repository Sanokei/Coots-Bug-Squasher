# Sneak Game Documentation
There only a handful of commands for the example.
The example is the backbone of  [Coot's: Bug Squasher](https://github.com/Sanokei/Coots-Bug-Squasher)

## Methods and Functions
### add(string)
Every derivitive of `PixelComponent` has an add method. It is used to create new components on `PixelGameObjects`. The add method will return the appropriate `PixelComponent`.
### run(string,string)
`.run(functionName, arg0, arg1, arg2, arg3, arg4, arg5)`The command can have upto 6 string arguments, but because of how auto function conversion works for Lua, I had to create a way for the code to throw an exception when a function had too many or too few arguments. 
## PixelBehaviourScripts
### RunScript()
Makes sure the script is active when the game is completed compiling. Runs the code once.
### addFile(string)
adds a lua file to the cache for runtime instead of having to rewrite the behaviour.
### Events
___
### Start()
First event that is run in the `PixelBehaviourScript`
### Update()
Loop that runs every game tick according to FixedUpdate
### OnCollision()
Runs when the collision system detects a collision. Will not run if the `other` is a trigger
### OnTrigger()
Runs when the collision system detects a collision and its a trigger.
### OnKeyDown(string)
Triggered when AlphaJargon detects a keydown event.

# AlphaJargon Documentation
## Methods and Functions
## add(string)
Not to be mistaken with the `.add()` method found in `PixelGameObjects` to add `PixelGameObjects`. The `add()` method is attached to Jargon to add `PixelGameObject` to the game itself. The names must be unique.
## Events
### AwakeGame()
This is the outer Awake function for when you want to make a game with AlphaJargon. You add new `PixelGameObjects` here
### StartGame()
Behaviour to the `PixelGameObjects` are added here
### InitializeGame()
Finally scripts on `PixelGameObjects` are ran here. By design, I made running scripts optional, but I never ended up using the option, opting to always have them run. I may change this to the inverse later.