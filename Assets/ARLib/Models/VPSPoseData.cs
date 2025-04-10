using UnityEngine;
using System;

[Serializable]
public class VPSPoseData
{
    public string status;
    public LocalisationData localisation;
}

[Serializable]
public class LocalisationData
{
    public double timestamp;
    public RotationData trackingRotation;
    public PositionData trackingPosition;
    public RotationData vpsRotation;
    public double gpsLongitude;
    public double heading;
    public int accuracy;
    public string locationId;
    public double gpsLatitude;
    public PositionData vpsPosition;
}

[Serializable]
public class RotationData
{
    public float z;
    public float y;
    public float x;
}

[Serializable]
public class PositionData
{
    public float z;
    public float x;
    public float y;
} 