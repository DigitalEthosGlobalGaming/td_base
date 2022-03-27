
using Degg.Utils;
using Sandbox;
using System;
using System.Collections.Generic;

namespace Degg.TDBase
{
	public partial class RoundBase
	{
		public bool IsStarted { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual float SpawnInterval { get; set; }
		public TDPlayerMap Map { get; set; }
		public Queue<Type> Enemies { get; set; } = new Queue<Type>();

		public List<EnemyBase> EnemyEntities { get; set; } = new List<EnemyBase>();

		public Timer EnemySpawner { get; set; }


		public void Start()
		{
			if ( IsStarted )
			{
				return;
			}

			IsStarted = true;
			EnemySpawner = new Timer( SpawnEnemy, SpawnInterval );
			EnemySpawner.Start();
		}

		public void Stop()
		{
			if ( EnemySpawner != null )
			{
				EnemySpawner.Delete();
			}
		}

		public virtual bool HasRoundEnded()
		{
			return AreAllDead();
		}

		public bool AreAllDead()
		{
			var isAllDead = true;

			if ( Enemies.Count > 0)
			{
				return false;
			}

			foreach ( var enemy in EnemyEntities )
			{
				if (enemy != null && enemy.IsValid && enemy.IsAlive)
				{
					isAllDead = false;
				}
			}

			return isAllDead;
		}

		public void Delete()
		{
			Stop();
		}
		public void AddToQueue<T>() where T : EnemyBase, new()
		{
			if ( Enemies != null )
			{
				Enemies.Enqueue( typeof( T ) );
			}
		}


		public void SpawnEnemy( Timer t = null )
		{
			if ( Enemies != null )
			{
				if ( Enemies.TryDequeue( out var myEnemy ) )
				{
					var enemy = Library.Create<EnemyBase>( myEnemy );
					enemy.Map = Map;
					enemy.Round = this;
					enemy.Setup();
					EnemyEntities.Add( enemy );
					Map.EnemyEntities.Add( enemy );
				}
			}

		}
	}
}
