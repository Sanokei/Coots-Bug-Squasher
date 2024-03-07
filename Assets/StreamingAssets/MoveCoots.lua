-- Sano plus Leni Co. (R) 
-- @license use as you wish
-- @name Checkpoint
-- @version 0.0.1

-- Lua doesnt have switch cases. how silly :p - Leni
function OnKeyDown(keycode)
    if keycode == 'LeftArrow' then
        coots['trans'].move(-1, 0) -- FIXME: relies too hard on global names. - Sano
        -- lighten up Sano! this is meant to be fun :3 - Leni
    end
    if keycode == 'RightArrow' then
        coots['trans'].move(1, 0)
    end
    if keycode == 'UpArrow' then
        coots['trans'].move(0, 1)
    end
    if keycode == 'DownArrow' then
        coots['trans'].move(0, -1)
    end
end