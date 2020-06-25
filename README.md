# Keil Map Tools

Hi, the question is:
# Keil Map Tools

Hi, the question is:

- Are you working with Keil Compiler?
- Are you working with Keil RTX Operating System?

If yes, this tool could be useful for you!

## .Keil Map Viewer

Keil Map Viewer has some killer features:

- Organize Map file in readable, orderable and useful view
- Filter real-time by searching text pattern in each single field
- Auto reload map file data

To integrate it in Keil compiler it&#39;s enough to add as external tools

![](https://github.com/undici77/KeilMapTools/blob/master/Images/KeilMapViewer.png)

## .Keil Rtx Stack Size

If you use Keil RTX Operating System and STM32 G0, F1 or F4, this tool can calculate the stack usage for each thread.

It&#39;s enough to follow to add it in post build and enable --info=stack feature in linker options.

Command line and parameters are:

- --help produce help message
- --debug enable debug info
- --ini set ini configuration file/folder
- --map set map file
- --out set output file/folder
- --arg set architecture
- --regex set thread search regex
- --oversize set thread stack oversizing

![](https://github.com/undici77/KeilMapTools/blob/master/Images/KeilRtxStackSize.png)

## .Integration

*Linker Options*
![](https://github.com/undici77/KeilMapTools/blob/master/Images/KeilLinkerOptions.png)

*Post Build Options*
![](https://github.com/undici77/KeilMapTools/blob/master/Images/KeilProjectOptions.png)

*Tool Menu Options*
![](https://github.com/undici77/KeilMapTools/blob/master/Images/KeilExternalTools.png)

## .KeilMapTools.ini
```
[Architecture]
Name=STM32
[Map]
FilePath=EVSE\_KIT/Application.map
[Output]
FilePath=Src\os\_threads\_stack\_size.h
[Threads]
Regex=thread|task
[Stack]
Oversizing=0
[Window]
Width=1255
Height=710
[Tab]
Selected=1
```
