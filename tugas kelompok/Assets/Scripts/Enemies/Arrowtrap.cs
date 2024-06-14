using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arrowtrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;    // Waktu jeda antara serangan
    [SerializeField] private Transform firePoint;     // Titik dimana panah akan ditembakkan
    [SerializeField] private GameObject[] arrows;     // Array objek panah yang akan ditembakkan
    private float cooldownTimer;                      // Penghitung waktu jeda serangan
    private Queue<GameObject> arrowQueue;             // Antrian panah yang tersedia

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Awake()
    {
        // Inisialisasi antrian panah
        arrowQueue = new Queue<GameObject>(arrows);
    }

    // Method untuk melakukan serangan
    private void Attack()
    {
        cooldownTimer = 0; // Reset penghitungan jeda serangan
        SoundManager.instance.PlaySound(arrowSound);

        // Cek apakah ada panah yang tersedia di antrian
        if (arrowQueue.Count > 0)
        {
            GameObject arrow = arrowQueue.Dequeue(); // Ambil panah dari antrian
            arrow.transform.position = firePoint.position; // Set posisi panah ke titik tembakan
            arrow.GetComponent<EnemyProjectile>().ActivateProjectile(); // Aktifkan panah
            StartCoroutine(RequeueArrow(arrow)); // Masukkan kembali panah ke antrian setelah delay
        }
    }

    // Coroutine untuk memasukkan kembali panah ke antrian setelah tidak aktif
    private IEnumerator RequeueArrow(GameObject arrow)
    {
        yield return new WaitUntil(() => !arrow.activeInHierarchy);
        arrowQueue.Enqueue(arrow); // Masukkan kembali panah ke antrian
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // Tambah waktu jeda dengan waktu frame saat ini

        // Jika waktu jeda sudah mencapai atau melebihi waktu jeda serangan, lakukan serangan
        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
