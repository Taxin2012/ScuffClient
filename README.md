# ScuffClient
THIS PROJECT IS DEFUNCT AS OF MAY 2020 DUE TO INFRASTRUCTURE CHANGES BY THE VRCHAT DEV TEAM
See https://github.com/sebrandt1/CrownLoader for an up to date VRChat cheat client.


A VRChat cheat client with functionality such as:


#Cheats

-Flying.

-NoClipping.

-Speed Modifier.

-Teleport to other players.

-Teleport to mouse (Invisible Ray is emitted from screen center and upon clicking it teleports you to whatever object the ray collides with).


#Misc

-Logging out other players (Patched late 2019).

--Logout exploit worked by sending 20,000 bytes repeatedly to target player through the photon networking system.
-Crash target players game client.

--Crash worked by sending an empty byte array to the target player repeatedly which would force throw an IndexOutOfRangeException on their game client. The data was sent through the voice chat system, when the voice chat data would deserialize on the target players client it would start on index 4 in the byte array. This meant that any byte array less than 4 bytes in length would forcefully throw the IndexOutOfRangeException.

-Save and load any avatar through the avatars ID on the VRChat API.

-Trigger any map events for all players currently on that map. (i.e events that toggle objects on or off, events that teleport everyone to a specific location).

-See players that blocked your and display their name with a red nametag (Normally you can't see players who've blocked you).

-Join private worlds that you normally wouldn't be able to join.

-Delete portals dropped by other players.

-Prevent the client user from entering portals without turning on a toggle (Safety measure because other client users were able to drop portals on top of other players).

-Drop portals on other players, forcing them to join a different world.

-Hardware ID randomizer on every HWID request from the server (This was to prevent Hardware ID bans from moderators).


#Other
On every patch released by the developers, their type names, method names and variable names would all be randomized.

To circumvent this I've used Reflections to find the proper types, methods and variables used in the client by searching for them through other means such as parameter lengths, parameter types, return types, get & set identifiers.....(and so on).

This would allow my client to work independently of the names on classes, methods etc. (No need to manually look for the new names in decompiler programs like DnSpy).
