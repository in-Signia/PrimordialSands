using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using PrimordialSands.NPCs;

namespace PrimordialSands.Buffs
{
	public class Flood : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Flood");
			Description.SetDefault("Your blood is tainted with flood");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<PrimordialSandsPlayer>(mod).flood = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<_GlobalNPC>(mod).flood = true;
		}
	}
}
