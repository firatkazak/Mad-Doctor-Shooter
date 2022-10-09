using UnityEngine;
public class EnemyDamageArea : MonoBehaviour
{
    private float deactivateWaitTime = 0.1f;//Bekleme Süresini Devre Dışı Bırakmak için.
    private float deactivateTimer;//Zamanlayıcıyı devre dışı bırakmak için.
    private float damageAmount = 20.0f;//Hasar değeri.
    private bool canDealDamage;//Hasar Verebilir mi?
    public LayerMask playerLayer;// LayerMask tanımladık.
    private PlayerHealth playerHealth;// PlayerHealt tanımladık.
    private void Awake()
    {
        playerHealth = GameObject.FindWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerHealth>();//Komponent atama işlemi.
        gameObject.SetActive(false);//Başlangıçta nesneyi false yaptık.
    }
    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 1f, playerLayer))
        //Eğer doktor ile gardiyanın konumu arasında yarıçapı 1f'lik layer var ise
        {//ve
            if (canDealDamage)//Hasar Verebilir ise
            {
                canDealDamage = false;//Hasar Verebilir mi değişkenini false yap.
                playerHealth.TakeDamage(damageAmount);
                //playerHealt script'indeki TakeDamage fonksiyonunu damageAmount(20) kadar düşür.(100 ise 80)
                //Mantık: canDealDamage'ı false yapıp takedamage fonskiyonunu çalıştırınca hemen ölmüyoruz 20 birim can gidiyor sadece.
            }
        }
        DeactivateDamageArea();//Hasar Alanını Devre Dışı Bırak.
        //Hasar verilemeyecek durumda hasar verme, ancak hasar verilebilir durumdaysa her vuruşta 20'şer can düş.
    }
    private void DeactivateDamageArea()
    {
        if (Time.time > deactivateTimer)//Eğer zaman deactiveTimer'dan büyük ise
            gameObject.SetActive(false);//game objesini false yap.
        //Bu kod aktif iken Doktor'a zarar vermeyecek.
    }
    public void ResetDeactivateTimer()
    {
        //DeactivateDamageArea fonksiyonunda hasar verme işini false yapmıştık. resetleyince haliyle true yapmamız lazım.
        canDealDamage = true;//Hasar Verebilir mi değişkenini true yap.
        deactivateTimer = Time.time + deactivateWaitTime;//Mevcut zamana 0.1 sn ekle, deactivateTimer'a ata.
    }
}//Class EnemyDamageArea
