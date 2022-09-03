using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;

	GameObject controller;

	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		if(PV.IsMine)
		{
			CreateController();
		}
	}

	void CreateController()
	{
		// Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        Vector3 position = new Vector3(0f, 0f, 0f);
		controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), position, Quaternion.identity);
        Debug.Log("tesCreateController"+controller.ToString());
	}

	public void Die()
	{
		PhotonNetwork.Destroy(controller);
		CreateController();
	}
}