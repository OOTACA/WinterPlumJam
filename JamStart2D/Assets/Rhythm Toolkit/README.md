This asset contains:
drywetmidi by Melanchall: https://github.com/melanchall/drywetmidi
code from tutorial by SkyanSam: https://www.youtube.com/watch?v=ev0HsmgLScg
keyboard/gamepad button icons by Ansdor: https://ansdor.itch.io/button-icons
Thank you!

----------------------------------------------


SONG MANAGER PARAMETERS:
Audio Source: Audio Source component with music
Lanes: objects with Lane script (any quantity)
Midi Location: path to your beat MIDI file
Input Delay In Milliseconds: input error
Error Margin In Seconds: amount of time when miss is not considered as miss
Note Time: note movement speed
Note Spawn Y: Y axial point in which note spawns
Note Tap Line: line object where note button has to be pressed
Error Deviation: amount of time when miss is considered as half-hit
Start Song Delay: delay before music start playing (mind to use it for VFX)
Delay After Music Stop: delay after music stops
Background SR: background sprite renderer for VFX
Tap Line SR: tap line sprite renderer for VFX
Fade Duration: duration of background and tap line fading in & fading out
Rhythm Play Tag: tag of rhythm play object (used for turning off the system after playing)

Events:
OnPlayStart(): on script Song Manager start
OnPlayStop(): on song end
----------------------------------------------
SCORE MANAGER:
Handle the player input: note hit, note half-hit, note missed
----------------------------------------------
AUDIO SOURCE:
Music to play
----------------------------------------------
LANES:
Plugin supports any quantity of lanes and playable notes
----------------------------------------------
BACKGROUND & TAP LINE:
Graphic content
----------------------------------------------
PREFABS:
Player prefab with player input
RhythmPlay prefab with rhythm object: just drag'n'drop it to your scene
4 default notes for 4 buttons (keyboard & gamepad): you can change it or create new prefabs (do not forget the Note script)
This plugin supports immediate 