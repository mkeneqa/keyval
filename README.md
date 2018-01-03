# Welcome

Welcome to this KeyVal Wiki Application. The following should explain a little about the application and how the development progress notes. 

## Purpose for Application

One of the programs I have to log into every morning to trade with 
requires extra authentication in the form of referencing a security 
card.  Basically I have to log in with user name a password, then the 
program will pull up a set of two numbers, I then have to reference 
these two numbers from a security card that I have and put the 
corresponding sequence of numbers and letters into the program, then it 
finishes the authentication and logs me in.  Needless to say it is very 
tedious.
I'm hoping to get a simple app or windows based program that will do 
three things.

1.  Allow me to create a very simple database
2.  Allow me to reference that database by being able to type in two 
different numbers
3.  Copy the corresponding numbers to the clipboard, which I can then 
paste into my program.
 
For Example:  Say the following set in the database
 
1. qwe
2. 2f4
3. 4n9
4. 9sf etc. . . 
 
Say the program asks for 1 and 3... I would then type 1 and 3 into the 
app and it would then return the values qwe and 4n9 and automatically 
copy qwe4n9 to the clipboard.

##Screenshots

__A. Main Screen With Drop Down Edit Menu__

![](https://i.imgur.com/Rkz1oES.png)

__B. Entering In Two Keys and Getting It's Corresponding Value__

![](https://i.imgur.com/uJDHEW8.png)

__C. Editor Window For The Key Value Pairs__

![](https://i.imgur.com/kcwE1qM.png)

## Beta 1.0 Release Notes
__List Of Changes (The last being the most important I think):__

* Added new icons to give application a bit more visual appeal,also changed the executable file icon to a key image
* Now KeyVal checks for out-of-range input Keys in the Database and outputs error message without crashing the program 
*  The KeyVal Editing window is now ordered by the Keys column number where it used to be ordered by an unseen id
* When the program starts up the keyboard cursor will start in the first key input box.
* Logging is added to a text file: KeyValLog.txt
* A new window to change the directory path has been added under the Edit Menu
* I did this as a work around for the Parallels drive mapping issue. I discovered that when Parallels is running it uses a "psf" (parallels shared folder) to map to the mac desktop and directories into windows. Because of how this is being done, windows interprets it as a network path and for some reason won't connect/access with write privileges and therefore crashes the program. By simply removing "psf/home/" to a drive letter i.e. "Z:/" the program will work. You can find the parallels Drive Letter under 'My Computer'. Please see the attached image.
* You could also change this to point to another KeyVal.s3db database located in another folder or directory (say a dropbox shared drive or another existing database etc.) if you’d like as long as long as you provide the full path i.e "C:/Users/Mike/My Documents/KeVal.s3db". 
* If the program gives an error saying it can’t locate the database open the "Change Directory Directory Path" window and it should default to the KeyVal.s3db database within the parent folder when you close the window.

## Wiki Issue Tracker (Fixed. See Trello details below)
[Known Issues](https://bitbucket.org/mkene927/keyval/wiki/Known%20Issues)

## Trello Issue Tracking

Issue Tracking for this project is publicaally setup on Trello: [Trello Link](https://trello.com/b/5P8x8DLU/keyval-app-dev)
