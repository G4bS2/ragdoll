using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] Collider myColider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool bIsRagdoll = false;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"colisao detectada com:" +
            $"- tag: {collision.gameObject.tag} - bIsRagdoll: " +
            $"{bIsRagdoll}");

        if (collision.gameObject.CompareTag("Projectile")
            && !bIsRagdoll)
        {
            Debug.Log("Colidiu com projetil");
            ToggleRagdoll(false);
            StartCoroutine(GetBackUp());
        }
    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
    }


    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;
        myColider.enabled = bisAnimating;

        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            Debug.Log($"{ragdollBone} isKinematic  =" +
                $"{ragdollBone.isKinematic}");
        }

        foreach(Rigidbody ragdollBone in rigidbodies){
            ragdollBone.isKinematic = bisAnimating; }

        GetComponent<Animator>().enabled = bisAnimating;
        if (bisAnimating) { RandomAnimation(); }
    }

    void RandomAnimation()
    {
        int randomNum = UnityEngine.Random.Range(0, 2);
        Debug.Log(randomNum);
        Animator animator = GetComponent<Animator>();

        if (randomNum == 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
