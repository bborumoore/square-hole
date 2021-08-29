using UnityEngine;

public class OnCollision : MonoBehaviour
{
  public PlayerMovement m_char;
  private void OnCollisionEnter(Collision collision)
  {
      if (collision.transform.tag == "Player")
      {
          return;
      }
    //   m_char.OnCharacterCollerderHit(collision.collider);
  }
}
