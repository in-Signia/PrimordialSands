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
	public class Splintered : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Splintered");
			Description.SetDefault("You have been impaled with splinters, cuts defense by 1/4");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<PrimordialSandsPlayer>(mod).splintered = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<_GlobalNPC>(mod).splintered = true;
		}
	}
}
