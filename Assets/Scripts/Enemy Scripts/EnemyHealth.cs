using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;//Düþmanýn caný.
    private Enemy enemyScript;//Enemy scriptine ulaþmak için.
    public Slider enemyHealthSlider;//Düþmanýn can çubuðu.
    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();//enemyScript'e Enemy script(class)ini atadýk.
    }
    public void TakeDamage(float damageAmount)
    {
        if (health <= 0)//Caný 0 ya da daha az ise,
            return;//Döndür.Mantýk: Düþmanýn caný -5 vb. olduðunda oyun mantýk hatasý vermeden fonksiyon çalýþmaya devam edecek.
        health -= damageAmount;//damageAmount kadar(20 birim) düþmanýn canýndan azaltýp ona ata. yani 100 iken 1 kere vurdu mu 35 azalacak can 65 olacak.
        if (health <= 0f)//Düþman caný 0 ya da daha düþük ise.
        {
            health = 0;//Caný 0 yap.(35 vuruyor mesela 100'e 3 kere vursa -5 olur ama mantýksal deðil.Bunun için 0 gösteriyoruz.)
            enemyScript.EnemyDied();//Enemy scriptinde tanýmladýðýmýz EnemyDied fonksiyonunu çalýþtýr.
            EnemySpawner.instance.EnemyDied(gameObject);//instance edilmiþ EnemySpawner'daki EnemyDied fonksiyonunu çalýþtýr.
            GamePlayController.instance.EnemyKilled();//instance edilmiþ GamePlayController'daki EnemyKilled fonksiyonunu çalýþtýr.
        }
        enemyHealthSlider.value = health;//Düþman Health'ini Düþmanýn can cubuðuna atadýk. Can 100 ise çubuk full dolu. 65 ise 3te1'i dolu vb.
    }
}//Class EnemyHealth
