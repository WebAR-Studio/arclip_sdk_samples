using System.Collections.Generic;

namespace ARLib
{
    [System.Serializable]
    public class CameraPoseData
    {
        public float xPos;
        public float yPos;
        public float zPos;
        public float xAngle;
        public float yAngle;
        public float zAngle;
        public List<float> projectionMatrix;
    }
}