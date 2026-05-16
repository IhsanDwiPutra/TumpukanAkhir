using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Pengaturan Puzzle")]
    // Karena Stack.ToArray() membaca dari atas ke bawah, 
    // urutan jawaban di Inspector nanti harus ditulis dari TOP ke BOTTOM.
    public string[] targetSequence; 
    public StackManager stackManager;
    public OxygenTimer oxygenTimer; // Referensi ke timer

    [Header("Status Game")]
    public bool isPuzzleSolved = false;

    [Header("Referensi Ending")]
    public GameObject escapeDoor; // Pintu keluar bunker

    // Fungsi ini akan dipanggil oleh StackManager setiap kali pemain memasukkan fuse
    public void CheckPuzzle(Stack<GameObject> currentStack)
    {
        // Jangan cek jika jumlah fuse belum mencapai target
        if (currentStack.Count != targetSequence.Length)
        {
            return; 
        }

        // Ubah stack menjadi array untuk mempermudah pengecekan (Index 0 adalah TOP)
        GameObject[] stackArray = currentStack.ToArray(); 
        bool isCorrect = true;

        for (int i = 0; i < targetSequence.Length; i++)
        {
            // Ambil data warna dari fuse yang ada di panel
            string fuseColor = stackArray[i].GetComponent<FuseObject>().fuseColor;
            
            // Bandingkan dengan kunci jawaban
            if (fuseColor != targetSequence[i])
            {
                isCorrect = false;
                break; // Hentikan loop jika ada satu saja yang salah
            }
        }

        if (isCorrect)
        {
            isPuzzleSolved = true;
            TriggerSuccessEnding();
        }
        else
        {
            TriggerError();
        }
    }

    private void TriggerSuccessEnding()
    {
        Debug.Log("Puzzle Selesai! Pintu Terbuka.");
        // TODO: Mainkan suara pintu bunker terbuka dan pemain bisa keluar

        // Hentikan timer karena pemain sudah aman
        if (oxygenTimer != null) oxygenTimer.timerIsRunning = false;

        // Menghilangkan atau membuka pintu (simulasi sederhana)
        if (escapeDoor != null) escapeDoor.SetActive(false); 
        
        // TODO: Mainkan suara engsel pintu besi
    }

    private void TriggerError()
    {
        Debug.LogWarning("Urutan Salah! Listrik Overload!");
        // TODO: Mainkan suara meledak, kurangi timer oksigen sebagai penalti

        // Memberikan penalti pengurangan waktu (misal: 30 detik)
        if (oxygenTimer != null) oxygenTimer.ReduceTime(30f);
        
        // TODO: Mainkan suara fuse meledak
    }
}