using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    struct MCNKheader
    {
        UInt32 flags;
        UInt32 indexX;
        UInt32 indexY;
        UInt32 nLayers;
        UInt32 nDoodadRefs;

        offset ofsMCVT;
        offset ofsMCNR;
        offset ofsMCLY;
        offset ofsMCRF;
        offset ofsMCAL;

        UInt32 sizeAlpha;

        offset ofsMCSH;

        UInt32 sizeShadow;
        UInt32 areaid;
        UInt32 nMapObjRefs;
        UInt32 holes;

        byte[] groundEffectsMap; // = new byte[16];

        UInt32 predTex;
        UInt32 noEffectDoodad;

        offset ofsMCSE;
        UInt32 nSndEmitters;

        offset ofsMCLQ;
        UInt32 sizeLiquid;

        float[] pos; // = new float[3];

        offset ofsMCCV;

        UInt32 props;
        UInt32 effectId;

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(flags));
            bytes.AddRange(BitConverter.GetBytes(indexX));
            bytes.AddRange(BitConverter.GetBytes(indexY));
            bytes.AddRange(BitConverter.GetBytes(nLayers));
            bytes.AddRange(BitConverter.GetBytes(nDoodadRefs));

            bytes.AddRange(BitConverter.GetBytes(ofsMCVT.address));
            bytes.AddRange(BitConverter.GetBytes(ofsMCNR.address));
            bytes.AddRange(BitConverter.GetBytes(ofsMCLY.address));
            bytes.AddRange(BitConverter.GetBytes(ofsMCRF.address));
            bytes.AddRange(BitConverter.GetBytes(ofsMCAL.address));

            bytes.AddRange(BitConverter.GetBytes(sizeAlpha));

            bytes.AddRange(BitConverter.GetBytes(ofsMCSH.address));

            bytes.AddRange(BitConverter.GetBytes(sizeShadow));
            bytes.AddRange(BitConverter.GetBytes(areaid));
            bytes.AddRange(BitConverter.GetBytes(nMapObjRefs));
            bytes.AddRange(BitConverter.GetBytes(holes));

            bytes.AddRange(groundEffectsMap);

            bytes.AddRange(BitConverter.GetBytes(predTex));
            bytes.AddRange(BitConverter.GetBytes(noEffectDoodad));

            bytes.AddRange(BitConverter.GetBytes(ofsMCSE.address));

            bytes.AddRange(BitConverter.GetBytes(nSndEmitters));

            bytes.AddRange(BitConverter.GetBytes(ofsMCLQ.address));
            bytes.AddRange(BitConverter.GetBytes(sizeLiquid));

            foreach(float x in pos)
            {
                bytes.AddRange(BitConverter.GetBytes(x));
            }

            bytes.AddRange(BitConverter.GetBytes(ofsMCCV.address));
            bytes.AddRange(BitConverter.GetBytes(props));
            bytes.AddRange(BitConverter.GetBytes(effectId));

            return bytes.ToArray();
        }

        public uint Flags { get => flags; set => flags = value; }
        public uint IndexX { get => indexX; set => indexX = value; }
        public uint IndexY { get => indexY; set => indexY = value; }
        public uint NLayers { get => nLayers; set => nLayers = value; }
        public uint NDoodadRefs { get => nDoodadRefs; set => nDoodadRefs = value; }
        public uint SizeAlpha { get => sizeAlpha; set => sizeAlpha = value; }
        public uint SizeShadow { get => sizeShadow; set => sizeShadow = value; }
        public uint Areaid { get => areaid; set => areaid = value; }
        public uint NMapObjRefs { get => nMapObjRefs; set => nMapObjRefs = value; }
        public uint Holes { get => holes; set => holes = value; }
        public byte[] GroundEffectsMap { get => groundEffectsMap; set => groundEffectsMap = value; }
        public uint PredTex { get => predTex; set => predTex = value; }
        public uint NoEffectDoodad { get => noEffectDoodad; set => noEffectDoodad = value; }
        public uint NSndEmitters { get => nSndEmitters; set => nSndEmitters = value; }
        public uint SizeLiquid { get => sizeLiquid; set => sizeLiquid = value; }
        public float[] Pos { get => pos; set => pos = value; }
        public uint Props { get => props; set => props = value; }
        public uint EffectId { get => effectId; set => effectId = value; }
        internal offset OfsMCVT { get => ofsMCVT; set => ofsMCVT = value; }
        internal offset OfsMCNR { get => ofsMCNR; set => ofsMCNR = value; }
        internal offset OfsMCLY { get => ofsMCLY; set => ofsMCLY = value; }
        internal offset OfsMCRF { get => ofsMCRF; set => ofsMCRF = value; }
        internal offset OfsMCAL { get => ofsMCAL; set => ofsMCAL = value; }
        internal offset OfsMCSH { get => ofsMCSH; set => ofsMCSH = value; }
        internal offset OfsMCSE { get => ofsMCSE; set => ofsMCSE = value; }
        internal offset OfsMCLQ { get => ofsMCLQ; set => ofsMCLQ = value; }
        internal offset OfsMCCV { get => ofsMCCV; set => ofsMCCV = value; }
    }
}
