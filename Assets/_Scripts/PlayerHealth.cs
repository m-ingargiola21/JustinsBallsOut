using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    public float m_StartingHealth = 100f;          
    
    private float m_CurrentHealth;  
    private bool m_Dead;            
    
    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }

    public void TakeDamage()
    {
        if (!isServer)
            return;

        m_CurrentHealth -= m_CurrentHealth;
        
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            RpcOnDeath();
        }
    }
    
    [ClientRpc]
    private void RpcOnDeath()
    {
        if(isLocalPlayer)
        {
            m_Dead = true;
            gameObject.SetActive(false);
        }        
    }
}