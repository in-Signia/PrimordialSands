using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.Projectiles
{
    public class EriditeBulletProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eridite Bullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 32;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.aiStyle = 1;
            aiType = 14;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X / 8, (int)projectile.Center.Y / 8, 4f, 1f, 2f);
            float num101 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            if (projectile.alpha > 0)
            {
                projectile.alpha -= (int)((byte)((double)num101 * 0.9));
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }

            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 3f)
            {
                int num3;
                for (int num452 = 0; num452 < 4; num452 = num3 + 1)
                {
                    Vector2 vector36 = projectile.position;
                    vector36 -= projectile.velocity * ((float)num452 * 2f);            
                    int num453 = Dust.NewDust(vector36, 1, 1, 134, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num453].noGravity = true;
                    Main.dust[num453].position = vector36;
                    Main.dust[num453].scale = (float)Main.rand.Next(70, 70) * 0.012f;
                    Dust dust3 = Main.dust[num453];
                    dust3.velocity *= 1.2f;
                    num3 = num452;
                }
                return;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
