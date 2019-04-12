using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace PrimordialSands
{
    public class OrcsAcquisition
    {
		public static int[] Invaders = {

        ModLoader.GetMod("PrimordialSands").NPCType("Orc")

        };
	
        public static int[] GetFullInvaderList()
		{
			int[] list = new int[Invaders.Length];
            Invaders.CopyTo(list, 0);		
			return list;
		}	
		public static void StartOrcsAcquisition()
		{
			if (Main.invasionType != 0 && Main.invasionSize == 0)
			{
				Main.invasionType = 0;
			}
			if (Main.invasionType == 0)
			{
				int num = 0;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && Main.player[i].statLifeMax >= 200)
					{
						num++;
					}
				}
				if (num > 0)
				{
					Main.invasionType = -1;
					PrimordialSandsWorld.OrcsAcquisitionUp = true;
					Main.invasionSize = 100 * num;
					Main.invasionSizeStart = Main.invasionSize;
					Main.invasionProgress = 0;
					Main.invasionProgressIcon = 0 + 3;
					Main.invasionProgressWave = 0;
					Main.invasionProgressMax = Main.invasionSizeStart;
					Main.invasionWarn = 360;
					if (Main.rand.Next(2) == 0)
					{
						Main.invasionX = 0.0;
						return;
					}
					Main.invasionX = (double)Main.maxTilesX;
				}
			}
		}
		public static void OrcsAcquisitionWarning()
		{
			String text = "";
			if (Main.invasionX == (double)Main.spawnTileX)
			{
				text = "An Orcin aquisition is in effect!";
			}
			if(Main.invasionSize <= 0)
			{
				text = "An Orcin army has been defeated!";
			}
			if (Main.netMode == 0)
			{
				Main.NewText(text, 175, 75, 255, false);
				return;
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f, 0, 0, 0);
			}
		}
		
		public static void UpdateOrcsAcquisition()
		{
			if(PrimordialSandsWorld.OrcsAcquisitionUp)
			{
				if(Main.invasionSize <= 0)
				{
					PrimordialSandsWorld.OrcsAcquisitionUp = false;
					OrcsAcquisitionWarning();
					Main.invasionType = 0;
					Main.invasionDelay = 0;
				}

				if (Main.invasionX == (double)Main.spawnTileX)
				{
					return;
				}
				
				float num = (float)Main.dayRate;
				if (Main.invasionX > (double)Main.spawnTileX)
				{
					Main.invasionX -= (double)num;
					if (Main.invasionX <= (double)Main.spawnTileX)
					{
						Main.invasionX = (double)Main.spawnTileX;
                        OrcsAcquisitionWarning();
					}
					else
					{
						Main.invasionWarn--;
					}
				}
				else
				{
					if (Main.invasionX < (double)Main.spawnTileX)
					{
						Main.invasionX += (double)num;
						if (Main.invasionX >= (double)Main.spawnTileX)
						{
							Main.invasionX = (double)Main.spawnTileX;
                            OrcsAcquisitionWarning();
						}
						else
						{
							Main.invasionWarn--;
						}
					}
				}
			}
		}
		
		public static void CheckOrcsAcquisitionProgress()
		{
			int[] FullList = GetFullInvaderList();
			
			if (Main.invasionProgressMode != 2)
			{
				Main.invasionProgressNearInvasion = false;
				return;
			}
			bool flag = false;
			Player player = Main.player[Main.myPlayer];
			Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
			int num = 5000;
			int icon = 0;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active)
				{
					icon = 0;
					int type = Main.npc[i].type;
					for(int n = 0; n < FullList.Length; n++)
					{
						if(type == FullList[n])
						{
							Rectangle value = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value))
							{
								flag = true;
								break;
							}
						}
					}
				}
			}
			Main.invasionProgressNearInvasion = flag;
			int progressMax3 = 1;
			if (PrimordialSandsWorld.OrcsAcquisitionUp)
			{
				progressMax3 = Main.invasionSizeStart;
			}
			if(PrimordialSandsWorld.OrcsAcquisitionUp && (Main.invasionX == (double)Main.spawnTileX))
			{
				Main.ReportInvasionProgress(Main.invasionSizeStart - Main.invasionSize, progressMax3, icon, 0);
			}
			
			foreach(Player p in Main.player)
			{
                NetMessage.SendData(78, p.whoAmI, -1, null, Main.invasionSizeStart - Main.invasionSize, (float)Main.invasionSizeStart, (float)Main.invasionType -1f, 0f, 0, 0, 0);
            }
        }
    }
}
