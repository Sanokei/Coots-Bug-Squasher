-- Create pixelGameObjects 
function AwakeGame()
    jargon.add('coots')
    jargon.add('wall')
    jargon.add('goal')
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
    coots.add('MoveCoots', 'PixelBehaviourScript').addFile("MoveCoots.lua")
    -- Add wall
    wall.add('wall', 'PixelSprite')
    wall['wall'].add(
        [[
        wwwwwwwwwwww
        woooooooooow
        woooooooooow
        woooooooooow
        woooooooooow
        woooooooooow
        woooooooooow
        woooooooooow
        woooooooooow
        woooooooooow
        woooooooooow
        wwwwwwwwwwww
        ]]
    )
    wall.add('pc','PixelCollider')
    wall['pc'].add(
        [[
        xxxxxxxxxxxx
        xoooooooooox
        xoooooooooox
        xoooooooooox
        xoooooooooox
        xoooooooooox
        xoooooooooox
        xoooooooooox
        xoooooooooox
        xoooooooooox
        xoooooooooox
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
end
-- add references to scripts that need it and run scripts
function StartGame()
    coots['MoveCoots'].RunScript();
end

function TriggerEvent()
    
end