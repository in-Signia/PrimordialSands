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
	public class ClaymoreCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Claymore Cooldown");
			Description.SetDefault("You must wait before countering an attack");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<PrimordialSandsPlayer>(mod).claymoreCooldown = true;
		}
	}
}
