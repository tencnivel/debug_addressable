# debug_addressable

Minimalist Unity project to reproduce problems with addressables

## How to reproduce 'HTTP/1.1 500 Internal Server Error'

### Prerequisite

Unity 2019.4.8f1

### Clone the project to your environment

`git clone this project`

### Configure the project to your environment

#### Tweak the Addressable 'Default' profile

Modify the RemoteLoadPath to a web server on which you can upload your asset bundles. 

Make sure to use RemoteLoadPath with 'https' because this would not work on iOS.

### Check that things are working as expected on Android

  * Switch to the Android platform and configure the keystore
  * Create the addressable asset bundle, catalog and 'addressables_content_state.bin' using the Addressable 'Default build script'
  * Upload the content of 'ServerData/Android' to the web server
  * Build and run the APK to a device
  * Redirect logs to a file `./adb.exe logcat  -s Unity > /C/Users/your_user/tmp/logcat.log`
  * Press the button in the UI
  * Check in logcat.log that you can see the content of  'textfile1.txt'
  * Modify the content of 'TextFiles/textfile1.txt'
  * Update the addressable asset bundlesand catalog using the Addressable 'Update a previous build' script
  * Upload the new catalog and asset bundle to the web server
  * Restart the app on the android device, press the button in the UI, check in logcat.log that the app is using the latest version of 'textfile1.txt'

### Reproduce the bug on iOS

If you try to reproduce the same steps on iOS as on Android you will get an "HTTP/1.1 500 Internal Server Error" when pressing the button in the UI.
The URL of the bundle will be displayed in the error message, you can check that there is no problem with this URL by downloading the bundle with any other mean (eg. wget).




