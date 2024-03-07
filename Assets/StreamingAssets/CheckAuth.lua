-- LANCET CYBER SECURITY (R) ALL RIGHTS RESERVED
-- @license UNLAWFUL USE OF THIS CODE MAY LEAD TO LITIGATION.
-- @name oAuth
-- @version 0.0.2

function OnTrigger(parent,other)
    -- FIXME URGENT: it is possible to add pixelcollider triggers in the same place to skip levels
    -- of security altogether. - sano
    if parent == 'coots' and other == 'portal' then
        Event.Invoke("oAuthClear")
    end
end