using ARLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ARController : MonoBehaviour
{

    public static ARController Instance { get; private set; }

    public Camera renderCamera;
    private List<GameObject> currentPlanes = new List<GameObject>();
    private List<GameObject> currentImages = new List<GameObject>();

    public ARLibController arlib;

    public event Action<ImagesArrayData> OnTrackedImagesUpdated;

    private UIState currentState = UIState.Loading;

    void Awake()
    {   
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ARLibController.CameraPoseUpdated += OnCameraPoseUpdate;
        ARLibController.SurfaceTrackingUpdated += OnSurfaceTrackingUpdate;
        ARLibController.ImageTrackingUpdated += OnImageTrackingUpdate;
        ARLibController.TrackedImagesArrayUpdate += OnTrackedImagesArrayUpdate;
        ARLibController.VPSInitialized += OnVPSInitialize;
        ARLibController.Initialize();
    }

    void Start() {
        ARLibController.EnableCamera();
    }

    public void SetState(UIState state) {
        currentState = state;
    }

    public void preloadImages() {
        TrackingImageData data = new TrackingImageData
        {
            name = "MyImage",
            url = "https://cdn.web-ar.studio/12/242507/media/image/2025-04-0812.14.44_1744218808876.jpg",
            physicalWidth = 0.1f
        };
        ARLibController.AddTrackingImage(data);
    }

    public void OnEnableSurfaceTrackingButtonTap()
    {
        Debug.Log("OnEnableSurfaceTrackingButtonTap");
        ARLibController.EnableSurfaceTracking("both");
    }

    public void OnEnableImageTrackingButtonTap()
    {
        Debug.Log("OnEnableImageTrackingButtonTap");
        ARLibController.EnableImageTracking();
    }

    public void OnEnableVPSButtonTap()
    {
        var settings = new VPSSettings {
            locationsIds = new[] {"2_floor_668696175ec4c318084343f1"},
            type = "mobile",
            gps = false
        };
        ARLibController.SetupVPS(settings);
    }

    public void OnDisableTrackingButtonTap()
    {
        ClearOldPlanes();
        ClearOldImages();
        ARLibController.DisableTracking();
    }

    // Callbacks

    void OnCameraPoseUpdate(CameraPoseData poseData)
    {
        CameraPoseUpdater.UpdateCameraPose(renderCamera, poseData);
    }

    void OnSurfaceTrackingUpdate(PlaneInfo[] planeInfos)
    {
        if (currentState != UIState.SurfaceTracking) {
            ClearOldPlanes();
            return;
        }

        UpdatePlanes(planeInfos);
    }

    void OnImageTrackingUpdate(TrackedImageInfo[] imagesInfo)
    {
        if (currentState != UIState.ImageTracking) {
            ClearOldImages();
            return;
        }

        UpdateImages(imagesInfo);
    }

    void OnTrackedImagesArrayUpdate(ImagesArrayData names)
    {
        OnTrackedImagesUpdated?.Invoke(names);
    }

    void OnVPSInitialize()
    {
        ARLibController.StartVPS();
    }

    // Private

    void UpdatePlanes(PlaneInfo[] planeInfos)
    {
        ClearOldPlanes();

        foreach (var planeInfo in planeInfos)
        {
            GameObject planeObj = CreatePlaneObject(planeInfo);
            currentPlanes.Add(planeObj);
        }
    }

    void ClearOldPlanes()
    {
        foreach (var plane in currentPlanes)
        {
            Destroy(plane);
        }
        currentPlanes.Clear();
    }

    GameObject CreatePlaneObject(PlaneInfo planeInfo)
    {
        GameObject planeObject = new GameObject("Plane");

        var poseData = planeInfo.centerPose;

        planeObject.transform.position = new Vector3(poseData.xPos, poseData.yPos, poseData.zPos);
        planeObject.transform.localEulerAngles = new Vector3(poseData.xAngle, poseData.yAngle, poseData.zAngle);

        LineRenderer lr = planeObject.AddComponent<LineRenderer>();
        int vertexCount = planeInfo.vertices.Length / 2;
        lr.positionCount = vertexCount + 1;

        Vector3[] positions = new Vector3[vertexCount + 1];

        for (int i = 0; i < vertexCount; i++)
        {
            float x = planeInfo.vertices[i * 2];
            float z = planeInfo.vertices[i * 2 + 1];
            positions[i] = new Vector3(x, 0, z);
        }
        positions[vertexCount] = positions[0];

        lr.SetPositions(positions);
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.loop = true;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.useWorldSpace = false;
        lr.transform.parent = planeObject.transform;

        return planeObject;
    }

    GameObject CreateOriginObject(PlaneInfo planeInfo)
    {
        GameObject originObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var poseData = planeInfo.centerPose;
        originObject.transform.position = new Vector3(poseData.xPos, poseData.yPos, poseData.zPos);
        originObject.transform.localScale = Vector3.one * 0.1f;

        var greenMat = new Material(Shader.Find("Standard"));
        greenMat.color = Color.green;
        originObject.GetComponent<MeshRenderer>().material = greenMat;

        return originObject;
    }


    void UpdateImages(TrackedImageInfo[] imagesInfo)
    {
        ClearOldImages();

        foreach (var imageInfo in imagesInfo)
        {
            GameObject planeObj = CreateImageObject(imageInfo);
            currentImages.Add(planeObj);
        }
    }

    void ClearOldImages()
    {
        foreach (var image in currentImages)
        {
            Destroy(image);
        }
        currentImages.Clear();
    }

    GameObject CreateImageObject(TrackedImageInfo info) {

        Vector3 scale = new Vector3(
            info.sizeXmeters,
            0.005f,
            info.sizeZmeters
        );

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(
            info.centerPose.xPos,
            info.centerPose.yPos,
            info.centerPose.zPos
        );
        cube.transform.eulerAngles = new Vector3(
            info.centerPose.xAngle,
            info.centerPose.yAngle,
            info.centerPose.zAngle
        );
        cube.transform.localScale = scale;
        cube.name = info.name;

        return cube;
    }
}


public class CameraPoseUpdater
{
    public static void UpdateCameraPose(Camera camera, CameraPoseData poseData)
    {
        camera.transform.position = new Vector3(poseData.xPos, poseData.yPos, poseData.zPos);
        camera.transform.localEulerAngles = new Vector3(poseData.xAngle, poseData.yAngle, poseData.zAngle);

        if (poseData.projectionMatrix.Count == 16) {
            Matrix4x4 matrix = new Matrix4x4();

            for (int i = 0; i < 16; i++)
            {
                matrix[i] = poseData.projectionMatrix[i];
            }

            camera.projectionMatrix = matrix;
        }
    }
}