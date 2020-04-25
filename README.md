# CM0102Patcher v2.00

https://champman0102.co.uk/showthread.php?t=11661

## Downloads

https://github.com/nckstwrt/CM0102Patcher/releases

## Description

![alt text](https://i.imgur.com/oT112T3.png)

It also has a tools section with things like a patch applier that can apply .patch files from things like Flex 2 or patch files made with "fc /b cm0102_original.exe cm0102_patched.exe > newpatch.patch"

![alt text](https://i.imgur.com/QtmWs5O.png)

An unfinished scouter, based on CM Scout, but allows you to view and sort by the "intrinsic" skills value (can load compressed and uncompressed saves)

![alt text](https://i.imgur.com/w5E8SGO.png)

Also there is a little officials.dat patcher for patching up referees as per recent discussions on getting too many red cards from recent updates:

![alt text](https://i.imgur.com/6q6ltuS.png)

The RGN Image Converter:

![alt text](https://i.imgur.com/8vuKuwq.png)

Competition History Editor:

![alt text](https://i.imgur.com/n3RuoZg.png)

## Updates

v2.01:
* Added Competition History Editor
* Added extra Misc Patches (IgnorePlayerHistoriesOnLoad, Wales Patch, Remove4MonthLoanLimit)
* Protect against users trying to patch running executables

v2.00:
* Big Change: Patcher no longer changes the Date by modifying Data files - it does it all in the exe now (unless changing to before 2001)
* Big Change: Patcher now detects what patches have been applied and allows you to toggle them on and off
* Support for working on PlayOnMac
* Slight fix to the image cropping
* Alternative "Give More Options In Offers DropDown" Misc Patch
* Better support for starting before 2001 (mainly to work around Dec 27th '01 crash)
* Northern League patch now supports relegating teams as well as promoting them
* Scouter Copy To Clipboard (for pasting to Excel) now saves what's filtered rather than everything
* Save Game Changer now has option to Move a player to another Team
* Patcher now warns user if they are about to make a change that is "irreversible" (i.e. without restoring/reinstalling)
* Increase Number of Loans Allowed patch added to Misc Patches
* Show Private Bids Patch added to Misc Patches (in case you have applied Hide Private Bids previously in error)
* Added FixNetworkGames Patch to Misc Patches (This can be applied to Saturn v9 to make it work in Win95 more required for Network games)
* Save Scouter can now exclude those already about to be transferred in the filter
* Save Scouter font size increased
* Added No Foreign Player restrictions patches to Misc Patches

v1.27:
* Updated Save Game Changer to support setting all contract start dates back a year (stops all teams refusing to sell because they bought recently)
* Updated Save Game Changer to support setting the Current Reputation to a maxium of 5000 (stops all players being completely indispensable to the big clubs)
* Fixed minor bugs in the Scouter

v1.26:
* Prompts users to create a Restore Point before they apply changes. This way they should be able to revert by clicking Restore in case of issues.
* Correctly handles reverting back to 800x600 now

v1.25:
* New tool Save Game Changer - this can reduce stats in Saved Games that have run long and add in some of the special players from the original CM0102
* Minor fix in the image cropping code
* Handle changing the year to before 2000 better
* Scouter now uncaps the 0-20 limit, so if a player really has as stat at above 20, it will show what it would be
* Scouter now reads in some contract details

v1.24:
* Updated Misc Patches and added a few more descriptions
* Fixed RGN Converter when outputting to directories from Tools
* Increase font of Scouter grid
* Save Scouter can now filter by Scouter Rating

v1.23:
* Misc Patches improved and expanded (includes MadScientist's Creativity fix and other recently found patches)
* Fixed issue with stadium expansion patch
* Better checking for Tapani/Saturn .EXEs
* Lock the exe when patching (stops write errors)
* Welsh -> English suspensions on Northern Patch (from Saturn v3 patches)
* Added Attribute Masking Off By Default Patch
* More generic Euro Fix for 2019
* Test Support for Patching Process Memory rather than the exe itself
* Includes Inline Full Date Including History Patching. Not currently enabled.
* Fix for 2012 year change. Should fix setting to 2020.

v1.22:
* Option in Tools to remove Stadium Expansion limits
* Misc patches have Saturn v8 options and points deduction patch (Experts Only!)
* Tapani exe detection to make usage more foolproof for Saturn exes

v1.21:
* Resolution patching fixes
* "Page Down" patch in misc patches

v1.20:
* Fixed issue with Leap Year dates (meaning some DoBs were off by one). Thanks again MrFoo!
* Added playable China league (swaps out S.Korea)

v1.19:
* Better Image Error Handling for Mr.Foo.
* Better Patching (Handles /* */ to a degree)

v1.18:
* Support multiple resolutions (Beta: Not fully tested)
* Auto-fix Euros Crash when setting year to 2019
* Updated "Saturn Patches" to "Misc Patches" and added a few bits

v1.17
* Very minor fix to inflation currency patch (PUSH DWORD PTR SS:[ESP+4] issue)

v1.16:
* Idle Sensitivity now also works on transfer screen
* Ctrl+Shift+N to clear options
* More 7 Subs fixes
* Image Converter can now go RGN->PNG/BMP/JPG
* Experimental changes to make exe portable
* 3.9.60 patches added to Saturn patches
* NoCD for CM00/01
* Year Changer coded for CM00/01

v1.15:
* Fixes a crash when using the RGN Converter multiple times (Thanks MrFoo for letting me know!)

v1.14:
* Adds all the Saturn patches as options to add (Experts Only! Don't use unless you know what you're doing!)

v1.13:
* Lots of improvements to the Scouter
* Fixed jobs abroad boost
* Fixed issues when setting the Welsh Premier to be the Nation League South
* Manage background league teams fixed
* Can also hide Load Preset and recently used tactics when restricting tactics

v1.12:
* Option to stop loading of tactics and changing of wib-wob
* Change also stop scouters (like CMScout) working on your save games

v1.11:
* Added the National League South as a new option to replace the Welsh Premier

v1.10:
* Added the National League North (replaces Welsh Premier)
* Added the Southern Premier Division (replaces Welsh Premier). If using 2018/19 update creates league with current teams.
* Added auto cropping to keep aspect ratio when going 800x600 -> 1280x800
* Added cropping to RGN conversion tools

v1.09:
* Added option to remove 3 foreign player restriction
* EEC Hack supports uncompressed save game files too
* Added "Restore Points" to save Exe, Data and Pictures

v1.08:
* Fixed the World Cup history being on odd years (didn't affect future world cup dates/years)
* Fixed the League Selection text (would say England 18/09 - was just a cosmetic bug)
* Save Scouter improved and fixed. Can now filter on attributes.

v1.07:
* Fix for Euro.cfg when updating Holland->Netherlands
* Scouter now has presets for columns selection
* Removed Player Search Finds All Players and changed with Manage Any Team

v1.06:
* Fixes for player_setup.cfg (which affected Loans and Injuries)
* Fix to workaround any bad truncation in history data files
* Improvements to the Scouter

v1.05:
* Transfer Window Patch (from Saturn's patches) added to "Update names"
* New RGN Image converter in Tools (BMP/JPG/PNG/RGN -> RGN with resizing)
* Inflation Multiplier can now be less than one

v1.04:
* Tapani's new regen code can now be added (preferred for long games)
* Sped up the RGN converting
* Option to update names (Beta! - Currently does English leagues, Europa and Champs League)
* 7 Subs for French
* Intrinsic Scouter updated (MadScientist's filter code)

v1.03:
* Added back the 1280x800 resolution patch! Now working and will resize all your 800x600 images to 1280x800 - so any graphics pack that works with 800x600 can automatically convert to working in 1280x800!
* CM Scout inspired Save Scouter which also shows Intrinsic values - yet to be finished! So very Alpha!

v1.02:
* History (players, competitions, etc) is also advanced in time when changing the start year
* Removed the 1280x800 resolution patch option until I can get it fully working
* Added option to boost the chances when applying for jobs abroad
