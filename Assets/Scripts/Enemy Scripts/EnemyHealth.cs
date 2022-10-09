using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;//D��man�n can�.
    private Enemy enemyScript;//Enemy scriptine ula�mak i�in.
    public Slider enemyHealthSlider;//D��man�n can �ubu�u.
    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();//enemyScript'e Enemy script(class)ini atad�k.
    }
    public void TakeDamage(float damageAmount)
    {
        if (health <= 0)//Can� 0 ya da daha az ise,
            return;//D�nd�r.Mant�k: D��man�n can� -5 vb. oldu�unda oyun mant�k hatas� vermeden fonksiyon �al��maya devam edecek.
        health -= damageAmount;//damageAmount kadar(20 birim) d��man�n can�ndan azalt�p ona ata. yani 100 iken 1 kere vurdu mu 35 azalacak can 65 olacak.
        if (health <= 0f)//D��man can� 0 ya da daha d���k ise.
        {
            health = 0;//Can� 0 yap.(35 vuruyor mesela 100'e 3 kere vursa -5 olur ama mant�ksal de�il.Bunun i�in 0 g�steriyoruz.)
            enemyScript.EnemyDied();//Enemy scriptinde tan�mlad���m�z EnemyDied fonksiyonunu �al��t�r.
            EnemySpawner.instance.EnemyDied(gameObject);//instance edilmi� EnemySpawner'daki EnemyDied fonksiyonunu �al��t�r.
            GamePlayController.instance.EnemyKilled();//instance edilmi� GamePlayController'daki EnemyKilled fonksiyonunu �al��t�r.
        }
        enemyHealthSlider.value = health;//D��man Health'ini D��man�n can cubu�una atad�k. Can 100 ise �ubuk full dolu. 65 ise 3te1'i dolu vb.
    }
}//Class EnemyHealth
