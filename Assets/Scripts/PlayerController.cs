using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

// public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
public class PlayerController : MonoBehaviourPunCallbacks
{
	// [SerializeField] Image healthbarImage;
	// [SerializeField] GameObject ui;

	// [SerializeField] GameObject cameraHolder;

	// [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

	// [SerializeField] Item[] items;

	// int itemIndex;
	// int previousItemIndex = -1;

	// float verticalLookRotation;
	// bool grounded;
	// Vector3 smoothMoveVelocity;
	// Vector3 moveAmount;

	// Rigidbody rb;

	PhotonView PV;

	const float maxHealth = 100f;
	float currentHealth = maxHealth;

	PlayerManager playerManager;

	void Awake()
	{
		// rb = GetComponent<Rigidbody>();
		PV = GetComponent<PhotonView>();

		// playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
	}

	void Start()
	{
		if(PV.IsMine)
		{
			// EquipItem(0);
		}
		else
		{
			Destroy(GetComponentInChildren<Camera>().gameObject);
		}
	}

	void Update()
	{
		if(!PV.IsMine)
			return;
		// for(int i = 0; i < items.Length; i++)
		// {
		// 	if(Input.GetKeyDown((i + 1).ToString()))
		// 	{
		// 		EquipItem(i);
		// 		break;
		// 	}
		// }

		// if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
		// {
		// 	if(itemIndex >= items.Length - 1)
		// 	{
		// 		EquipItem(0);
		// 	}
		// 	else
		// 	{
		// 		EquipItem(itemIndex + 1);
		// 	}
		// }
		// else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
		// {
		// 	if(itemIndex <= 0)
		// 	{
		// 		EquipItem(items.Length - 1);
		// 	}
		// 	else
		// 	{
		// 		EquipItem(itemIndex - 1);
		// 	}
		// }

		// if(Input.GetMouseButtonDown(0))
		// {
		// 	items[itemIndex].Use();
		// }
	}


}