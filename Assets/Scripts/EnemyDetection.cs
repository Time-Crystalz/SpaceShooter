using System;

using UnityEngine;

public class EnemyDetection : Enemy
{
    private Enemy enemy;
    private enmeyShooter shooter;
    private player playerBody;
    
    public event EventHandler StopOnplayerLoacted;
    public event EventHandler<OnPlayerLocatedEventArgs> OnPlayerLocatedEvent;

    public class OnPlayerLocatedEventArgs : EventArgs
    {
        public Vector3 playerPosition;
    }
    public event  EventHandler MoveOnplayerLost;

    private void Awake()
    {
         shooter = GetComponentInParent<enmeyShooter>();
    }

    private void Update()
    {
       
    }
    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        other.TryGetComponent<player>(out  playerBody);
        if (playerBody)
        {
            
            shooter.playerBody = playerBody;
            OnPlayerLocated(playerBody);
            
        }
        
        

    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent<player>(out playerBody);
        if (playerBody)
        {
            playerBody = null;
            shooter.playerBody = null;
            OnplayerLost();
            
        }
    }

    public override void OnPlayerLocated(player player)
    {
        
        if (shooter)
        {
            
            shooter.isFiring = true;
            StopOnplayerLoacted?.Invoke(this,EventArgs.Empty);  
            OnPlayerLocatedEvent.Invoke(this,new OnPlayerLocatedEventArgs()
            {
                playerPosition = player.transform.position
            });
        }
    }

   
    private void OnplayerLost()
    {   
        shooter.isFiring = false;
        MoveOnplayerLost?.Invoke(this,EventArgs.Empty);
    }


}
