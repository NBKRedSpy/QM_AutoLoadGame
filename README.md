# Quasimorph Auto Load Save

![slot loader icon](media/SlotLoader.png)

When loading the game, the mod will automatically load the last game loaded after three seconds.
The delay is configurable.

The auto load can be aborted by the following:
* Holding or pressing the shift key when the main menu is shown.
* Entering a menu such as options or start

The mod can be disabled via the config so users do not have to unsubscribe from the mod to disable.

This mod is particularly useful for modders which reload the the same save many, many times when debugging.

# Configuration

The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_AutoLoadGame\QM_AutoLoadGame.json`.  

|Name|Default|Description|
|--|--|--|
|Disable|false|An option to disable the mod without needing to unsubscribe in the Workshop|
|CountDownSeconds|3|How many seconds will elapse before the auto load occurs|
|LastLoadedSlot||Ignore this item.  It is the storage for the last slot that was loaded|

# Support
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
Thanks!

# Source Code
Source code is available on GitHub at https://github.com/NBKRedSpy/QM_AutoLoadGame

# Credits:
[Loader icons created by Freepik - Flaticon](https://www.flaticon.com/free-icons/loader)

# Change Log

## 1.3.1
* Fixed loading wrong slot (was always the first slot)
* Fixed the user clicking the screen not aborting the auto load.

## 1.3.0
* Version 0.8.5 compatibility
## 1.1.0
* Version 0.8 compatibility
