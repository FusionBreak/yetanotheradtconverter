# YetAnotherAdtConverter
Convert [.adt files](https://wowdev.wiki/ADT/v18) from 335a (for example from Noggit) up to Legion

#### Requirements: ####
*  [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

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
GroundEffectsAdding | False | Takes the information for the textures on which to add Groundeffects from GroundEffects.ini (if the file does not exist, it will be created instead) and adds it. The original .adt will not be changed. For more see "Ground effects"...
ReCalculateGroundEffectsMap|False|Fixes bugs with Groundeffects on some old maps (Noggit). *((Original by Mjollna.))*

#### Ground effects: ####
If GroundEffects.ini does not exist, it will be created the first time the converter is started. (If the config "GroundEffectsAdding" is set to "True"!)

To add ground effects, the entries in GroundEffects.ini must have the following formatting: 

```TexturePath=ID```

Example: 
```
tileset/tanaris/tanarissandbase02.blp=99999  
tileset\expansion02\boreantundra\bt_mossyd.blp=666666
```

**The TexturePath corresponds to Noggit or Casc etc.**

**The ID must be taken from the GroundEffectTexture.db2**. All entries are separated by a line break. (No commas, semicolons or other crazy stuff you would expect now!)

For more see [here](http://www.modcraft.io/index.php?topic=2357.0)...

#### Upcoming plans ####

- [x] Inserting Groundeffects


** 🛑 The further development for this project was stopped by me. 🛑 **
