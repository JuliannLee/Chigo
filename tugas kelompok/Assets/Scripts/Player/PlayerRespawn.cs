using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("Health component not found on the player.");
        }
    }

    public void Respawn()
    {
        if (currentCheckpoint == null)
        {
            Debug.LogWarning("Current checkpoint is not set. Cannot respawn.");
            return;
        }

        // Restore player health and reset animation
        playerHealth.Respawn();

        // Move player to checkpoint location
        transform.position = currentCheckpoint.position;

        // Move the camera to the checkpoint's room
        Transform checkpointRoom = currentCheckpoint.parent;
        if (checkpointRoom == null)
        {
            Debug.LogError("Checkpoint room transform is null. Check if the checkpoint has a parent transform.");
            return;
        }

        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController != null)
        {
            cameraController.MoveToNewRoom(checkpointRoom);
        }
        else
        {
            Debug.LogError("CameraController component not found on the main camera.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpoint);

            // Disable the checkpoint's collider
            Collider2D checkpointCollider = collision.GetComponent<Collider2D>();
            if (checkpointCollider != null)
            {
                checkpointCollider.enabled = false;
            }
            else
            {
                Debug.LogWarning("Collider2D component not found on the checkpoint.");
            }

            // Trigger the checkpoint's animation
            Animator checkpointAnimator = collision.GetComponent<Animator>();
            if (checkpointAnimator != null)
            {
                checkpointAnimator.SetTrigger("appear");
            }
            else
            {
                Debug.LogWarning("Animator component not found on the checkpoint.");
            }
        }
    }
}
