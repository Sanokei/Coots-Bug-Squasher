-- Create pixelGameObjects 
function AwakeGame()
    jargon.add('player')
    jargon.add('floor')
end
-- Add pixelComponents to the objects
function InitializeGame()
    -- Add player
    player.add('trans','PixelTransform')
    player.add('rb', 'PixelRigidBody') -- add gravity
    player.add('pc','PixelCollider')
    player['pc'].add(
        [[
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
        xooooooooooo
        xooooooooooo
        ]]
    )
    player.add('still', 'PixelSprite')
    player['still'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        rooooooooooo
        cooooooooooo
        oooooooooooo
        ]]
    )
    -- Auto adds the pixelGameObject its on to the globals
    player.add('PlayerMovement', 'PixelBehaviourScript').add
    (
        [[
        function OnKeyDown(keydown)
            if(keydown == 'd')
                player['trans'].move(0,1)
            end
            if(keydown == 'a')
                player['trans'].move(0,-1)
            end
        end
        ]]
        
    )

    -- Add Floor
    floor.add('pc','PixelCollider')
    floor['pc'].add(
        [[
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
        oooooooooooo
        xxxxxxxxxxxx
        ]]
    )
    floor.add('floor', 'PixelSprite')
    floor['floor'].add(
        [[
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
        oooooooooooo
        cccccccccccc
        ]]
    )
end
-- add references to scripts that need it and run scripts
function StartGame()
    PlayerMovement.addPixelGameObjectToScriptGlobals('jargon',jargon)
    PlayerMovement.RunScript()
end