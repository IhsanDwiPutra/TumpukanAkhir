using UnityEngine;

public class FuseObject : MonoBehaviour
{
    [Header("Data Fuse")]
    public string fuseColor = "Merah"; // Clue warna untuk puzzle
    public int fuseID = 1;

    // Fungsi ini dipanggil saat pemain berhasil mengambil fuse
    public void OnPickedUp()
    {
        Debug.Log("Fuse " + fuseColor + " diambil!");
        // Menonaktifkan objek dari scene (seolah-olah masuk ke kantong)
        gameObject.SetActive(false); 
    }
}