using UnityEngine;
using Photon.Pun;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool escape;
		public bool sprint;
		public bool vrtoggle;
		

		[SerializeField] bool isSolo = false;
		PhotonView PV;

		[Header("Movement Settings")]
		public bool analogMovement;

		private void Awake() 
		{
			if (!isSolo)
			{
				PV = GetComponent<PhotonView>();
			}
		}
		private void Start() {
			if(!isSolo)
			{	
				if (!PV.IsMine)
				{
					return;
				}
			}
		}

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif
		
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnEscape(InputValue value)
		{
			EscapeInput(value.isPressed);
		}
		public void OnVrtoggle(InputValue value)
		{
			VrtoggleInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void EscapeInput(bool newEscapeState)
		{
			escape = newEscapeState;
			// if (!isSolo)
			// {
			// 	if (PV.IsMine)
			// 	{
			// 		FirstPersonController.Instance.showPauseMenu();
			// 	}
			// }else{
			// 	FirstPersonController.Instance.showPauseMenu(escape);
			// }
		}
		public void VrtoggleInput(bool newVrtoggleState)
		{
			vrtoggle = newVrtoggleState;
			FirstPersonController.Instance.vrToggle();
			// if (!isSolo)
			// {
			// }else{
			// 	FirstPersonController.Instance.vrToggle();
			// }
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}