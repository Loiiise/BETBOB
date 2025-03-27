# Summary
BETBOB is a program that makes it easy to backup files from several places on your device and gathers them to one single place. If you're backing up your files to keep your data safe, it's of course recommended to store this backup on another device, an external drive or a cloud solution.

## Platforms
The program was developed and tested for my own use on a Windows 10 machine, other versions of Windows should not be a problem. I also suspect it will run just fine on other operating systems like MacOS and Linux, although I have not verified this. If you do try; please let me know your findings.

# Recommended usage
Currently the program only has a commandline interface. The rest of the instructions assume you have a terminal open at the location of the exe.

## Disclaimer
There is no way to know where your store your files. If you only use common system folders (like `Documents`, `Pictures` and such) the default method will work great for you. If you store files in other places too, be sure to add them to the configuration in the `Customizing it to your needs (optional)` section.

## Initializing 
We start off by calling `BETBOB init`. This will generate a file in the folder we're currently in. We now have a configuration!

## Customizing it to your needs (optional)
When you open the file (for example in VSCode or Notepad) you will find it added some standard folders. Feel free to add any other files or folders or change the output path. Just be sure it will still be in a valid `json` format.

## Performing the backup
Call `BETBOB backup` for the program to perform the backup. It will look for the configuration we just generated. Once it's done, it will let you know. Depending on the size of your file and the speed of your drive(s) this might take a while.

## Checking if everything went well
Once the backup if performed the program will let you know where your files are stored. When you open your backup, you may notice there are some folders you didn't request, and maybe not all the folders you wanted show up right away. This is because the program currently always maintains the original file structure. If you trace the paths; all your files should be there.
Also, the backup contains a copy of the configuration it used to create this backup. This will make it easy to reuse on future backups!

# Other usages  
There are a few settings you can tweak to do some fancy things. Instructions on these will be documented later. For now: feel free to mess around.