# Unity - Image Downloader and UIDialog-Design

<h2> Problem Statement - Image Downloader</h2>
<p>Design a singleton library which can be used by any class to download images from
the web. Design a game scene with several images with an attached script WebImage.
Webimage internally calls this DownloadImage Singleton to download the image and
update its image.</p>
<h4>WebImage.cs</h4>
<p>Expose a string url and auto set the image to downloaded image from web. If no image is
downloaded because of internet or other issues, it will default to existing image
</p>
<h3>Key Points: </h3>
<p>1. Game performance goes down if multiple images are downloaded at same time.So the solution enforce a
policy that game can only download 3 images at one specified point of time. Downloaded
images can also contain alpha</p>
<p>2. Cache downloaded images on file system to avoid redownload</p>
<p>3. Invalidate downloaded cache after 7 days</p>
<p>4. Optional Memory Caching- Expose a boolean in WebImage specifying whether image
should be cached in memory or not</p>

<h2> Unity UI - Dialog Design </h2>
<h3> Demo Video </h3>
<a href="https://www.youtube.com/watch?v=ABVIvbe1ubc">Dialog Demo Video</a>
<h4> Details </h4>
<p>
Dialog will contains an icon, title text, description text, variable
number(0,1,2,3) of buttons and a close button. Pressing any button or close
button should close the dialog. Dialog must animate in and out of the
screen. Dialog must block the background screen, so user cannot click on
background when dialog is in front.Dialog class should expose a method to show the dialog which takes in
variables for the dialog configuration - icon sprite, title, description,
buttons etc. Dialog class should return the user click response back to the
caller class - telling which button is pressed.
Common Loader Scene:
Create a loader scene which creates a non-destroyable Dialog UI and
DialogManager Singleton. All other scenes spawn the dialog by calling
DialogManager.Instance.Show(... all required parameter for configuration ..)
Build a game with three scenes. Every scene has
● Three buttons to navigate to any scenes
● One button (“Show”) to show a dialog
● One button (“Print”) which print scene name to the console
● One Response(UI.Text) to show the response
<h4>Scene 1</h4>:​ When I press “Show” button, it’ll show the dialog with:
Icon: icon1
Title: Scene 1
Description: Scene 1 dialog has two buttons
Buttons: Yes and No
Response:
● If I press yes button, Response Text will show Yes
● If I press no button, Response Text will show no
● If I press close button, Response Text will become empty
<h4>Scene 2</h4>:​ When I press “Show” button, it’ll show the dialog with:
Icon: icon2
Title: Scene 2
Description: Scene 2 dialog has no buttons
Response:
● When I press close button, Response Text will become empty
<h4>Scene 3</h4>:​ When I press “Show” button, it’ll show the dialog with:
Icon: icon1
Title: Scene 3
Description: Scene 3 dialog has one button and no close button
Button: Buy
Response:
● When I press Buy button, Response Text will show Buy
</p>

