using System;
using System.Collections.Generic;
using System.IO;

namespace YetAnotherAdtConverter
{
    class GroundEffectsAdder
    {
        static FileInfo groundEffectsFile = new FileInfo("GroundEffects.ini");
        static public List<GroundEffect> GroundEffects = new List<GroundEffect>();

        public GroundEffectsAdder()
        {
            ReadFile();
        }

        public void AddGroundEffects(Files.WOTLK.ADT adt)
        {
            if(GroundEffects != null)
            {
                bool hasBeenAdded = false;

                for(int x = 0; x < adt.MTEX.Textures.Count; x++)
                {
                    adt.MTEX.Textures[x] = adt.MTEX.Textures[x].Replace("\\", "/");
                }

                for(int x = 0; x < adt.MCNKs.Count; x++)
                {
                    if(adt.MCNKs[x].Mcly != null)
                    {
                        for(int y = 0; y < adt.MCNKs[x].Mcly.Entries.Count; y++)
                        {
                            Files.Structs.MCLYentry entry = adt.MCNKs[x].Mcly.Entries[y];
                            try
                            {
                                entry.Detailtextureid = (uint)GroundEffects.Find(
                                    a => a.TexturePath.Replace("\\", "/") == adt.MTEX.Textures[(int)entry.Textid]
                                    ).GroundEffectTextureID;
                                hasBeenAdded = true;
                            }
                            catch
                            {
                                entry.Detailtextureid = 0;
                            }
                            adt.MCNKs[x].Mcly.Entries[y] = entry;
                        }
                    }

                    //Console.WriteLine("bevor:{0}", x);
                    //print_low_quality_map(old.MCNKs[x].MCHeader.GroundEffectsMap);

                    //Thanks to Mjollna(modcraft.io)! I use your code for this.
                    if(hasBeenAdded)
                    {
                        for(int y = 0; y < adt.MCNKs[x].MCHeader.GroundEffectsMap.Length; y++)
                            adt.MCNKs[x].MCHeader.GroundEffectsMap[y] = 0x0;

                        for(int layer = 1; layer < adt.MCNKs[x].Mcly.Entries.Count; layer++)
                        {
                            byte[] amap = getAlphaMap(adt.MCNKs[x], layer);

                            for(int a = 0; a < 8; a++)
                            {
                                for(int b = 0; b < 8; b++)
                                {
                                    int sum = 0;
                                    for(int c = 0; c < 8; c++)
                                    {
                                        for(int d = 0; d < 8; d++)
                                        {
                                            sum += amap[(a * 8 + c) * 64 + (b * 8 + d)];
                                        }
                                    }

                                    if(sum > 120 * 8 * 8)
                                    {
                                        int array_index = (a * 8 + b) / 4;
                                        int bit_index = ((a * 8 + b) % 4) * 2; // -6

                                        adt.MCNKs[x].MCHeader.GroundEffectsMap[array_index] |= Convert.ToByte(((layer & 3) << bit_index));
                                    }
                                }
                            }
                        }
                    }
                    //Console.WriteLine("after:{0}", x);
                    //print_low_quality_map(old.MCNKs[x].MCHeader.GroundEffectsMap);

                }
            }
        }

        //Thanks to Mjollna(modcraft.io)!
        byte[] getAlphaMap(Files.WOTLK.Chunks.MCNK mcnk, int layer)
        {
            Files.Structs.MCLYentry mclyEntry = mcnk.Mcly.Entries[layer];
            byte[] amap = new byte[64 * 64]; // 4096
            for(int x = 0; x < amap.Length; x++) { amap[x] = 0; }

            bool mBigAlpha = (((mcnk.Header.Byte_size / 16U) > layer + 1
                            ? (mclyEntry.Ofsalphamap - mclyEntry.Ofsalphamap)
                            : (mcnk.Mcal.Data.Length - mclyEntry.Ofsalphamap)
                                ) == 64 * 64
                            );

            if((mclyEntry.Flags & 0x100 /*256*/) != 0)
            {
                List<byte> abuf = new List<byte>(mcnk.Mcal.Data).GetRange((int)mclyEntry.Ofsalphamap, mcnk.Mcal.Data.Length - (int)mclyEntry.Ofsalphamap);

                if((mclyEntry.Flags & 0x200 /*512*/) != 0)
                {
                    int offI = 0;
                    int offO = 0;

                    while(offO < 4096)
                    {
                        bool fill = (abuf[offI] & 0x80 /*128*/) != 0;
                        int n = (abuf[offI] & 0x7F /*127*/);
                        ++offI;
                        for(int k = 0; k < n; ++k)
                        {
                            if(offO == 4096)
                                break;
                            amap[offO] = abuf[offI];
                            ++offO;
                            if(!fill)
                                ++offI;
                        }
                        if(fill)
                            ++offI;
                    }
                }
                else if(mBigAlpha)
                {
                    int a = 0;
                    for(int j = 0; j < 64; ++j)
                    {
                        for(int i = 0; i < 64; ++i)
                        {
                            amap[a] = abuf[a];
                            a++;
                        }
                    }
                    Array.Copy(amap, 62 * 64, amap, 63 * 64, 64);
                }
                else
                {
                    int a = 0;
                    int b = 0;
                    for(int j = 0; j < 64; ++j)
                    {
                        for(int i = 0; i < 32; ++i)
                        {
                            amap[a] = (byte)((255 * ((int)(abuf[b] & 0x0f))) / 0x0f);
                            a++;
                            if(i != 31)
                            {
                                amap[a] = (byte)((255 * ((int)(abuf[b] & 0xf0))) / 0xf0);
                                a++;
                            }
                            else
                            {
                                amap[a] = (byte)((255 * ((int)(abuf[b] & 0x0f))) / 0x0f);
                                a++;
                            }
                            b++;
                        }
                    }
                    Array.Copy(amap, 62 * 64, amap, 63 * 64, 64);
                }
            }

            return amap;
        }

        //Thanks to Mjollna(modcraft.io)!
        void print_low_quality_map(byte[] data)
        {
            for(int j = 0; j < 8; ++j)
            {
                for(int i = 0; i < 8; ++i)
                {
                    int array_index = ((j * 8 + i) / 4);
                    int bit_index = (((j * 8 + i) % 4) * 2); //6 - that ones goes 6 4 2 0 and I want it 0 2 4 6

                    Console.Write(((data[array_index] >> (int)bit_index) & 3));
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }

        void ReadFile()
        {
            if(!groundEffectsFile.Exists)
            {
                groundEffectsFile.Create();
            }
            else
            {
                StreamReader reader = new StreamReader(groundEffectsFile.FullName);

                for(string line = ""; (line = reader.ReadLine()) != null;)
                {
                    string[] setting = line.Split('=');
                    GroundEffects.Add(new GroundEffect(setting[0].Trim(), Convert.ToInt32(setting[1].Trim())));
                }
            }
        }
    }

    class GroundEffect
    {
        public string TexturePath;
        public int GroundEffectTextureID;

        public GroundEffect(string path, int id)
        {
            TexturePath = path;
            GroundEffectTextureID = id;
        }
    }
}
