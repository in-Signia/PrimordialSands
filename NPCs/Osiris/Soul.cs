using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace PrimordialSands.NPCs.Osiris
{
    public class Soul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul");
        }
        public override void SetDefaults()
        {

            npc.damage = 120;
            npc.npcSlots = 1f;
            npc.width = 24; //324
            npc.height = 22; //216
            npc.defense = 10;
            npc.lifeMax = 5000;
            npc.aiStyle = -1; //new
            aiType = -1; //new
            npc.knockBackResist = 0f;
            npc.alpha = 255;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath6;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 2f * bossLifeScale);
            npc.damage = 140;
            npc.defense = 40;
        }

        float charge = 0f;
        public override bool PreAI()
        {
            npc.ai[3] += 1;
            if (Main.rand.Next(2) == 0)
            {
                npc.ai[3] += 2;
            }
            if (npc.ai[3] >= 600)
            {
                npc.ai[3] = 0;
                Vector2 vector78 = new Vector2(npc.position.X + (float)(npc.width / 2) + (float)(Main.rand.Next(20) * npc.direction), npc.position.Y + (float)npc.height * 0.8f);
                Vector2 vector79 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float num600 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector79.X;
                float num601 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 300f - vector79.Y;
                float num602 = (float)Math.Sqrt((double)(num600 * num600 + num601 * num601));
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 93);
                if (Main.netMode != 1)
                {
                    float num603 = 12f;
                    if (Main.expertMode)
                    {
                        num603 += 3f;
                    }
                    if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.25)
                    {
                        num603 += 2f;
                    }
                    float num604 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector78.X + (float)Main.rand.Next(-80, 81);
                    float num605 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector78.Y + (float)Main.rand.Next(-40, 41);
                    float num606 = (float)Math.Sqrt((double)(num604 * num604 + num605 * num605));
                    num606 = num603 / num606;
                    num604 *= num606;
                    num605 *= num606;
                    int num607 = 38;
                    int num608 = mod.ProjectileType("SoulProjectile");
                    int num609 = Projectile.NewProjectile(vector78.X, vector78.Y, num604, num605, num608, num607, 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[num609].timeLeft = 300;
                }
            }
            if (npc.alpha >= 0)
            {
                npc.alpha -= 6;
            }
            if (npc.ai[2] <= 240)
            {
                npc.ai[2] += 1;
            }
            double deg = (double)npc.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = npc.ai[2];
            NPC p = Main.npc[(int)npc.ai[0]];
            npc.position.X = p.Center.X - (int)(Math.Cos(rad) * dist) - npc.width / 2;
            npc.position.Y = p.Center.Y - (int)(Math.Sin(rad) * dist) - npc.height / 2;
            if (charge <= 5f)
            {
                charge += 0.08f;
            }
            if (p.life <= 0 || !p.active)
            {
                npc.active = false;
                npc.life = 0;
                npc.checkDead();
                npc.HitEffect();
            }
            npc.rotation += 0.04f;
            npc.ai[1] += charge;
            return false;
        }
    }
}