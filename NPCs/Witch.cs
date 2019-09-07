using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.NPCs
{
    public class Witch : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Witch");
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 54;
            npc.height = 50;
            npc.damage = 11;
            npc.defense = 6;
            npc.lifeMax = 74;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.5f;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/WitchHurt");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/NPC/WitchDeath");
            npc.value = Terraria.Item.buyPrice(0, 0, 2, 50);
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
            npc.TargetClosest(true);
            if (npc.ai[3] < 0f)
            {
                npc.knockBackResist = 0f;
                npc.noGravity = true;
                npc.noTileCollide = true;
                if (npc.velocity.X < 0f)
                {
                    npc.direction = -1;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.direction = 1;
                }
                npc.rotation = npc.velocity.X * 0.1f;
                if (Main.netMode != 1)
                {
                    npc.localAI[3] += 1f;
                    if (npc.localAI[3] > (float)Main.rand.Next(50, 195))
                    {
                        npc.localAI[3] = 0f;
                        float Speed = 5f;
                        Vector2 vector7 = npc.Center;
                        vector7 += npc.velocity;
                        Projectile.NewProjectile((int)vector7.X, (int)vector7.Y, Speed, -Speed, mod.ProjectileType("WitchFlaskProjectile"), npc.damage, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }

            else
            {
                npc.localAI[3] = 0f;
                npc.knockBackResist = 0.35f * Main.knockBackMultiplier;
                npc.rotation *= 0.9f;
                npc.noGravity = false;
                npc.noTileCollide = false;
            }
            if (npc.ai[3] == 1f)
            {
                npc.knockBackResist = 0f;
            }
            if (npc.ai[3] == -1f)
            {
                npc.TargetClosest(true);
                float num19 = 8f;
                float num20 = 40f;
                Vector2 value3 = Main.player[npc.target].Center - npc.Center;
                float num21 = value3.Length();
                num19 += num21 / 50f;
                value3.Normalize();
                value3 *= num19;
                npc.velocity = (npc.velocity * (num20 - 1f) + value3) / num20;
                if (num21 < 100f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.ai[3] = 0f;
                    npc.ai[2] = 0f;
                }
                return;
            }
            if (npc.ai[3] == -2f)
            {
                npc.velocity.Y = npc.velocity.Y - 0.2f;
                if (npc.velocity.Y < -10f)
                {
                    npc.velocity.Y = -10f;
                }
                if (Main.player[npc.target].Center.Y - npc.Center.Y > 200f)
                {
                    npc.TargetClosest(true);
                    npc.ai[3] = -3f;
                    if (Main.player[npc.target].Center.X > npc.Center.X)
                    {
                        npc.ai[2] = 1f;
                    }
                    else
                    {
                        npc.ai[2] = -1f;
                    }
                }
                npc.velocity.X = npc.velocity.X * 0.99f;
                return;
            }
            if (npc.ai[3] == -3f)
            {
                if (npc.direction == 0)
                {
                    npc.TargetClosest(true);
                }
                if (npc.ai[2] == 0f)
                {
                    npc.ai[2] = (float)npc.direction;
                }
                npc.velocity.Y = npc.velocity.Y * 0.9f;
                npc.velocity.X = npc.velocity.X + npc.ai[2] * 0.3f;
                if (npc.velocity.X > 10f)
                {
                    npc.velocity.X = 10f;
                }
                if (npc.velocity.X < -10f)
                {
                    npc.velocity.X = -10f;
                }
                float num22 = Main.player[npc.target].Center.X - npc.Center.X;
                if ((npc.ai[2] < 0f && num22 > 300f) || (npc.ai[2] > 0f && num22 < -300f))
                {
                    npc.ai[3] = -4f;
                    npc.ai[2] = 0f;
                    return;
                }
                if (Math.Abs(num22) > 800f)
                {
                    npc.ai[3] = -1f;
                    npc.ai[2] = 0f;
                }
                return;
            }
            else
            {
                if (npc.ai[3] == -4f)
                {
                    npc.ai[2] += 1f;
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                    if (npc.velocity.Length() > 0.5f)
                    {
                        npc.velocity *= 0.9f;
                    }
                    int num23 = (int)npc.Center.X / 16;
                    int num24 = (int)(npc.position.Y + (float)npc.height + 12f) / 16;
                    bool flag3 = false;
                    int num25;
                    for (int i = num23 - 1; i <= num23 + 1; i = num25 + 1)
                    {
                        if (Main.tile[i, num24] == null)
                        {
                            Main.tile[num23, num24] = new Tile();
                        }
                        if (Main.tile[i, num24].active() && Main.tileSolid[(int)Main.tile[i, num24].type])
                        {
                            flag3 = true;
                        }
                        num25 = i;
                    }
                    if (flag3 && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[3] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (npc.ai[2] > 300f || npc.Center.Y > Main.player[npc.target].Center.Y + 200f)
                    {
                        npc.ai[3] = -1f;
                        npc.ai[2] = 0f;
                    }
                }
                else
                {
                    if (npc.ai[3] == 1f)
                    {
                        Vector2 center3 = npc.Center;
                        center3.Y -= 70f;
                        npc.velocity.X = npc.velocity.X * 0.8f;
                        npc.ai[2] += 1f;
                        if (npc.ai[2] >= 90f)
                        {
                            npc.ai[3] = -2f;
                            npc.ai[2] = 0f;
                        }
                    }
                    npc.ai[2] += 1f;
                    if (npc.velocity.Y == 0f)
                    {
                        if (npc.ai[2] >= 180f)
                        {
                            npc.ai[2] = 0f;
                            npc.ai[3] = 1f;
                        }
                    }
                    else
                    {
                        if (npc.ai[2] >= 360f)
                        {
                            npc.ai[2] = 0f;
                            npc.ai[3] = -2f;
                            npc.velocity.Y = npc.velocity.Y - 3f;
                        }
                    }
                    if (npc.target >= 0 && !Main.player[npc.target].dead && (Main.player[npc.target].Center - npc.Center).Length() > 800f)
                    {
                        npc.ai[3] = -1f;
                        npc.ai[2] = 0f;
                    }
                }
                if (Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                    if (Main.player[npc.target].dead && npc.timeLeft > 1)
                    {
                        npc.timeLeft = 1;
                    }
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !Main.dayTime ? 0.25f : 0f;
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(15) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VitalFlask"));
            }
            if (Main.rand.Next(15) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PutridFlask"));
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life < 1)
            {
                for (int i = 0; i < 120; i++)
                {
                    int dustType = 5;
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                    Main.dust[dustIndex].scale = 1.45f;
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.87f, npc.height * 0.87f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}