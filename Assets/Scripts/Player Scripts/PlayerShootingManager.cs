using UnityEngine;
public class PlayerShootingManager : MonoBehaviour
{
    public GameObject bulletPrefab;//Mermi prefabını sürükleyip bırakacağımız yer.
    public Transform bulletSpawnPos;//Merminin spawnlanacağı yer.
    public void Shoot(float facingDirection)
    {
     GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPos.position,Quaternion.identity);
     //newBullet adında mermi spawnlamak için kod.bulletPrefabınının pozisyonu ve açısında olacak.
     if(facingDirection < 0)//Yönü sola bakıyorsa.
     newBullet.GetComponent<Bullet>().SetNegativeSpeed();//Merminin sol tarafa giderken çalışacağı fonksiyon.
     SoundManager.instance.PlayShootSound();//SoundManager scriptindeki PlayShootSound fonksiyonunu çalıştır.(Ateş sesi.)
    }
}//Class PlayerShootingManager
