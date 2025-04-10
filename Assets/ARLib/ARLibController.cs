using ARLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ARLibController: MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ARLib_Initialize();

    [DllImport("__Internal")]
    private static extern void ARLib_EnableCamera();

    [DllImport("__Internal")]
    private static extern void ARLib_DisableCamera();

    [DllImport("__Internal")]
    private static extern void ARLib_DisableTracking();

    [DllImport("__Internal")]
    private static extern void ARLib_EnableSurfaceTracking(string axis);

    [DllImport("__Internal")]
    private static extern void ARLib_AddTrackingImage(string data);

    [DllImport("__Internal")]
    private static extern void ARLib_RemoveTrackingImage(string name);

    [DllImport("__Internal")]
    private static extern void ARLib_RemoveAllTrackingImages();

    [DllImport("__Internal")]
    private static extern void ARLib_EnableImageTracking();

    [DllImport("__Internal")]
    private static extern void ARLib_SetupVPS(string settings);

    [DllImport("__Internal")]
    private static extern void ARLib_StartVPS();

    [DllImport("__Internal")]
    private static extern void ARLib_StopVPS();

    [DllImport("__Internal")]
    private static extern void ARLib_PauseVPS();

    [DllImport("__Internal")]
    private static extern void ARLib_ResetTracking();

    [DllImport("__Internal")]
    private static extern void ARLib_ResumeVPS();

    [DllImport("__Internal")]
    private static extern void ARLib_SetLocationIds();

    [DllImport("__Internal")]
    private static extern void ARLib_SetAnimationTime(float value);

    [DllImport("__Internal")]
    private static extern void ARLib_SetSendFastPhotoDelay(float value);

    [DllImport("__Internal")]
    private static extern void ARLib_SetSendPhotoDelay(float value);

    [DllImport("__Internal")]
    private static extern void ARLib_SetDistanceForInterp(float value);

    [DllImport("__Internal")]
    private static extern void ARLib_SetGpsAccuracyBarrier(float value);

    [DllImport("__Internal")]
    private static extern void ARLib_SetTimeOutDuration(float value);

    [DllImport("__Internal")]
    private static extern void ARLib_SetFirstRequestDelay(float value);

    [DllImport("__Internal")]
    private static extern void ARLib_SetAngleForInterp(float value);

    public static event Action Initialized;

    /// <summary>
    /// Event triggered when camera pose is updated.
    /// </summary>
    public static event Action<CameraPoseData> CameraPoseUpdated;

    /// <summary>
    /// Event triggered when surface tracking data is updated.
    /// Contains JSON string with array of currently tracked surfaces.
    /// </summary>
    public static event Action<PlaneInfo[]> SurfaceTrackingUpdated;

    /// <summary>
    /// Event triggered when image tracking data is updated.
    /// Contains JSON string with array of currently tracked images.
    /// </summary>
    public static event Action<TrackedImageInfo[]> ImageTrackingUpdated;

    /// <summary>
    /// Event triggered when the list of tracked images is updated.
    /// Contains JSON string with array of image names.
    /// </summary>
    public static event Action<ImagesArrayData> TrackedImagesArrayUpdate;

    /// <summary>
    /// Event triggered when VPS is successfully initialized and ready to use.
    /// </summary>
    public static event Action VPSInitialized;

    /// <summary>
    /// Event triggered when VPS position is updated(successfully localized position).
    /// Contains JSON string with position data.
    /// </summary>
    public static event Action<VPSPoseData> VPSPositionUpdated;

    /// <summary>
    /// Initializes ARLib. Must be called before using other methods.
    /// </summary>
    public static void Initialize() 
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_Initialize();
#endif
    }

    /// <summary>
    /// Enables the device camera view.
    /// </summary>
    public static void EnableCamera() 
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_EnableCamera();
#endif
    }

    /// <summary>
    /// Disables the device camera view.
    /// </summary>
    public static void DisableCamera()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_DisableCamera();
#endif
    }

    /// <summary>
    /// Enables surface tracking in the specified mode.
    /// </summary>
    /// <param name="mode">Surface tracking mode. Valid values: "horizontal", "vertical", "both".</param>
    public static void EnableSurfaceTracking(string mode)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_EnableSurfaceTracking(mode);
#endif
    }

    /// <summary>
    /// Disables all types of tracking (surfaces, images, vps).
    /// </summary>
    public static void DisableTracking() 
    { 
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_DisableTracking();
#endif
    }

    /// <summary>
    /// Enables image tracking.
    /// </summary>
    /// <remarks>
    /// Before enabling tracking:
    /// 1. Add tracking images via <see cref="AddTrackingImage"/>
    /// 2. Wait for <see cref="OnTrackedImagesArrayUpdate"/> callback to ensure images are downloaded and ready
    /// </remarks>
    public static void EnableImageTracking() 
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_EnableImageTracking();
#endif
    }

    /// <summary>
    /// Adds an image for tracking.
    /// </summary>
    /// <param name="data">Image data for tracking.</param>
    /// <remarks>
    /// Images must be added before calling <see cref="EnableImageTracking"/>.
    /// After successful addition, <see cref="OnTrackedImagesArrayUpdate"/> will be called with the updated list of tracking images.
    /// </remarks>
    public static void AddTrackingImage(TrackingImageData data) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_AddTrackingImage(JsonUtility.ToJson(data));
#endif
    }

    /// <summary>
    /// Removes a specific tracking image by name.
    /// </summary>
    /// <param name="name">Name of the image to remove.</param>
    /// <remarks>
    /// After successful removal, <see cref="OnTrackedImagesArrayUpdate"/> will be called with the updated list of tracking images.
    /// </remarks>
    public static void RemoveTrackingImage(string name) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_RemoveTrackingImage(name);
#endif
    }

    /// <summary>
    /// Removes all tracking images.
    /// </summary>
    /// <remarks>
    /// After successful removal, <see cref="OnTrackedImagesArrayUpdate"/> will be called with an empty list.
    /// </remarks>
    public static void RemoveAllTrackingImages() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_RemoveAllTrackingImages();
#endif
    }

    /// <summary>
    /// Configures and Start VPS.
    /// </summary>
    /// <param name="settings">VPS settings including location IDs and other parameters.</param>
    /// <remarks>
    /// The <see cref="VPSInitialized"/> event will be triggered after successful setup.
    /// </remarks>
    public static void SetupVPS(VPSSettings settings) {
#if UNITY_WEBGL && !UNITY_EDITOR
        var stringSettings = JsonUtility.ToJson(settings);
        ARLib_SetupVPS(stringSettings);
#endif
    }


    /// <summary>
    /// Start VPS.
    /// </summary>
    public static void StartVPS() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_StartVPS();
#endif
    }

    /// <summary>
    /// Stops VPS operation.
    /// </summary>
    public static void StopVPS() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_StopVPS();
#endif
    }

    /// <summary>
    /// Pauses VPS operation.
    /// </summary>
    /// <remarks>
    /// Use <see cref="ResumeVPS"/> to resume operation.
    /// </remarks>
    public static void PauseVPS() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_PauseVPS();
#endif
    }

    /// <summary>
    /// Resets VPS tracking state.
    /// </summary>
    public static void ResetTracking() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_ResetTracking();
#endif
    }

    /// <summary>
    /// Resumes VPS operation after pause.
    /// </summary>
    /// <remarks>
    /// Used after <see cref="PauseVPS"/>.
    /// </remarks>
    public static void ResumeVPS() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_ResumeVPS();
#endif
    }

    /// <summary>
    /// Sets location IDs for VPS.
    /// </summary>
    public static void SetLocationIds() {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetLocationIds();
#endif
    }

    /// <summary>
    /// Sets animation time for VPS.
    /// </summary>
    /// <param name="value">Animation time in seconds.</param>
    public static void SetAnimationTime(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetAnimationTime(value);
#endif
    }

    /// <summary>
    /// Sets fast photo sending delay for VPS.
    /// </summary>
    /// <param name="value">Delay in seconds.</param>
    public static void SetSendFastPhotoDelay(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetSendFastPhotoDelay(value);
#endif
    }

    /// <summary>
    /// Sets photo sending delay for VPS.
    /// </summary>
    /// <param name="value">Delay in seconds.</param>
    public static void SetSendPhotoDelay(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetSendPhotoDelay(value);
#endif
    }

    /// <summary>
    /// Sets interpolation distance for VPS.
    /// </summary>
    /// <param name="value">Distance in meters.</param>
    public static void SetDistanceForInterp(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetDistanceForInterp(value);
#endif
    }

    /// <summary>
    /// Sets GPS accuracy barrier for VPS.
    /// </summary>
    /// <param name="value">Accuracy value in meters.</param>
    public static void SetGpsAccuracyBarrier(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetGpsAccuracyBarrier(value);
#endif
    }

    /// <summary>
    /// Sets timeout duration for VPS.
    /// </summary>
    /// <param name="value">Duration in seconds.</param>
    public static void SetTimeOutDuration(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetTimeOutDuration(value);
#endif
    }

    /// <summary>
    /// Sets first request delay for VPS.
    /// </summary>
    /// <param name="value">Delay in seconds.</param>
    public static void SetFirstRequestDelay(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetFirstRequestDelay(value);
#endif
    }

    /// <summary>
    /// Sets interpolation angle for VPS.
    /// </summary>
    /// <param name="value">Angle in degrees.</param>
    public static void SetAngleForInterp(float value) {
#if UNITY_WEBGL && !UNITY_EDITOR
        ARLib_SetAngleForInterp(value);
#endif
    }

    // ARLib Callbacks

    void OnInitialized() {
        Initialized?.Invoke();
    }

    void OnCameraPoseUpdate(string json)
    {
        try {
            var poseData = JsonUtility.FromJson<CameraPoseData>(json);
            CameraPoseUpdated?.Invoke(poseData);
        } catch (Exception e) {
            Debug.LogError($"Failed to parse camera pose data: {e.Message}");
        }
    }

    void OnSurfaceTrackingUpdate(string json)
    {
        try {
            string wrappedJson = "{\"items\":" + json + "}";
            var planesWrapper = JsonUtility.FromJson<PlanesWrapper>(wrappedJson);
            SurfaceTrackingUpdated?.Invoke(planesWrapper.items);
        } catch (Exception e) {
            Debug.LogError($"Failed to parse surface tracking data: {e.Message}");
        }
    }

    void OnImageTrackingUpdate(string json)
    {
        try {
            string wrappedJson = "{\"items\":" + json + "}";
            var imagesWrapper = JsonUtility.FromJson<ImagesWrapper>(wrappedJson);
            ImageTrackingUpdated?.Invoke(imagesWrapper.items);
        } catch (Exception e) {
            Debug.LogError($"Failed to parse image tracking data: {e.Message}");
        }
    }

    void OnTrackedImagesArrayUpdate(string names) {
        try {
            var imageNames = JsonUtility.FromJson<ImagesArrayData>(names);
            TrackedImagesArrayUpdate?.Invoke(imageNames);
        } catch (Exception e) {
            Debug.LogError($"Failed to parse tracked images data: {e.Message}");
        }
    }

    void OnVPSReady() {
        VPSInitialized?.Invoke();
    }

    void OnVPSPositionUpdate(string json) {
        try {
            var poseData = JsonUtility.FromJson<VPSPoseData>(json);
            VPSPositionUpdated?.Invoke(poseData);
        } catch (Exception e) {
            Debug.LogError($"Failed to parse VPS position data: {e.Message}");
        }
    }

}