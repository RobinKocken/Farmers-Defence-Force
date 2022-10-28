using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastPlayer : MonoBehaviour
{
    [Header("PickUp")]
    public KeyCode interact;
    public float distance;
    RaycastHit hit;

    [Header("Blueprint")]
    public GameObject cam;
    public LayerMask placeble;

    public Item prefab;
    MeshRenderer[] currentPrefabMeshRenderers;
    Material[] currentPrefabDefaultMaterials;

    public GameObject move;

    public float distanceBuild;
    RaycastHit hitBuild;

    public float mouseWheel;

    public Material canPlace, cantPlace;
    public GameObject placeParticlePrefab;

    [Header("Gas")]
    public InventoryManager inventory;

    [Header("Gas")]
    bool shopping;

    int bulletSlot;

    void Update()
    {
        Ray();
        Blueprint();
    }

    void Ray()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        if(Physics.Raycast(ray, out hit, distance))
        {
            if(Input.GetKeyDown(interact) && hit.transform.CompareTag("Item"))
            {
                if(hit.transform.GetComponent<PickupItem>())
                {
                    hit.transform.GetComponent<PickupItem>().PickUp();
                }
            }
            else if(Input.GetKeyDown(interact) && hit.transform.CompareTag("Turret"))
            {
                if(hit.transform.GetComponent<SingleShotScript>())
                {
                    bulletSlot = inventory.CheckForItem(hit.transform.GetComponent<SingleShotScript>().iDBullet, bulletSlot);

                    if(bulletSlot != -1)
                    {
                        if(inventory.slots[bulletSlot].GetComponent<Slot>().itemData != null)
                        {
                            inventory.slots[bulletSlot].GetComponent<Slot>().amount = hit.transform.GetComponent<SingleShotScript>().ReloadAmmo(inventory.slots[bulletSlot].GetComponent<Slot>().amount);
                        }
                    }

                    if(inventory.currentGas > 0)
                    {
                        inventory.currentGas = hit.transform.GetComponent<SingleShotScript>().ReloadGas(inventory.currentGas);
                    }
                }
            }
            else if(Input.GetKey(interact) && hit.transform.CompareTag("Gas"))
            {
                inventory.AddGas();
            }
        }
    }

    void Blueprint()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hitBuild, distanceBuild, placeble))
        {
            if(move != null)
            {
                move.transform.position = hitBuild.point;
                move.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitBuild.normal);

                mouseWheel += Input.mouseScrollDelta.y;
                move.transform.Rotate(Vector3.up, mouseWheel * 10f);

                if(Input.GetButtonDown("Fire1"))
                {
                    move.transform.position = hitBuild.point;
                    move.GetComponent<Turret>().enabled = true;

                    for (int i = 0; i < currentPrefabMeshRenderers.Length; i++)
                    {
                        currentPrefabMeshRenderers[i].sharedMaterial = currentPrefabDefaultMaterials[i];
                    }
                    Destroy(Instantiate(placeParticlePrefab, hitBuild.point, placeParticlePrefab.transform.rotation), 2.3f);
                    //move.transform.parent = null;
                    mouseWheel = 0;
                    move = null;

                    inventory.canScroll = true;
                }
            }
        }

        if(prefab != null)
        {
            inventory.canScroll = false;

            move = Instantiate(prefab.prefab);

            currentPrefabMeshRenderers = move.GetComponentsInChildren<MeshRenderer>();
            currentPrefabDefaultMaterials = new Material[currentPrefabMeshRenderers.Length];

            for (int i = 0; i < currentPrefabDefaultMaterials.Length; i++)
            {
                currentPrefabDefaultMaterials[i] = currentPrefabMeshRenderers[i].sharedMaterial;
                currentPrefabMeshRenderers[i].sharedMaterial = canPlace;
            }

            prefab = null;

            //move.transform.SetParent(this.gameObject.transform);
        }
    }
}
