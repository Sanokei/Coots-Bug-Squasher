function OnKeyDown(keycode)
    if keycode == 'A' then
        coots['trans'].move(-1, 0)
    end
    if keycode == 'D' then
        coots['trans'].move(1, 0)
    end
    if keycode == 'W' then
        coots['trans'].move(0, 1)
    end
    if keycode == 'S' then
        coots['trans'].move(0, -1)
    end
end