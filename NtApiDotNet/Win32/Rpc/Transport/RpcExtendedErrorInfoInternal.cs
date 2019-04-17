﻿//  Copyright 2019 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using NtApiDotNet.Ndr.Marshal;
using NtApiDotNet.Utilities.Text;
using System;
using System.Text;

namespace NtApiDotNet.Win32.Rpc.Transport
{
    #region Complex Types
    internal struct RpcExtendedErrorInfoInternal : INdrConformantStructure
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }

        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(8);
            Chain = u.ReadEmbeddedPointer(u.ReadStruct<RpcExtendedErrorInfoInternal>);
            ComputerName = u.ReadStruct<ComputerNameUnion>();
            ProcessId = u.ReadInt32();
            TimeStamp = u.ReadInt64();
            GeneratingComponent = u.ReadInt32();
            Status = u.ReadInt32();
            DetectionLocation = u.ReadInt16();
            Flags = u.ReadInt16();
            nLen = u.ReadInt16();
            Parameters = u.ReadConformantStructArray<ExtendedErrorInfoParamInternal>();
        }

        int INdrConformantStructure.GetConformantDimensions()
        {
            return 1;
        }

        public NdrEmbeddedPointer<RpcExtendedErrorInfoInternal> Chain;
        public ComputerNameUnion ComputerName;
        public int ProcessId;
        public long TimeStamp;
        public int GeneratingComponent;
        public int Status;
        public short DetectionLocation;
        public short Flags;
        public short nLen;
        public ExtendedErrorInfoParamInternal[] Parameters;
    }
    internal struct ExtendedErrorInfoParamInternal : INdrStructure
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }

        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(8);
            ParameterType = u.ReadEnum16();
            ParameterData = u.ReadStruct<Union_2>();
        }

        public NdrEnum16 ParameterType;
        public Union_2 ParameterData;

        public object GetObject()
        {
            switch (ParameterType)
            {
                case 1:
                    return ParameterData.AnsiString.GetString().TrimEnd('\0');
                case 2:
                    return ParameterData.UnicodeString.GetString().TrimEnd('\0');
                case 3:
                    return ParameterData.LongVal;
                case 4:
                    return ParameterData.ShortVal;
                case 5:
                    return ParameterData.PointerVal;
                case 7:
                    return ParameterData.BinaryVal.GetObject();
                default:
                    return string.Empty;
            }
        }
    }
    internal struct Union_2 : INdrNonEncapsulatedUnion
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }
        void INdrNonEncapsulatedUnion.Marshal(NdrMarshalBuffer m, long l)
        {
            throw new NotImplementedException();
        }
        
        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(1);
            Selector = u.ReadInt16();
            if ((Selector == 1))
            {
                AnsiString = u.ReadStruct<AnsiStringData>();
                goto done;
            }
            if ((Selector == 2))
            {
                UnicodeString = u.ReadStruct<UnicodeStringData>();
                goto done;
            }
            if ((Selector == 3))
            {
                LongVal = u.ReadInt32();
                goto done;
            }
            if ((Selector == 4))
            {
                ShortVal = u.ReadInt16();
                goto done;
            }
            if ((Selector == 5))
            {
                PointerVal = u.ReadInt64();
                goto done;
            }
            if ((Selector == 6))
            {
                NoneVal = u.ReadEmpty();
                goto done;
            }
            if ((Selector == 7))
            {
                BinaryVal = u.ReadStruct<BinaryData>();
                goto done;
            }
            throw new System.ArgumentException("No matching union selector when marshaling Union_2");
            done:
            return;
        }

        private short Selector;
        public AnsiStringData AnsiString;
        public UnicodeStringData UnicodeString;
        public int LongVal;
        public short ShortVal;
        public long PointerVal;
        public NdrEmpty NoneVal;
        public BinaryData BinaryVal;
    }
    internal struct AnsiStringData : INdrStructure
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }
        
        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(4);
            Length = u.ReadInt16();
            Data = u.ReadEmbeddedPointer(u.ReadConformantArray<byte>);
        }

        public short Length;
        public NdrEmbeddedPointer<byte[]> Data;

        public string GetString()
        {
            return BinaryEncoding.Instance.GetString(Data.GetValue());
        }
    }
    internal struct UnicodeStringData : INdrStructure
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }

        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(4);
            Length = u.ReadInt16();
            Data = u.ReadEmbeddedPointer(u.ReadConformantArray<short>);
        }

        public short Length;
        public NdrEmbeddedPointer<short[]> Data;

        public string GetString()
        {
            short[] data = Data.GetValue();
            byte[] buffer = new byte[data.Length * 2];
            Buffer.BlockCopy(data, 0, buffer, 0, buffer.Length);
            return Encoding.Unicode.GetString(buffer);
        }
    }

    internal struct BinaryData : INdrStructure
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }

        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(4);
            Length = u.ReadInt16();
            Data = u.ReadEmbeddedPointer(u.ReadConformantArray<sbyte>);
        }

        public short Length;
        public NdrEmbeddedPointer<sbyte[]> Data;

        public object GetObject()
        {
            return (byte[])(object)Data.GetValue();
        }
    }
    internal struct ComputerNameUnion : INdrStructure
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }

        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(4);
            Member0 = u.ReadEnum16();
            Member8 = u.ReadStruct<ComputerNameData>();
        }

        public NdrEnum16 Member0;
        public ComputerNameData Member8;

        public string GetString()
        {
            if (Member0 == 1)
            {
                return Member8.Arm_1.GetString();
            }
            return string.Empty;
        }
    }
    internal struct ComputerNameData : INdrNonEncapsulatedUnion
    {
        void INdrStructure.Marshal(NdrMarshalBuffer m)
        {
            throw new NotImplementedException();
        }
        void INdrNonEncapsulatedUnion.Marshal(NdrMarshalBuffer m, long l)
        {
            throw new NotImplementedException();
        }

        void INdrStructure.Unmarshal(NdrUnmarshalBuffer u)
        {
            u.Align(1);
            switch (u.ReadInt16())
            {
                case 1:
                    Arm_1 = u.ReadStruct<UnicodeStringData>();
                    break;
                case 2:
                    break;
                default:
                    throw new System.ArgumentException("No matching union selector when marshaling ComputerNameData");
            }
        }

        private short Selector;
        public UnicodeStringData Arm_1;
        public NdrEmpty Arm_2;
    }
    #endregion
    #region Complex Type Encoders
    internal static class ExtendedErrorInfoDecoder
    {
        internal static RpcExtendedErrorInfoInternal? Decode(byte[] data)
        {
            NdrUnmarshalBuffer u = new NdrUnmarshalBuffer(data);
            RpcExtendedErrorInfoInternal v;
            // Read out referent.
            int referent = u.ReadReferent();
            if (referent == 0)
            {
                return null;
            }
            v = u.ReadStruct<RpcExtendedErrorInfoInternal>();
            u.PopulateDeferredPointers();
            return v;
        }
    }
    #endregion

}