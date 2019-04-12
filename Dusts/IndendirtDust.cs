using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PrimordialSands.Dusts
{
    class IndendirtDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.scale = 1.5f;
            dust.velocity *= 0.95f;
            dust.noGravity = true;
        }
    }
}