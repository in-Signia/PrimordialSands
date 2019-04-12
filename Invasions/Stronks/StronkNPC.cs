using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace PrimordialSands
{
	public class StronkNPC : GlobalNPC
	{	
		public override void EditSpawnPool(IDictionary< int, float > pool, NPCSpawnInfo spawnInfo)
        {
			if(PrimordialSandsWorld.OrcsAcquisitionUp && (Main.invasionX == (double)Main.spawnTileX))
			{
				pool.Clear();
                if (NPC.downedBoss2)
                {
                    foreach (int i in OrcsAcquisition.Invaders)
                    {
                        pool.Add(i, 1f);
                    }
                }

                else
                {
                    foreach (int i in OrcsAcquisition.Invaders)
                    {
                        pool.Add(i, 1f);
                    }
                }
			}
		}
		
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if(PrimordialSandsWorld.OrcsAcquisitionUp && (Main.invasionX == (double)Main.spawnTileX))
			{
				spawnRate = 1;
				maxSpawns = 10;
			}
		}
		
		public override void PostAI(NPC npc)
		{
			if(PrimordialSandsWorld.OrcsAcquisitionUp && (Main.invasionX == (double)Main.spawnTileX))
			{
				npc.timeLeft = 1000;
			}
		}
		
		public override void NPCLoot(NPC npc)
		{
			if(PrimordialSandsWorld.OrcsAcquisitionUp)
			{
				int[] FullList = OrcsAcquisition.GetFullInvaderList();
				foreach(int invader in FullList)
				{
					if(npc.type == invader)
					{
						Main.invasionSize -= 1;
					}
				}
			}
		}
	}
}