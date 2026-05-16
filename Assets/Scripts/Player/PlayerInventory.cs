using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Pengaturan Inventaris")]
    public int maxCapacity = 2;
    public List<FuseObject> carriedFuses = new List<FuseObject>();

    [Header("Referensi")]
    public UIManager uiManager; // Referensi ke script UIManager

    void Start()
    {
        // Update UI pertama kali saat game dimulai (0/2)
        if (uiManager != null)
        {
            uiManager.UpdateFuseCount(carriedFuses.Count, maxCapacity);
        }
    }

    public bool AddFuse(FuseObject newFuse)
    {
        if (carriedFuses.Count < maxCapacity)
        {
            carriedFuses.Add(newFuse);
            newFuse.OnPickedUp();
            
            // Panggil UIManager untuk memperbarui teks di layar
            if (uiManager != null)
            {
                uiManager.UpdateFuseCount(carriedFuses.Count, maxCapacity);
            }
            
            return true;
        }
        else
        {
            Debug.LogWarning("Inventaris penuh!");
            return false;
        }
    }

    public FuseObject GetFuseToPush()
    {
        if (carriedFuses.Count > 0)
        {
            FuseObject fuseToUse = carriedFuses[carriedFuses.Count - 1];
            carriedFuses.RemoveAt(carriedFuses.Count - 1);
            
            // Panggil UIManager untuk memperbarui teks di layar setelah fuse dikurangi
            if (uiManager != null)
            {
                uiManager.UpdateFuseCount(carriedFuses.Count, maxCapacity);
            }
            
            return fuseToUse;
        }
        return null;
    }
}