-- LANCET CYBER SECURITY (R) ALL RIGHTS RESERVED
-- @license UNLAWFUL USE OF THIS CODE MAY LEAD TO LITIGATION.
-- @name Piston Door
-- @version 0.3.1

-- WARNING:
---     """
---      only Master version of the product has password protection! - Plete
---      > function Open(password)
---     """
---     STOP REUSING CODE TO MAKE SURE MISTAKES ABOVE DO NOT HAPPEN.
-- - MANAGEMENT

-- copy and pasting is literally my job - Sano
-- ^ ditto - Plete
function Open()
    -- FIXME: This is not a good way to do behaviour changes. 
    -- decouple it in future iterations using string replace. - sano
    door['door'].add(
        [[
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ooooofoooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        oooooooooooo
        ]]
    )
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
        ]],true
    )

end

-- NOTE: adding just replaces the last sprite/collider, this is undefined behaviour.
function Close()
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
end