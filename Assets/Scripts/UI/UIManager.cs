using UnityEngine;
using TMPro;
using System.Collections.Generic; // Dibutuhkan untuk membaca Stack

public class UIManager : MonoBehaviour
{
    [Header("Referensi UI")]
    public TextMeshProUGUI fuseCountText;
    public TextMeshProUGUI oxygenText;

    [Header("Visualisasi LIFO Stack")]
    // Array untuk menampung 6 teks UI dari atas ke bawah
    public TextMeshProUGUI[] stackUIElements; 

    public void UpdateFuseCount(int currentAmount, int maxCapacity)
    {
        fuseCountText.text = "Fuse: " + currentAmount + "/" + maxCapacity;
    }

    public void UpdateOxygenText(float timeRemaining)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60); 
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        oxygenText.text = string.Format("Oksigen: {0:00}:{1:00}", minutes, seconds);
    }

    // Fungsi baru untuk merender isi Stack secara real-time
    public void UpdateStackDisplay(Stack<GameObject> currentStack)
    {
        // 1. Reset seluruh teks menjadi status kosong
        foreach (var textUI in stackUIElements)
        {
            textUI.text = "[ Kosong ]";
            textUI.color = Color.gray; // Warna redup
        }

        // 2. C# Stack.ToArray() secara otomatis membaca data dari TOP ke BOTTOM
        GameObject[] stackArray = currentStack.ToArray();

        // 3. Render data dari posisi TOP ke dalam UI
        for (int i = 0; i < stackArray.Length; i++)
        {
            // Ambil warna dari script FuseObject
            string warnaFuse = stackArray[i].GetComponent<FuseObject>().fuseColor;
            
            // Ubah teks UI
            stackUIElements[i].text = "FUSE: " + warnaFuse.ToUpper();
            
            // Berikan warna teks berbeda untuk elemen TOP (indeks 0) sebagai penekanan edukasi
            if (i == 0)
            {
                stackUIElements[i].color = Color.yellow; // TOP Stack
                stackUIElements[i].text += " (TOP)";
            }
            else
            {
                stackUIElements[i].color = Color.white;
            }
        }
    }
}