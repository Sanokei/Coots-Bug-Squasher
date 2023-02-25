function OnKeyDown(keycode)
    if keycode == 'LeftArrow' then
        coots['trans'].move(-1, 0)
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