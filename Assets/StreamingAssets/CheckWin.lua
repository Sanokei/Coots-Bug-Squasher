function OnTrigger(parent,other)
    if parent == 'coots' and other == 'goal' then
        Event.Invoke("win")
    end
end