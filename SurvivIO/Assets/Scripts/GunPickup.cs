using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject gunObj;
    private Gun gun;

    private void Start()
    {
        player = GameManager.Instance._player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gun = gunObj.GetComponent<Gun>();

        Inventory inventory = collision.GetComponent<Inventory>();
        if (inventory != null)
        {

            inventory.PickUpWeapon(gun, gunObj);

            Destroy(this.gameObject);
        }
    }
}
