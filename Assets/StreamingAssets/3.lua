-- Create pixelGameObjects 
function AwakeGame()
    jargon.add('coots')
    jargon.add('wall')
end
-- Add pixelComponents to the objects
function InitializeGame()
    -- Add coots
    coots.add('trans','PixelTransform')
    coots.add('still', 'PixelSprite')
    coots['still'].add(
        [[
        oooooooooooo
        ocoooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
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
        oxoooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
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

    wall.add('wall', 'PixelSprite')
    wall['wall'].add(
        [[
            oooooooooooo
            owwwwoowwwwo
            woooowwoooow
            woooooooooow
            woooooooooow
            wwooooooooww
            owoooooooowo
            oowoooooowoo
            oowwoooowwoo
            oooowoowoooo
            ooooowwooooo
            oooooooooooo
        ]]
    )
    -- wall.add('pc','PixelCollider')
    -- wall['pc'].add(
    --     [[
            -- explore outside ur cage lol
    --     ]]
    -- )
end
-- add references to scripts that need it and run scripts
function StartGame()
    coots['MoveCoots'].RunScript()
end