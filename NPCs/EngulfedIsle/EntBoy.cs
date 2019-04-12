using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.NPCs.EngulfedIsle
{
    public class EntBoy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tree Ent");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = 22;
            npc.width = 38;
            npc.height = 58;
            npc.damage = 12;
            npc.defense = 10;
            npc.lifeMax = 45;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.5f;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/WoodHit");
            npc.DeathSound = SoundID.NPCDeath8;
            npc.value = Terraria.Item.buyPrice(0, 0, 2, 0);
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.1f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (npc.Center.X < Main.player[npc.target].Center.X - 2f)
            {
                npc.direction = 1;
            }
            if (npc.Center.X > Main.player[npc.target].Center.X + 2f)
            {
                npc.direction = -1;
            }
            npc.spriteDirection = npc.direction;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return Main.LocalPlayer.GetModPlayer<PrimordialSandsPlayer>().ZoneSwamp && Main.dayTime ? 0.38f : 0f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(4) == (0))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Indenwood"), Main.rand.Next(2, 7));
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < 1)
            {
                for (int i = 0; i < 120; i++)
                {
                    int dustType = mod.DustType("IndenwoodDust");
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
            }
        }
    }
}