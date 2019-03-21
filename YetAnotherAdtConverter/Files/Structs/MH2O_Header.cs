using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherAdtConverter.Files.Structs
{
    /// <summary> Only found in an MH2O chunk. Always an array of 256 elements.
    /// <para>This structure has a fixed size of 12 bytes.</para>
    /// </summary>
    struct MH2O_Header
    {
        UInt32 offset_instances;       // points to SMLiquidInstance[layer_count]
        UInt32 layer_count;            // 0 if the chunk has no liquids. If > 1, the offsets will point to arrays.
        UInt32 offset_attributes;      // points to mh2o_chunk_attributes, can be ommitted for all-0

        public UInt32 Offset_instances { get => offset_instances; set => offset_instances = value; }
        public UInt32 Layer_count { get => layer_count; set => layer_count = value; }
        public UInt32 Offset_attributes { get => offset_attributes; set => offset_attributes = value; }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes(offset_attributes));
            bytes.AddRange(BitConverter.GetBytes(layer_count));
            bytes.AddRange(BitConverter.GetBytes(Offset_instances));

            return bytes.ToArray();
        }
    }
}
