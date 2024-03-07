-- Create pixelGameObjects 
function AwakeGame()
    jargon.add('coots')
    jargon.add('wall')
    jargon.add('portal')

    -- Airlock system
    jargon.add('airlock')
    jargon.add('door1')
    jargon.add('door2')
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
        woooowooooow
        woooowooooow
        woooowwwooow
        woooooooooow
        woooowwwooow
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
        xooooxxxooox
        xoooooooooox
        xooooxxxooox
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xooooxooooox
        xxxxxxxxxxxx
        ]]
    )

    portal.add('pc','PixelCollider') -- needs a pixelsprite to work
    portal['pc'].add(
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
    portal.add('ps', 'PixelSprite')
    portal['ps'].add(
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
    portal.add('CheckAuth', 'PixelBehaviourScript').addFile("CheckAuth.lua")
    
    -- Doors
    door1.add('door', 'PixelSprite')
    door1['door'].add(
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

    door1.add('pc','PixelCollider')
    door1['pc'].add(
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

    door2.add('door', 'PixelSprite')
    door2['door'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ooooooodoooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )
    
    door2.add('pc','PixelCollider')
    door2['pc'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooxoooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )
    
    airlock.add('AirlockBehave', 'PixelBehaviourScript').addFile("AirlockBehave.lua")
end
-- add references to scripts that need it and run scripts
function StartGame()
    coots['MoveCoots'].RunScript()
    portal['CheckAuth'].RunScript()
    airlock['AirlockBehave'].RunScript()
    -- Bad solution
    airlock['AirlockBehave'].addPixelGameObjectToScriptGlobals("door1")
    airlock['AirlockBehave'].addPixelGameObjectToScriptGlobals("door2")
end