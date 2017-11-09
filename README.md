## DESCRIPTION
The stable first release version of the application which gets inputs (corresponding<br>
to date formats) as program arguments, and makes culture dependent date ranges from them.

## INSTALLATION
1. Install .NET Framework (the latest version is advised) on your computer to compile<br>
source code on your own, or use already compiled release version in the `Program.exe`
2. (Method no.1) Compile program on your own:
- install [IDE](https://en.wikipedia.org/wiki/Integrated_development_environment) ([Visual Studio Community](https://www.visualstudio.com/vs/community/) (_Windows_), which came with a large pack of necessary<br>
add-ons, tools, and .NET framework already, [MonoDevelop](http://www.monodevelop.com/) (_Windows_, _Linux_, _Mac OS_),<br>
or JetBrains [Rider](https://www.jetbrains.com/rider/) (_Windows_, _Linux_, _Mac OS_) (_30 days free trial_), is recommended),
- download source code from GitHub (`.zip` or `.tar.gz` file),
- browse your local download directory, and unpack the archive to the new catalog,
- browse newly created catalog files from inside of IDE, find and open solution file `.sln`,
- click Build Project, Start, Run, or something similar (read your IDE developers site, Google,<br>
or StackOverflow dev forum for more details and helpful information how to do it exactly)
- after compilation, find the executable file by CMD.exe (_Windows_), CLI (_Linux_), PowerShell,<br>
Git Bash, or similar tool. Search for `.exe` file in your catalog (move deeper by `cd` command)<br>
to get path: `[YOUR LOCATION]\DateRange_ConsoleApp\DateRangeConsoleApplication\bin`.<br>
Choose `Debug` or `Release` catalog in which you find `DateRangeConsoleApplication.exe` file
2. (Method no.2) Use already compiled program:
- download [Program.exe](https://github.com/Thomas-M-Krystyan/DateRange_ConsoleApp/releases/download/v1.0.0/Program.exe) file from GitHub repository
- find the executable file by CMD.exe (_Windows_), CLI (_Linux_), PowerShell, Git Bash, or similar<br>
tool. Search for `Program.exe` file in your download catalog (move deeper by `cd` command)

## RUNNING
3. Run application with at least one argument:
- on CMD.exe / CLI / PowerShell => type `Program.exe 25-08-2012 27-09-2012` (_example_)<br>
(Note: on bash, you must add keyword `start` before the Program.exe command)
- confirm your choice by pressing [ENTER]
- if you do everything properly you should be able to see generated date range from your inputs

#### Expected results:
```
1)  Program.exe  05.01.2017

> 05.01.2017

2)  Program.exe  05.01.2017  05.01.2017

> 05.01.2017

3)  Program.exe  02.01.2017  03.01.2017  05.01.2017

> 02-05.01.2017

4)  Program.exe  02.01.2017  20.02.2017  01.03.2017  15.03.2017

> 02.01 – 15.03.2017

5)  Program.exe  05.01.2016  05.01.2017  05.01.2018

> 05.01.2016 – 05.01.2018

6)  Program.exe  31.12.2020  01.01.2015

> ERROR: The previous date "31.12.2020" cannot be later
  than the farther one "01.01.2015"!
```
Do not worry it is a normal behavior
