using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float moveSpeed = 15.0f;//Merminin gidiş hızı.
    private float damageAmount = 35.0f;//Hasar miktarı.
    private Vector3 moveVector = Vector3.zero;//Hareket yönü.
    private Vector3 tempScale;//Geçici skala.
    private void Update()
    {
     MoveBullet();//Move bullet fonksiyonunu sürekli çalıştıracak.
    }
    private void MoveBullet()
    {
     moveVector.x = moveSpeed * Time.deltaTime;//x düzleminde move speed hızında gidecek.
     transform.position += moveVector;//moveVector'ü merminin mevcut pozisyonuna ata.
    }
    public void SetNegativeSpeed()
    {
     moveSpeed *= -1f;//moveSpeed'i -1 ile çarpıp - yaptık.
     tempScale = transform.localScale;//localScale'i tempScale'e atadık.
     tempScale.x = -tempScale.x;//negatif tempScale.x'i tempScale.x'e atadık.
     transform.localScale = tempScale;//tempScale'i localScale'e atadık.
     //bu yöntem ile mermi sol(-,negatif) yöne gidebilecek.
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
     if(other.CompareTag(TagManager.ENEMY_TAG))//Mermi düşmana temas ederse
     {
      other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
      //EnemyHealth scriptindeki TakeDamage fonksiyonunu damageAmount(35) kadar çalıştır.
      //Yani düşmana mermi isabet ettiğinde düşmanın canı 35 birim azalacak.100-35=65 gibi.
      Destroy(gameObject);//Düşmana temas edince mermi yok olacak ekrandan.
     }
    }
}//Class Bullet
