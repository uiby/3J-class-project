﻿///
/// @file  LeapHandAction.cs
/// @brief
///   This script will turn hand motion into game obj motion.
///   Implementer have to consider operability.
///

using UnityEngine;
using System;
using System.Collections;
using Leap;

namespace Player.Action {
	/// LeapHandAction < PlayerAction < BaseAction < MonoBehaviour
	public class LeapHandAction : PlayerAction {
		private Hand hand;
		private Hand otherHand;

		private Transform GetMainCamera() {
			return Camera.main.transform;
		}

		// motion
		const int MOVING_DETECT_RANGE = 60;
		private float moveSpeed = 0.03f;

		// rotation
		private float upScale = 7;
		private float downScale = -7;
		private float rightScale = 7;
		private float leftScale = -7;
		private float counterClockwiseScale = 7;
		private float clockwiseScale = -7;
		private bool isRotatedX = false;
		private bool isRotatedY = false;
		private bool isRotatedZ = false;
		private float rotateScale = 10;
		private float pitch;
		private float yaw;
		private float roll;

		// rotate camera
		public bool isCameraRotatedToRight = false;
		public bool isCameraRotatedToLeft  = false;


		private bool isFingersFolded(Hand hand) {
			Vector origin = hand.Fingers[0].TipPosition;
			float dist = 0;

			foreach (Finger finger in hand.Fingers) {
				dist += finger.TipPosition.DistanceTo(origin);
			}
			return (dist < 250) ? true : false;
		}

		private bool hasTwoHands() {
			return otherHand.IsValid;
		}


		override
		protected void InitPerFrame() {
			Controller controller = new Controller();
			Frame frame = controller.Frame();
			HandList hands = frame.Hands;
			hand = hands[0];
			otherHand = hands[1];

			pitch = hand.Direction.Pitch * rotateScale;
			yaw   = hand.Direction.Yaw   * rotateScale;
			roll  = hand.PalmNormal.Roll * rotateScale;

			// horizon hand
			if (!isFingersFolded(hand) && upScale > pitch && pitch > downScale) {
				isRotatedX = false;
			}
			if (!isFingersFolded(hand) && rightScale > yaw && yaw > leftScale) {
				isRotatedY = false;
			}
			if (!isFingersFolded(hand) && counterClockwiseScale > roll && roll > clockwiseScale) {
				isRotatedZ = false;
			}
		}

		override
		protected bool ValidatePerFrame() {
			return base.blockController && hand.Confidence > 0.1;
		}

		/// move block with opened hand in x-axis
		override
		protected void DetectMotionX() {
			if (isFingersFolded(hand)) return;
			if (hasTwoHands()) return;

			float handX = hand.PalmPosition.x;

			if (handX > MOVING_DETECT_RANGE) {
				Vector3 right = GetMainCamera().TransformDirection(Vector3.right) * moveSpeed;
				blockController.MoveBlock(right);
			} else if (handX < -MOVING_DETECT_RANGE) {
				Vector3 left = GetMainCamera().TransformDirection(Vector3.left) * moveSpeed;
				blockController.MoveBlock(left);
			}
		}

		/// Drop Block with clenched fists
		override
		protected void DetectMotionY() {
			if (isFingersFolded(hand)) return;

			float velocityY = hand.PalmVelocity.y;

			if (isFingersFolded(hand)) {
				if (velocityY < -400) {
					blockController.DropBlock();
				}
			}
		}

		/// move block with opened hand in z-axis
		override
		protected void DetectMotionZ() {
			if (isFingersFolded(hand)) return;
			if (hasTwoHands()) return;

			float handZ = -hand.PalmPosition.z;

			if (handZ > MOVING_DETECT_RANGE) {
				Vector3 forward = GetMainCamera().TransformDirection(Vector3.forward) * moveSpeed;
				blockController.MoveBlock(forward);
			} else if (handZ < -MOVING_DETECT_RANGE) {
				Vector3 back = GetMainCamera().TransformDirection(Vector3.back) * moveSpeed;
				blockController.MoveBlock(back);
			}
		}

		/// Pitch Block
		override
		protected void DetectRotationX() {
			if (isFingersFolded(hand)) return;
			if (hasTwoHands()) return;
			if (isRotatedX) return;
			if (pitch > upScale) {
				Vector3 back = GetMainCamera().TransformDirection(Vector3.back);
				blockController.PitchBlock(back);
			} else if (pitch < downScale) {
				Vector3 forward = GetMainCamera().TransformDirection(Vector3.forward);
				blockController.PitchBlock(forward);
			} else {
				return;
			}
			isRotatedX = true;
		}

		/// Yaw Block
		override
		protected void DetectRotationY() {
			if (isFingersFolded(hand)) return;
			if (hasTwoHands()) return;
			if (isRotatedY) return;
			if (yaw > rightScale) {
				blockController.YawBlock(1);
			} else if (yaw < leftScale) {
				blockController.YawBlock(-1);
			} else {
				return;
			}
			isRotatedY = true;
		}

		/// Roll Block
		override
		protected void DetectRotationZ() {
			if (isFingersFolded(hand)) return;
			if (hasTwoHands()) return;
			if (isRotatedZ) return;
			if (roll > counterClockwiseScale) {
				Vector3 left = GetMainCamera().TransformDirection(Vector3.left);
				blockController.RollBlock(left);
			} else if (roll < clockwiseScale) {
				Vector3 right = GetMainCamera().TransformDirection(Vector3.right);
				blockController.RollBlock(right);
			} else {
				return;
			}
			isRotatedZ = true;
		}

		override
		protected void DetectRotationCamera() {
			if (!hasTwoHands()) return;

			int depth = 80;

			if (hand.IsRight) {
				float rightHandZ = -hand.PalmPosition.z;
				float leftHandZ = -otherHand.PalmPosition.z;
				if (leftHandZ > depth && rightHandZ < -depth) {
					GetCameraController().RotateCam(-1);
				} else if (leftHandZ < -depth && rightHandZ > depth) {
					GetCameraController().RotateCam(1);
				}
			} else if (hand.IsLeft) {
				float leftHandZ = -hand.PalmPosition.z;
				float rightHandZ = -otherHand.PalmPosition.z;
				if (leftHandZ > depth && rightHandZ < -depth) {
					GetCameraController().RotateCam(-1);
				} else if (leftHandZ < -depth && rightHandZ > depth) {
					GetCameraController().RotateCam(1);
				}
			}
		}

		/// Press Block
		override
		protected void DetectPressMotion() {

		}

		/// Shake Block
		override
		protected void DetectShakeMotion() {

		}

	}
}
