using UnityEngine;

namespace Network.Player
{   
    [System.Serializable]
    public struct VRPoseData
    {
        public Vector3 headPos;
        public Quaternion headRot;

        public Vector3 leftPos;
        public Quaternion leftRot;

        public Vector3 rightPos;
        public Quaternion rightRot;
    }
}
