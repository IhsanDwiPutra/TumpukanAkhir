using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactRange = 3.0f;
    public LayerMask interactableLayer;
    public Transform playerCamera;
    public PlayerInventory playerInventory; 

    void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * interactRange, Color.green);

            // Cek jika menatap FuseObject (Sekering di lantai)
            FuseObject targetFuse = hit.collider.GetComponent<FuseObject>();
            if (targetFuse != null && Input.GetKeyDown(KeyCode.E))
            {
                playerInventory.AddFuse(targetFuse);
                return; // Keluar dari kondisi agar tidak tumpang tindih
            }

            // Cek jika menatap StackManager (Panel Listrik)
            StackManager panel = hit.collider.GetComponent<StackManager>();
            if (panel != null)
            {
                // Aksi PUSH (Tekan E)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    FuseObject fuseToPush = playerInventory.GetFuseToPush();
                    if (fuseToPush != null)
                    {
                        // Jika berhasil PUSH ke panel, lepas dari inventory
                        panel.PushFuse(fuseToPush.gameObject);
                    }
                    else
                    {
                        Debug.Log("Inventaris kosong! Tidak ada fuse untuk di-PUSH.");
                    }
                }
                
                // Aksi POP (Tekan Q)
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    GameObject poppedObject = panel.PopFuse();
                    if (poppedObject != null)
                    {
                        // Kembalikan ke inventaris
                        FuseObject poppedFuseData = poppedObject.GetComponent<FuseObject>();
                        if (!playerInventory.AddFuse(poppedFuseData))
                        {
                            // Jika kantong penuh tapi maksa POP, jatuh ke lantai
                            poppedObject.transform.SetParent(null);
                            Debug.Log("Kantong penuh! Fuse jatuh ke lantai.");
                        }
                    }
                }
            }
        }
    }
}