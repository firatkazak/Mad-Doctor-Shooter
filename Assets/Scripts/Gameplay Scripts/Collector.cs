using UnityEngine;
public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
     if(other.CompareTag(TagManager.BULLET_TAG))
     Destroy(other.gameObject);
     //1 tane Collector isminde görünmez nesne koyduk. Ona 2 tane box collider ekledik.
     //O colliderları sağ ve sol sınırlarımıza koyduk.Mermi onlara çarpınca silindi.
    }
}//Collector
