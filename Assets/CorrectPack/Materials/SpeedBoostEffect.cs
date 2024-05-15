using UnityEngine;

public class SpeedBoostEffect : MonoBehaviour
{
    public Material mat;
    public float clipBoost;
    public float clipStand;
    public ParticleSystem ps;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        mat.SetFloat("_clip", clipStand);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            mat.SetFloat("_clip", clipBoost);
        }
        else
        {
            mat.SetFloat("_clip", clipStand);
        }
        if (!_characterController.isGrounded)
        {
            ps.Play();
            Debug.Log("стою же");
        }
    }
}
