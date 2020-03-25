﻿using System.Collections.Generic;
using UnityEngine;

namespace RagdollMecanimMixer {

    [System.Serializable]
    public class Bone {
        public string name;

        public int parentID;
        public List<int> childIDs;

        //动画骨骼的Transform
        public Transform animTransform;
        //布娃娃的Transform
        public Transform physTransform;
        //骨骼的刚体
        public Rigidbody rigidbody;
        //骨骼的joint，连接父骨骼
        public ConfigurableJoint joint;
        //骨骼的碰撞体
        public Collider collider;
        
        public Quaternion rotOffset;
        
        public Vector3 animPosition;
        public Quaternion animLocalRotation;
        public Quaternion physStartLocalRotation;
        
        //用于插值计算
        public Vector3 rbPrevPos;
        public Quaternion rbPrevRot;
        public Vector3 rbLerpPos;
        public Quaternion rbLerpRot;

        public Vector3 kinVelocity;
        public Vector3 kinAngularVelocity;

        public Vector3 prevStatePos;
        public Quaternion prevStateRot;

        public Vector3 beforeAnimationPos;
        public Quaternion beforeAnimationRot;
        public bool withoutAnimation;

        public bool selfCollision;
        public bool IsKinematic {
            set {
                rigidbody.isKinematic = value;
                if(value == false) {
                    rigidbody.velocity = kinVelocity;
                    rigidbody.angularVelocity = kinAngularVelocity;
                }
            }
            get {
                return rigidbody.isKinematic;
            }
        }
        public bool onlyAnimation;
        public bool AngularLimits {
            set {
                if (!IsRoot) {
                    if (value)
                        joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Limited;
                    else
                        joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Free;
                }
            }
            get {
                if (!IsRoot)
                    return joint.angularXMotion == ConfigurableJointMotion.Limited;
                else
                    return false;
            }
        }

        public float positionDriveSpring;
        public float positionDriveDamper;
        public float positionAccuracy;

        public float rotationDriveSpring;
        public float rotationDriveDamper;
        public float rotationAccuracy;
        public float RotationAccuracy {
            set {
                rotationAccuracy = value;
                if (!IsRoot) {
                    JointDrive jointDrive = joint.slerpDrive;
                    jointDrive.positionSpring = rotationAccuracy * rotationDriveSpring * rigidbody.mass;
                    jointDrive.positionDamper = rotationDriveDamper * rigidbody.mass;
                    joint.slerpDrive = jointDrive;
                }
            }
            get {
                return rotationAccuracy;
            }
        }

        public bool IsRoot {
            get {
                return parentID == -1;
            }
        }

        public bool dependOnDir = false;
    }
}