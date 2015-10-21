# Touch controller for jubeat analyser
![screenshot](https://raw.githubusercontent.com/sinusinu/JATouchController/master/sshot.png)<br/>
[Download Release Build](https://github.com/sinusinu/JATouchController/releases/download/1.1/JATouchController.zip)
## Overview

Touch controller for jubeat analyser is a standalone app that converts touchscreen input into corresponding key input for jubeat analyser.

This application does not modify or hack anything, but just transparently converts user input and send it to jubeat analyser, having minimum impact on performance or game experience.

Requires Windows 8 or later with Touch input device(Windows 7 is not supported).<br/>
Tested and confirmed working on my 8" Windows 10 tablet, but I cannot guarantee that it would work on yours too.

## How to use

1. Launch jubeat analyser and this app together
2. Start jubeat analyser as 4x4 mode (title will say 'music select')
3. Adjust position and size of this app to match 4x4 grid on the jubeat analyser
4. Tap 'Search for jubeat analyser' (if the title of the jubeat analyser is not a 'music select', then change the text in textbox to the current title)
5. Now you can play jubeat analyser with touch!

When you Tap the Close button on the top-right of the window, the grid will become red.<br/>
When the grid is red, Tapping the Close button will send ESC key(to stop playing current song) to jubeat analyser. This will make the grid invisible.<br/>
When the grid is invisible, Tapping the Close button will shut down the touch controller.

(Close button: 1x=Nothing, 2x=ESC, 3x=Close)

## License

Touch controller for jubeat analyser is distributed under the terms of the Do What The Fuck You Want To Public License, Version 2.