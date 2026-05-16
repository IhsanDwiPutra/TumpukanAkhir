using UnityEngine;

public class OxygenTimer : MonoBehaviour
{
    [Header("Pengaturan Oksigen")]
    // 3 Menit = 180 detik
    public float timeRemaining = 180f; 
    public bool timerIsRunning = false;

    [Header("Referensi")]
    public UIManager uiManager;
    public PlayerController playerController; // Untuk mematikan pergerakan saat mati
    public GameObject blackScreenUI; // Panel UI untuk layar gelap

    private void Start()
    {
        // Memulai timer secara otomatis saat game jalan
        timerIsRunning = true; 
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Mengurangi waktu berdasarkan frame Unity yang berjalan
                timeRemaining -= Time.deltaTime; 
                
                // Panggil UI Manager untuk update teks di layar
                if (uiManager != null)
                {
                    uiManager.UpdateOxygenText(timeRemaining);
                }
            }
            else
            {
                Debug.Log("Oksigen habis! Memicu Failure Ending.");
                timeRemaining = 0;
                timerIsRunning = false;
                
                // Pastikan teks menunjukkan angka 00:00 secara tepat
                if (uiManager != null)
                {
                    uiManager.UpdateOxygenText(timeRemaining);
                }

                TriggerFailureEnding();
            }
        }
    }

    // Fungsi ini dipanggil oleh PuzzleManager jika urutan salah
    public void ReduceTime(float penaltyTime)
    {
        timeRemaining -= penaltyTime;
        Debug.Log("Penalti! Waktu berkurang " + penaltyTime + " detik.");
        // TODO Nanti: Tambahkan efek visual kamera bergetar (Camera Shake)
    }

    private void TriggerFailureEnding()
    {
        Debug.Log("Oksigen habis! Layar gelap total.");
        
        // Mematikan kontrol pemain
        if (playerController != null) playerController.enabled = false;
        
        // Menampilkan layar hitam pekat
        if (blackScreenUI != null) blackScreenUI.SetActive(true);
    }
}