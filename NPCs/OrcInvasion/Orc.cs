using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.NPCs.OrcInvasion
{
    public class Orc : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orc");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            animationType = 508;
            npc.damage = 18;
            npc.defense = 10;
            npc.lifeMax = 205;
            npc.width = 76;
            npc.height = 92;
            npc.knockBackResist = 0.15f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.DD2_OgreDeath;
            npc.value = Terraria.Item.buyPrice(0, 0, 2, 0);
        }
        public override void AI()
        {

        }
    }
}