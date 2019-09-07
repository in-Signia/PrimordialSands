using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrimordialSands.Projectiles
{
    public class PoseidonBoltProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poseidon's Fury");
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 28;
            projectile.light = 0.25f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 175;
            projectile.timeLeft = 345;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }
        public override void Kill(int timeLeft)
        {
            int num3;
            for (int num20 = 0; num20 < 1; num20 = num3 + 1)
            {
                float num21 = projectile.velocity.X / 4f * (float)num20;
                float num22 = projectile.velocity.Y / 4f * (float)num20;
                int num23 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 226, 0f, 0f, 0, default(Color), 1.3f);
                Main.dust[num23].position.X = projectile.Center.X - num21;
                Main.dust[num23].position.Y = projectile.Center.Y - num22;
                Dust dust3 = Main.dust[num23];
                dust3.velocity *= 0.6f;
                Main.dust[num23].scale = 0.6f;
                Main.dust[num23].fadeIn = 0.4f;
                num3 = num20;
            }
            for (int num20 = 0; num20 < 1; num20 = num3 + 1)
            {
                float num21 = projectile.velocity.X / 4f * (float)num20;
                float num22 = projectile.velocity.Y / 4f * (float)num20;
                int num23 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 226, 0f, 0f, 0, default(Color), 1.3f);
                Main.dust[num23].position.X = projectile.Center.X - num21;
                Main.dust[num23].position.Y = projectile.Center.Y - num22;
                Dust dust3 = Main.dust[num23];
                dust3.velocity *= 0.66f;
                Main.dust[num23].scale = 0.76f;
                Main.dust[num23].fadeIn = 0.4f;
                num3 = num20;
            }
        }
        public override void AI()
        {
            int num3;
            for (int num452 = 0; num452 < 1; num452 = num3 + 1)
            {
                Vector2 vector36 = projectile.position;
                vector36 -= projectile.velocity * ((float)num452 * 0.25f);
                projectile.alpha = 255;
                int num453 = Dust.NewDust(vector36, 1, 1, 226, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num453].position = vector36;
                Main.dust[num453].scale = 0.5f;
                Dust dust3 = Main.dust[num453];
                dust3.velocity *= 0f;
                dust3.noGravity = true;
                num3 = num452;
            }
            for (int num452 = 0; num452 < 1; num452 = num3 + 1)
            {
                Vector2 vector36 = projectile.position;
                vector36 -= projectile.velocity * ((float)num452 * 0.25f);
                projectile.alpha = 255;
                int num453 = Dust.NewDust(vector36, 1, 1, 226, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num453].position = vector36;
                Dust dust3 = Main.dust[num453];
                dust3.velocity *= 0f;
                dust3.noGravity = true;
                num3 = num452;
            }
            if (projectile.timeLeft == 295)
            {
                Vector2 value11 = Main.screenPosition + new Vector2((float)projectile.ai[0], (float)projectile.ai[1]);
                projectile.velocity = Vector2.Normalize(value11 - projectile.Center) * 13;
            }
            if (projectile.timeLeft <= 280)
            {
                float num372 = projectile.position.X;
                float num373 = projectile.position.Y;
                float num374 = 100000f;
                bool flag10 = false;
                projectile.ai[0] += 1f;
                if (projectile.ai[0] > 10f)
                {
                    projectile.ai[0] = 10f;

                    for (int num375 = 0; num375 < 200; num375 = num3 + 1)
                    {
                        if (Main.npc[num375].CanBeChasedBy(projectile, false) && (!Main.npc[num375].wet || projectile.type == 307))
                        {
                            float num376 = Main.npc[num375].position.X + (float)(Main.npc[num375].width / 2);
                            float num377 = Main.npc[num375].position.Y + (float)(Main.npc[num375].height / 2);
                            float num378 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num376) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num377);
                            if (num378 < 800f && num378 < num374 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num375].position, Main.npc[num375].width, Main.npc[num375].height))
                            {
                                num374 = num378;
                                num372 = num376;
                                num373 = num377;
                                flag10 = true;
                            }
                        }
                        num3 = num375;
                    }
                }
                if (!flag10)
                {
                    num372 = projectile.position.X + (float)(projectile.width / 2) + projectile.velocity.X * 100f;
                    num373 = projectile.position.Y + (float)(projectile.height / 2) + projectile.velocity.Y * 100f;
                }

                projectile.friendly = true;

                float num379 = 14f;
                float num380 = 0.1f;
                Vector2 vector29 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num381 = num372 - vector29.X;
                float num382 = num373 - vector29.Y;
                float num383 = (float)Math.Sqrt((double)(num381 * num381 + num382 * num382));
                num383 = num379 / num383;
                num381 *= num383;
                num382 *= num383;
                if (projectile.velocity.X < num381)
                {
                    projectile.velocity.X = projectile.velocity.X + num380;
                    if (projectile.velocity.X < 0f && num381 > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num380 * 2f;
                    }
                }
                else if (projectile.velocity.X > num381)
                {
                    projectile.velocity.X = projectile.velocity.X - num380;
                    if (projectile.velocity.X > 0f && num381 < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - num380 * 2f;
                    }
                }
                if (projectile.velocity.Y < num382)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num380;
                    if (projectile.velocity.Y < 0f && num382 > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num380 * 2f;
                        return;
                    }
                }
                else if (projectile.velocity.Y > num382)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num380;
                    if (projectile.velocity.Y > 0f && num382 < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num380 * 2f;
                        return;
                    }
                }
            }
        }
    }
}