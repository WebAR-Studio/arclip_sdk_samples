using UnityEngine;
using System;

public class ARLibTester : MonoBehaviour
{
    private const string DEFAULT_CAMERA_POSE = "{\"zAngle\":-3.9828472,\"yAngle\":291.30988,\"zPos\":1.0311759,\"xPos\":-1.0278465,\"projectionMatrix\":[3.2147164,0,0,0,0,1.4858518,0,0,-0.012757659,0.01155293,-1.0000001,-1,0,0,-0.001,0],\"yPos\":0.05572335,\"xAngle\":12.325776}";
    private const string DEFAULT_SURFACE_TRACKING = "[{\"centerPose\":{\"zAngle\":0.21607481,\"xAngle\":-88.74867,\"yAngle\":179.35066,\"yPos\":-0.3478001,\"xPos\":-1.9290587,\"zPos\":-0.74720913},\"vertices\":[-1.0499978,-0.34999996,0.2500012,-0.29999998,1.1000004,-0.25,1.45,-0.2,1.7999997,-0.14999999,1.8499997,-0.099999994,1.8499997,-0.049999997,1.7999997,0.099999994,1.6999998,0.19999999,0.9500005,0.49999997,0.450001,0.5499999,1.4305115e-06,0.5499999,-0.9999978,0.4999999,-1.0999978,0.44999993,-1.1499977,0.39999992,-1.1999977,0.29999992,-1.2999976,-0.15000002,-1.2999976,-0.34999996],\"type\":\"VERTICAL\"},{\"vertices\":[-0.6,0.049999993,-0.5499999,-0.050000004,-0.24999999,-0.5499999,-0.19999997,-0.5499999,-0.049999975,-0.49999997,0.79999995,-3.9952763e-08,1.3,0.29999992,1.4499999,0.39999995,1.5,0.5999999,1.5,0.65,1.4,0.79999995,1.2499999,0.99999994,1.15,1.0499998,0.99999994,1.0499998,0.935663,1.0218524,-0.59999996,0.15296656],\"type\":\"HORIZONTAL\",\"centerPose\":{\"xAngle\":0,\"yAngle\":211.1462,\"zPos\":-0.08121816,\"zAngle\":0,\"yPos\":-0.999558,\"xPos\":-1.0125424}},{\"centerPose\":{\"zPos\":-0.5281688,\"xPos\":-1.6435297,\"xAngle\":-81.60986,\"yPos\":-0.75350654,\"yAngle\":180.12099,\"zAngle\":1.5075086},\"type\":\"VERTICAL\",\"vertices\":[-0.87288755,-0.24532646,0.6999999,-0.14999999,0.6999999,-0.050000012,0.24999988,0.099999994,-0.9500004,0.10000006,-1.0000005,0.10000006,-1.1000004,-0.049999945,-1.1000004,-0.19999996,-1.055382,-0.24461837]},{\"centerPose\":{\"yPos\":-1.394752,\"zPos\":0.5802078,\"xPos\":-1.6476681,\"xAngle\":0,\"zAngle\":0,\"yAngle\":211.1462},\"vertices\":[-0.85,-0.29999995,-0.74999994,-0.45,-0.69999987,-0.5,-0.4499999,-0.5,-0.24999987,-0.4,0.2500002,-0.09999992,0.6500003,0.15000014,0.7500003,0.25000012,0.80000037,0.3000002,0.80000037,0.40000024,0.6500003,0.55000025,0.55000025,0.55000025,0.45000026,0.50000024,1.756419e-07,0.25000015,-0.34999987,0.050000124,-0.85,-0.24999994],\"type\":\"HORIZONTAL\"},{\"type\":\"VERTICAL\",\"centerPose\":{\"zPos\":-0.07718697,\"yPos\":-0.30521247,\"yAngle\":261.75464,\"xPos\":-3.8429847,\"zAngle\":16.40661,\"xAngle\":-89.08187},\"vertices\":[0.34999996,-0.20000002,0.34999996,0.09999998,0.29999995,0.19999997,0.24999994,0.24999999,0.19999993,0.29999998,-0.10000014,0.45,-0.25000018,0.45,-0.4000002,0.44999993,-0.45000023,0.39999992,-0.45000023,0.24999994,-0.4000002,-0.10000001,-0.20000017,-0.2]}]";
    private const string DEFAULT_IMAGE_TRACKING = "[{\"sizeZmeters\":0.18161157,\"centerPose\":{\"yPos\":-0.0821593,\"xPos\":0.23575564,\"yAngle\":123.23298,\"zAngle\":5.7055225,\"zPos\":-0.13720967,\"xAngle\":-78.82307},\"trackingState\":\"TRACKING\",\"sizeXmeters\":0.1,\"name\":\"MyImage\"}]";
    private const string DEFAULT_TRACKED_IMAGES = "{\"names\":{\"image2\",\"image2\"]}";
    private const string DEFAULT_VPS_POSITION = "{\"status\":\"VPS_READY\",\"localisation\":{\"timestamp\":1744284305.672099,\"trackingRotation\":{\"z\":21.342361,\"y\":-167.7282,\"x\":177.88382},\"trackingPosition\":{\"z\":0.44230074,\"x\":-3.8124082,\"y\":-0.53475565},\"vpsRotation\":{\"z\":21.342361,\"x\":177.88382,\"y\":-167.7282},\"gpsLongitude\":37.00949093200282,\"heading\":200.8991402319043,\"accuracy\":0,\"locationId\":\"2_floor_668696175ec4c318084343f1\",\"gpsLatitude\":55.84484002229948,\"vpsPosition\":{\"z\":0.44230074,\"y\":-0.53475565,\"x\":-3.8124082}}}";

    private ARLibController arLibController;

    private void Awake()
    {
        arLibController = GetComponent<ARLibController>();
        if (arLibController == null)
        {
            Debug.LogError("ARLibController не найден на этом GameObject!");
        }
    }

    public void TestInitialized()
    {
        if (arLibController == null) return;
        arLibController.SendMessage("OnInitialized");
    }

    public void TestCameraPoseUpdate()
    {
        if (arLibController == null) return;
        arLibController.SendMessage("OnCameraPoseUpdate", DEFAULT_CAMERA_POSE);
    }

    public void TestSurfaceTrackingUpdate()
    {
        if (arLibController == null) return;
        arLibController.SendMessage("OnSurfaceTrackingUpdate", DEFAULT_SURFACE_TRACKING);
    }

    public void TestImageTrackingUpdate()
    {
        if (arLibController == null) return;
        arLibController.SendMessage("OnImageTrackingUpdate", DEFAULT_IMAGE_TRACKING);
    }

    public void TestTrackedImagesUpdate()
    {
        if (arLibController == null) return;
        arLibController.SendMessage("OnTrackedImagesArrayUpdate", DEFAULT_TRACKED_IMAGES);
    }

    public void TestVPSReady()
    {
        if (arLibController == null) return;
        arLibController.SendMessage("OnVPSReady");
    }

    public void TestVPSPositionUpdate()
    {
        if (arLibController == null) return;
        arLibController.SendMessage("OnVPSPositionUpdate", DEFAULT_VPS_POSITION);
    }
} 