using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.NPCs.Vajra
{
    public class Vajra : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vajra, Goddess of the Flood");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.damage = 15;
            npc.defense = 10;
            npc.lifeMax = 1400;
            npc.width = 92;
            npc.height = 150;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.boss = true;
            npc.buffImmune[mod.BuffType("Flood")] = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/WoodHit");
            npc.DeathSound = SoundID.NPCDeath10;
            npc.value = Terraria.Item.buyPrice(0, 1, 30, 0);
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/HarmonicConvergence");
            bossBag = mod.ItemType("VajraBag");
        }

        public int A = 0;
        public int B = 0;
        public override void AI() //unfinished code by kitty, ai is a work in progress
        {
            Player player = Main.player[npc.target];
            {
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                }
                float num = 5f;
                float num2 = 0.1f;
                Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float playerX = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
                float playerY = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
                playerX = (float)((int)(playerX / 8f) * 8);
                playerY = (float)((int)(playerY / 8f) * 8);
                vector.X = (float)((int)(vector.X / 8f) * 8);
                vector.Y = (float)((int)(vector.Y / 8f) * 8);
                playerX -= vector.X;
                playerY -= vector.Y;
                float playerCenterSqrt = (float)Math.Sqrt((double)(playerX * playerX + playerY * playerY));
                float num7 = playerCenterSqrt;
                if (playerCenterSqrt == 0f)
                {
                    playerX = npc.velocity.X;
                    playerY = npc.velocity.Y;
                }
                else
                {
                    playerCenterSqrt = num / playerCenterSqrt;
                    playerX *= playerCenterSqrt;
                    playerY *= playerCenterSqrt;
                }
                if (Main.player[npc.target].dead)
                {
                    playerX = (float)npc.direction * num / 2f;
                    playerY = -num / 2f;
                }
                if (npc.velocity.X < playerX)
                {
                    npc.velocity.X = npc.velocity.X + num2;
                }
                else if (npc.velocity.X > playerX)
                {
                    npc.velocity.X = npc.velocity.X - num2;
                }
                if (npc.velocity.Y < playerY)
                {
                    npc.velocity.Y = npc.velocity.Y + num2;
                }
                else if (npc.velocity.Y > playerY)
                {
                    npc.velocity.Y = npc.velocity.Y - num2;
                }


                int npcX = (int)npc.position.X + npc.width / 2;
                int npcY = (int)npc.position.Y + npc.height / 2;
                npcX /= 16;
                npcY /= 16;
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            npc.ai[2] += 1;
            if (npc.ai[0] == 0)
            {
                if (npc.ai[1] == 0)
                {

                    npc.ai[1] = 1;
                }
                if (npc.ai[1] == 1)
                {
                    if (npc.ai[2] >= 35)
                    {
                        float Speed = 10f;  // projectile speed
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                        int damage = 25;  // projectile damage
                        int type = mod.ProjectileType("SpiritualEnergyProjectile");  //put your projectile
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int playerY4 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                        npc.ai[2] = 0;
                        A += 1;
                    }

                    if (A >= 10)
                    {
                        npc.ai[1] = 2;
                        A = 0;
                        B += 1;
                    }
                }
                if (npc.ai[1] == 2)
                {
                    if (npc.ai[2] == 55)
                    {
                        int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 vector355 = new Vector2(npc.position.X + (float)npc.width * 0.8f - 4f, npc.position.Y + (float)npc.height * 0.7f);
                            Vector2 perturbedSpeed = new Vector2(npc.velocity.X, npc.velocity.Y).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.                                                                                // perturbedSpeed = perturbedSpeed * scale; 
                            Projectile.NewProjectile(vector355.X, vector355.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SpiritualEnergyProjectile"), 26, 0f, Main.myPlayer);
                            npc.ai[2] = 0;
                            A += 1;
                        }
                        if (A >= 5)
                        {
                            B += 1;
                            A = 0;
                            if (B >= 5)
                            {
                                npc.ai[1] = 3;
                            }
                            else
                            {
                                npc.ai[1] = 1;
                            }
                        }
                    }
                }
                if (npc.ai[1] == 3)
                {
                    if (B >= 6)
                    {
                        npc.ai[1] = 4 + (float)Main.rand.Next(0, 2);
                    }
                    else
                    {
                        npc.ai[1] = 1;
                    }
                }

                if (npc.ai[1] == 4)
                {
                    if (npc.ai[2] >= 4)
                    {
                        float Speed = 20f;  // projectile speed
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                        int damage = 25;  // projectile damage
                        int type = mod.ProjectileType("SpiritualEnergyProjectile");  //put your projectile
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int playerY4 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                        npc.ai[2] = 0;
                        A += 1;
                    }
                    if (A >= 50)
                    {
                        npc.ai[1] = 1;
                    }
                }
                if (npc.ai[1] == 5)
                {
                    if (Main.rand.Next(4) == 0) //premission from IQ, owner of copper+ to use the code a while ago
                    {
                        float BGPx = npc.Center.X + 500;
                        float BGPy = npc.Center.Y + Main.rand.Next(4000) - 4000;
                        Projectile.NewProjectile(BGPx, BGPy, -10f, 0f, mod.ProjectileType("SpiritualEnergyProjectile"), 20, 0f);
                        float BGPx2 = npc.Center.X - 500;
                        float BGPy2 = npc.Center.Y + Main.rand.Next(4000) - 4000;
                        Projectile.NewProjectile(BGPx2, BGPy2, 10f, 0f, mod.ProjectileType("SpiritualEnergyProjectile"), 20, 0f);
                        A += 1;
                    }
                    if (A >= 50)
                    {
                        npc.ai[1] = 1;
                    }
                }
                if (npc.ai[1] == 6)
                {
                    if (npc.ai[2] >= 4)
                    {
                        int npcY = Main.rand.Next(2, 4);
                        for (int j = 0; j < npcY; j++)
                        {
                            Vector2 vector5 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                            vector5 += npc.velocity * 3f;
                            vector5.Normalize();
                            vector5 *= (float)Main.rand.Next(35, 39) * 0.1f;

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector5.X, vector5.Y, mod.ProjectileType("SpiritualEnergyProjectile"), 10, 0f, 0);
                            A += 1;
                        }
                    }
                    if (A >= 50)
                    {
                        npc.ai[1] = 1;
                    }
                }
            }
            if (npc.ai[0] == 1)
            {

            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int num5;
                for (int i = 0; i < 100; i = num5 + 1)
                {
                    int num4 = Dust.NewDust(npc.Center, npc.width, npc.height, mod.DustType("IndenwoodDust"), 0f, 0f, 0, default(Color), 1f);
                    Dust dust = Main.dust[num4];
                    dust.velocity *= 2f;
                    dust = Main.dust[num4];
                    dust.scale = 1.5f;
                    num5 = i;
                }
            }
        }
        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode && Main.rand.Next(3) == (0))
            {
                player.AddBuff(mod.BuffType("Flood"), 220, true);
            }
            else if (!Main.expertMode && Main.rand.Next(4) == (0))
            {
                player.AddBuff(mod.BuffType("Flood"), 180, true);
            }
        }
    }
}