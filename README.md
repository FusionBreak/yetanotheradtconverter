# YetAnotherAdtConverter
Convert [.adt files](https://wowdev.wiki/ADT/v18) from 335a (for example from Noggit) up to Legion

#### Requirements: ####
*  [.NET Framework 4.7.2](https://dotnet.microsoft.com/download)

#### Usage: ####
* Put the .adt files into the "Input" folder. If the Input folder does not exist, it will be created at first execution.
* Run the YetAnotherAdtConverter.exe.
* Wait for the "done..." message. The window closes automatically.
* Get the finished .adt from the "Output" folder. If it does not exist, it will be created.

#### Configs: ####
The "config.ini" can be used to set the following settings:

Config | Default | Description
------------ | ------------- | -------------
LogFile | False | Should the console output be saved to a file? (log.txt)
Input | .\Input | Directory in which the 335a files are read.
Output | .\Output | Directory where the files are written.
CreateMFBO | True | Should a MFBO chunk be created if the input has none? (This is not really necessary, but better include more details than too few, huh?)
BoundingSize | 100 | Used to see objects from a defined distance based on the radius. The larger the value, the further objects (.wmo / .m2) can be seen. If there are too many objects, performance may be impaired.
BoundingRadius | 200 | -//-
DynamicBoundingGeneration | True | Automatically adjusts the BoundingSize and BoundingRadius based on the specified values to the number of objects in the .adt. This should increase the performance of the game. **[** *boundingSize = specifiedValue - ((NumberOfObjects - 1000) / 10)* **]**

#### Upcoming plans ####

- [ ] Inserting Groundeffects
- [ ] .wdl, .tex, _fogs.wdt, _lgt.wdt, _occ.wdt creation
- [ ] BFA 8.0.1 support *(A comparison between the files from 7.2.5 and 8.0.1 showed that the .tex might have changed)*
