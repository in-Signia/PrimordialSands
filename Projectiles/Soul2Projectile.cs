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

namespace PrimordialSands.Projectiles
{
    public class Soul2Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul");
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.light = 0.25f;
            projectile.tileCollide = false;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.alpha = 200;
            projectile.timeLeft = 305;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num621 = 0; num621 < 6; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 263, 0f, 0f, 100, default(Color), 2.2f);
                Main.dust[num622].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].noGravity = true;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int num623 = 0; num623 < 8; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 175, 0f, 0f, 100, default(Color), 2.7f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 4f;
                num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 263, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num624].velocity *= 2f;
                Main.dust[num624].noGravity = true;
            }
        }
        int ai1 = 0;
        int ai2 = 0;
        public override void AI()
        {
            int num414 = (int)(projectile.Center.X);
            int num415 = (int)(projectile.Center.Y);
            projectile.ai[0] += 1;
            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = 1f;
                Main.PlaySound(SoundID.Item120, projectile.position);
            }
            if (projectile.ai[0] <= 2)
            {
                projectile.velocity = new Vector2(0f, -0.75f);
            }
            if (projectile.ai[0] >= 10 && projectile.ai[0] <= 34)
            {
                projectile.velocity.Y *= 1.14f;
            }
                if (projectile.ai[0] >= 40)
            {
                projectile.velocity.Y *= 0.7f;
                projectile.ai[1] += 1;
            }
            if (projectile.ai[0] >= 200)
            {
                projectile.alpha += 10;
            }
            else
            {
                projectile.alpha -= 5;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.alpha > 255)
            {
                projectile.alpha = 255;
            }
            float Speed = 5f;
            if (projectile.ai[1] == 15)
            {
                Projectile.NewProjectile((float)num414, (float)num415, 0f, -Speed, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] == 30)
            {
                Projectile.NewProjectile((float)num414, (float)num415, -Speed, -Speed, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] == 45)
            {
                Projectile.NewProjectile((float)num414, (float)num415, -Speed, 0f, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] == 60)
            {
                Projectile.NewProjectile((float)num414, (float)num415, -Speed, Speed, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] == 75)
            {
                Projectile.NewProjectile((float)num414, (float)num415, 0f, Speed, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] == 90)
            {
                Projectile.NewProjectile((float)num414, (float)num415, Speed, Speed, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] == 105)
            {
                Projectile.NewProjectile((float)num414, (float)num415, Speed, 0f, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] == 120)
            {
                Projectile.NewProjectile((float)num414, (float)num415, Speed, -Speed, mod.ProjectileType("SoulProjectile"), 25, 0f, Main.myPlayer, 0f, 0f);
                projectile.ai[1] = 0;
            }
                Lighting.AddLight(projectile.Center, 0.3f, 0.75f, 0.9f);


            if (ai1 >= 40)
            {
                projectile.alpha += 3;
            }
            else
            {
                projectile.alpha -= 40;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.alpha > 255)
            {
                projectile.alpha = 255;
            }
            return;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }
    }
}