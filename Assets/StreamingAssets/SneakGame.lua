-- Create pixelGameObjects 
function AwakeGame()
    jargon.add('player')
    jargon.add('floor')
end
-- Add pixelComponents to the objects
function InitializeGame()
    -- Add player
    player.add('rb', 'PixelRigidBody') -- add gravity
    player.add('trans','PixelTransform')
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
    player.add('pc','PixelCollider') -- needs a pixelsprite to work
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
        xooooooooooo
        xooooooooooo
        oooooooooooo
        ]]
    )
    -- Auto adds the pixelGameObject its on to the globals
    player.add('PlayerMovement', 'PixelBehaviourScript').add(
        [[
        function Start()
            print('start')
        end
        function OnKeyDown(keycode)
            if keycode == 'A' then
                player['trans'].move(-1, 0)
            end
            if keycode == 'D' then
                player['trans'].move(1, 0)
            end
            if keycode == 'W' then
                player['trans'].move(0, 1)
            end
            if keycode == 'S' then
                player['trans'].move(0, -1)
            end
        end
        ]]
        
    )
    -- Add Floor
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
        crcrcrcrcrcr
        ]]
    )
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
end
-- add references to scripts that need it and run scripts
function StartGame()
end