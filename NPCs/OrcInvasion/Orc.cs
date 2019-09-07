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
            npc.aiStyle = 3;
            aiType = -1;
            animationType = 508;
            npc.damage = 18;
            npc.defense = 10;
            npc.lifeMax = 205;
            npc.width = 72;
            npc.height = 88;
            npc.knockBackResist = 0.15f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.DD2_OgreDeath;
            npc.value = Terraria.Item.buyPrice(0, 0, 2, 0);
        }
        public override void AI()
        {
            npc.knockBackResist = 0.4f * Main.knockBackMultiplier;
            npc.noGravity = false;
            Vector2 center2 = npc.Center;
            if (npc.ai[3] == -0.10101f)
            {
                npc.ai[3] = 0f;
                float num3 = npc.velocity.Length();
                num3 *= 2f;
                if (num3 > 10f)
                {
                    num3 = 10f;
                }
                npc.velocity.Normalize();
                npc.velocity *= num3;
                if (npc.velocity.X < 0f)
                {
                    npc.direction = -1;
                }
                if (npc.velocity.X > 0f)
                {
                    npc.direction = 1;
                }
                npc.spriteDirection = npc.direction;
            }
            int num37 = 60;
            if (npc.ai[3] < (float)num37 && PrimordialSandsWorld.OrcsAcquisitionUp)
            {
                if (Main.rand.Next(1300) == 0)
                {
                    Main.PlaySound(SoundID.DD2_OgreAttack, npc.position);
                }
                if (Main.rand.Next(1400) == 0)
                {
                    Main.PlaySound(SoundID.DD2_OgreHurt, npc.position);
                }
                if (Main.rand.Next(1200) == 0)
                {
                    Main.PlaySound(SoundID.DD2_OgreRoar, npc.position);
                }
                npc.TargetClosest(true);
            }
            else if (npc.ai[2] <= 0f)
            {
                if (Main.dayTime && (double)(npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.velocity.X == 0f)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        npc.ai[0] += 1f;
                        if (npc.ai[0] >= 2f)
                        {
                            npc.direction *= -1;
                            npc.spriteDirection = npc.direction;
                            npc.ai[0] = 0f;
                        }
                    }
                }
                else
                {
                    npc.ai[0] = 0f;
                }
                if (npc.direction == 0)
                {
                    npc.direction = 1;
                }
            }

            float num58 = 1.5f;
            if (npc.velocity.X < -num58 || npc.velocity.X > num58)
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity *= 0.8f;
                }
            }
            else if (npc.velocity.X < num58 && npc.direction == 1)
            {
                npc.velocity.X = npc.velocity.X + 0.07f;
                if (npc.velocity.X > num58)
                {
                    npc.velocity.X = num58;
                }
            }
            else if (npc.velocity.X > -num58 && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - 0.07f;
                if (npc.velocity.X < -num58)
                {
                    npc.velocity.X = -num58;
                }
            }
        }
    }
}