# CS499-VRetina
Repository containing Team 8's project for CS-499. 

## About VRetina
VRetina is an extended reality application that allows for viewing of static high-resolution 3D retinal images. Currently we have implemented a system that allows up-close viewing of the retinal images, and future iterations of this will incorporate study tools to help identify different features of each retinal image.

## Required Tools
* [Git LFS](https://git-lfs.github.com/)
* Unity (Currently 2019.3.0f6, but should be updated regularly to the newest version)
  * Include build support for Windows/Mac, as well as Android and iOS
* Unity Hub

## Utilized Packages:
* Google VR Android and Google VR iOS*
* Unity’s AR Foundation
* ARCore XR Plugin, ARKit XR Plugin
* Quantum UI

*Google VR for Android and iOS is deprecated and will be removed in Unity 2020.1 A new Unity XR Plugin is in the works. The thread following the plugin can be found here.

## Setting Up Unity
1. Install Unity Hub.
1. Install the latest version of Unity, marking the build support checkboxes for Windows/Mac, as well as Android (Check “Android SDK and NDK Tools”, as well as “OpenJDK”) and iOS.
1. Clone the repository.
1. Once Unity is installed, in Unity Hub navigate to Projects > Add, and open the folder in the cloned repository named “VR Test”.
1. The application can now be opened up from the Projects tab in the Unity Hub.

## Building and Running the Application

### Android
1. All plugins and packages should be included when the repo is cloned.
1. Navigate to File > Build Settings. The “Scenes in Build” should include, in this order, Scenes/Start, Scenes/VRDemo, and Scenes/ARDemo. 
1. Select Android under the list of Platforms, then choose “Switch Platform” at the bottom of the window. This may take a few minutes.
1. On your Android device, navigate to your device settings and enable USB Debugging.
1. Plug your device into your computer. In the build settings next to “Run Device”, click the refresh button and choose your android device from the dropdown list.
1. Click “Build and Run”, then save the APK. Building to the device for the first time, or after substantial changes, may take a few minutes. 
1. Once it is done building the application, the application should automatically load on the device.

### iOS
1. Building for iOS requires an iOS device as well as a Mac with Xcode installed.
1. All plugins and packages should be included when the repo is cloned.
1. Navigate to File > Build Settings. The “Scenes in Build” should include, in this order, Scenes/Start, Scenes/VRDemo, and Scenes/ARDemo. 
1. Select iOS under the list of Platforms, then choose “Switch Platform” at the bottom of the window. This may take a few minutes.
1. Click the “Build” button, and save the application.
1. You may receive a popup saying that “Unity.XR.ARKit will be stripped”. Click “Yes, fix and build”
1. Building the application will create an XCode project. Navigate to the built iOS project and open it in Xcode.
1. Plug in your iOS device
1. With the project open in Xcode, click on the top level project directory to get a list of general project settings. 
1. Plug in your iOS device
1. Ensure that the “Unity-iPhone” scheme is selected at the top of the screen, along with the plugged in iOS device.
1. Click the “Run” play button. This will deploy the app to your iOS device, and may take a few minutes.
1. Once it is done building the application, the application should automatically load on the device. Your device may ask to give the application permission to your camera. Select “OK”.
