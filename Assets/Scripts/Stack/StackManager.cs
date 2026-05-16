using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [Header("Pengaturan Stack")]
    public int maxStack = 6; 
    
    private Stack<GameObject> fuseStack = new Stack<GameObject>();

    [Header("Referensi")]
    public UIManager uiManager;
    public PuzzleManager puzzleManager; // Tambahkan referensi ke PuzzleManager

    // Fungsi PUSH
    public bool PushFuse(GameObject newFuse)
    {
        if (fuseStack.Count < maxStack)
        {
            fuseStack.Push(newFuse); 
            
            // Mengaktifkan kembali objek secara visual dan menjadikannya anak dari Panel
            newFuse.SetActive(true);
            newFuse.transform.SetParent(transform);
            
            // TODO: Atur posisi visual tumpukannya nanti
            newFuse.transform.localPosition = new Vector3(0, fuseStack.Count * 0.2f, -0.5f); 

            // Panggil UIManager setelah PUSH
            if (uiManager != null) uiManager.UpdateStackDisplay(fuseStack);

            Debug.Log("PUSH berhasil! Arus atas sekarang: " + newFuse.GetComponent<FuseObject>().fuseColor);

            // Panggil PuzzleManager untuk mengecek urutan
            if (puzzleManager != null) 
            {
                puzzleManager.CheckPuzzle(fuseStack);
            }

            return true;
        }
        else
        {
            Debug.LogWarning("Stack Overload! Panel Penuh.");
            return false;
        }
    }

    // Fungsi POP (Sekarang mengembalikan GameObject)
    public GameObject PopFuse()
    {
        if (!IsEmpty())
        {
            GameObject removedFuse = fuseStack.Pop(); 

            // Panggil UIManager setelah POP
            if (uiManager != null) uiManager.UpdateStackDisplay(fuseStack);

            Debug.Log("POP ditarik: " + removedFuse.name);
            return removedFuse;
        }
        else
        {
            Debug.LogWarning("Stack Kosong! Tidak ada yang bisa di-POP.");
            return null;
        }
    }

    public bool IsEmpty()
    {
        return fuseStack.Count == 0; 
    }
}