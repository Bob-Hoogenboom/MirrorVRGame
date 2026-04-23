using Mirror;
using UnityEngine;

namespace Network.Player
{
    public class NetworkPlayerVR : NetworkBehaviour
    {
        [Header("Local XR References")]
        [SerializeField] private Transform head;
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;

        [Header("Remote Rig Targets")]
        [SerializeField] private Transform headTarget;
        [SerializeField] private Transform leftTarget;
        [SerializeField] private Transform rightTarget;

        private VRPoseData _lastReceivedPose;
        private float _sendInterval = 1f / 20f;
        private float _timer;


        private void LateUpdate()
        {
            if (!isLocalPlayer) return;
            Debug.Log(isLocalPlayer);

            _timer += Time.deltaTime;
            if (_timer >= _sendInterval)
            {
                _timer = 0;
                SendPose();
                ApplyRemotePose();
            }
        }

        //local sending position
        private void SendPose()
        {
            VRPoseData pose = new VRPoseData
            {
                headPos = head.localPosition,
                headRot = head.localRotation,

                leftPos = leftHand.localPosition,
                leftRot = leftHand.localRotation,

                rightPos = rightHand.localPosition,
                rightRot = rightHand.localRotation
            };

            CmdSendPose(pose);
        }

        //send data to server
        [Command]
        private void CmdSendPose(VRPoseData pose)
        {
            RpcReceivePose(pose);
        }

        //broadcast to clients
        [ClientRpc(channel = Channels.Unreliable)]
        private void RpcReceivePose(VRPoseData pose)
        {
            if (isLocalPlayer) return; // don't apply to self
            _lastReceivedPose = pose;
        }

        //smoothing
        private void ApplyRemotePose()
        {
            float smooth = 15f * Time.deltaTime;

            headTarget.localPosition = Vector3.Lerp(headTarget.localPosition, _lastReceivedPose.headPos, smooth);
            headTarget.localRotation = Quaternion.Slerp(headTarget.localRotation, _lastReceivedPose.headRot, smooth);

            leftTarget.localPosition = Vector3.Lerp(leftTarget.localPosition, _lastReceivedPose.leftPos, smooth);
            leftTarget.localRotation = Quaternion.Slerp(leftTarget.localRotation, _lastReceivedPose.leftRot, smooth);

            rightTarget.localPosition = Vector3.Lerp(rightTarget.localPosition, _lastReceivedPose.rightPos, smooth);
            rightTarget.localRotation = Quaternion.Slerp(rightTarget.localRotation, _lastReceivedPose.rightRot, smooth);
        }
    }
}

