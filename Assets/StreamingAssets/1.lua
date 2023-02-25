-- Create pixelGameObjects 
function AwakeGame()
    jargon.add('coots')
    jargon.add('wall')
    jargon.add('goal')
    jargon.add('door')
end
-- Add pixelComponents to the objects
function InitializeGame()
    -- Add coots
    coots.add('trans','PixelTransform')
    coots.add('still', 'PixelSprite')
    coots['still'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ocoooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )
    coots.add('pc','PixelCollider') -- needs a pixelsprite to work
    coots['pc'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oxoooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )
    -- Auto adds the pixelGameObject its on to the globals
    coots.add('cootsMovement', 'PixelBehaviourScript').addFile("MoveCoots.lua")
    -- Add wall
    wall.add('wall', 'PixelSprite')
    wall['wall'].add(
        [[
        wwwwwwwwwwww
        woooowooooow
        woooowooooow
        woooowooooow
        woooooooooow
        woooowooooow
        woooowooooow
        woooowooooow
        woooowooooow
        woooowooooow
        woooowooooow
        wwwwwwwwwwww
        ]]
    )
    wall.add('pc','PixelCollider')
    wall['pc'].add(
        [[
        xxxxxxxxxxxx
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xoooooooooox
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xxxxxxxxxxxx
        ]]
    )

    goal.add('pc','PixelCollider') -- needs a pixelsprite to work
    goal['pc'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ooooooooooxo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
        , true
    )
    goal.add('still', 'PixelSprite')
    goal['still'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooobo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )

    door.add('door', 'PixelSprite')
    door['door'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ooooodoooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )

    door.add('pc','PixelCollider')
    door['pc'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooxoooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )
    
    door.add('doorBehaviour', 'PixelBehaviourScript').addFile("DoorBehave.lua")
end
-- add references to scripts that need it and run scripts
function StartGame()
    coots['cootsMovement'].RunScript();
end

-- [[
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- oooooooooooo
-- ]]