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
    public class TreeEntMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Tree Ent");
            Description.SetDefault("A small creature, with much firepower... the tree ent will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            PrimordialSandsPlayer modPlayer = player.GetModPlayer<PrimordialSandsPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("TreeEntMinion")] > 0)
            {
                modPlayer.treeEnt = true;
            }
            if (!modPlayer.treeEnt)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}