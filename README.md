# Tumpukan Akhir

> 3D First-Person Psychological Horror Puzzle berbasis Struktur Data Stack (LIFO)

## Tentang Game

**Tumpukan Akhir** adalah game horror psikologis first-person yang dibuat sebagai proyek UAS Struktur Data dan pameran kampus UBSI Pontianak.

Game ini mengimplementasikan konsep **Stack (LIFO - Last In First Out)** secara langsung ke dalam gameplay puzzle interaktif.

Pemain terjebak di dalam bunker basement kampus yang terkunci otomatis dan harus menyusun sekering listrik dengan urutan tertentu untuk keluar sebelum oksigen habis.

---

# Genre

- Psychological Horror
- Puzzle
- Educational Game
- First-Person

---

# Fitur Utama

- Sistem Stack (PUSH, POP, TOP, ISEMPTY)
- Puzzle berbasis logika LIFO
- Atmosfer horror psikologis
- Timer oksigen
- Sistem flashlight dengan baterai terbatas
- Inventory fuse terbatas
- Visualisasi struktur data secara interaktif

---

# Gameplay

Pemain harus:

1. Menjelajahi basement
2. Mencari fuse
3. Membaca petunjuk urutan
4. Menyusun fuse ke panel
5. Menggunakan konsep Stack untuk memperbaiki kesalahan
6. Bertahan sebelum oksigen habis

---

# Implementasi Struktur Data

## Stack (LIFO)

Game menggunakan struktur data Stack sebagai mekanik utama.

### Operasi yang digunakan:

| Operasi | Fungsi |
|---|---|
| PUSH | Menambahkan fuse |
| POP | Mengambil fuse teratas |
| TOP | Mengecek fuse paling atas |
| ISEMPTY | Mengecek stack kosong |

---

# Teknologi

| Teknologi | Digunakan Untuk |
|---|---|
| Unity URP | Game Engine |
| C# | Programming |
| Blender | 3D Modeling |
| Visual Studio Code | IDE |
| Audacity | Audio Editing |

---

# Struktur Folder

```text
Assets/
│
├── Scripts/
│   ├── Player/
│   ├── Stack/
│   ├── Puzzle/
│   ├── Horror/
│   └── UI/
│
├── Prefabs/
├── Models/
├── Materials/
├── Audio/
├── Animations/
├── Scenes/
└── UI/