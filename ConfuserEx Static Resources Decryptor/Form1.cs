using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfuserEx_Static_Resources_Decryptor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.AllowDrop = true;
        }
        private void TextBox1DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        string DirectoryName = "";
        public static string ExePath = "";
        private void TextBox1DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array arrayyy = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (arrayyy != null)
                {
                    string text = arrayyy.GetValue(0).ToString();
                    int num = text.LastIndexOf(".", StringComparison.Ordinal);
                    if (num != -1)
                    {
                        string text2 = text.Substring(num);
                        text2 = text2.ToLower();
                        if (text2 == ".exe" || text2 == ".dll")
                        {
                            Activate();
                            ExePath = text;
                            label2.Text = "Status : Exe Loaded";
                            int num2 = text.LastIndexOf("\\", StringComparison.Ordinal);
                            if (num2 != -1)
                            {
                                DirectoryName = text.Remove(num2, text.Length - num2);
                            }
                            if (DirectoryName.Length == 2)
                            {
                                DirectoryName += "\\";
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            ModuleDefMD moduleDefMD = ModuleDefMD.Load(ExePath);
            uint valuee = getfirst(moduleDefMD);
            uint[] uintValue = GetUintValue(moduleDefMD);
            uint second = getSecond(moduleDefMD);
            initialize(valuee, uintValue, second, moduleDefMD);
            RemoveCall(moduleDefMD);
            label2.Text = "Status : Saving Exe";
            string filename = string.Concat(new string[]
            {
       DirectoryName,
       "\\",
       Path.GetFileNameWithoutExtension(ExePath),
       "-Decrypted",
       Path.GetExtension(ExePath)
            });
            ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(moduleDefMD);
            moduleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            moduleWriterOptions.MetaDataOptions.Flags = MetaDataFlags.PreserveAll;
            NativeModuleWriterOptions nativeModuleWriterOptions = new NativeModuleWriterOptions(moduleDefMD);
            nativeModuleWriterOptions.MetaDataOptions.Flags = MetaDataFlags.PreserveAll;
            nativeModuleWriterOptions.Logger = DummyLogger.NoThrowInstance;
            bool isILOnly = moduleDefMD.IsILOnly;
            if (isILOnly)
            {
                moduleDefMD.Write(filename, moduleWriterOptions);
            }
            else
            {
                moduleDefMD.NativeWrite(filename, nativeModuleWriterOptions);
            }
            label2.Text = "Status : Success ! ";
        }
        public static void RemoveCall(ModuleDefMD module)
        {
            foreach (TypeDef current in module.Types)
            {
                if(current.Name.Equals("<Module>"))
                {
                    foreach (MethodDef current2 in current.Methods)
                    {
                        if (current2.IsStaticConstructor)
                        {
                                for (int i = 0; i < current2.Body.Instructions.Count; i++)
                                {
                                    if (current2.Body.Instructions[i].OpCode == OpCodes.Call)
                                    {
                                        if (current2.Body.Instructions[i].Operand.ToString().Contains(cctormethod.Name))
                                        {
                                            current2.Body.Instructions.RemoveAt(i);
                                        }
                                    }
                                }
                        }
                    }
                }
               
            }
        }
        internal class Class1
        {
            internal class Class2
            {
                internal readonly Struct1[] struct1_0 = new Struct1[16];
                internal readonly Struct1[] struct1_1 = new Struct1[16];
                internal Struct0 struct0_0 = default(Struct0);
                internal Struct0 struct0_1 = default(Struct0);
                internal Struct1 struct1_2 = new Struct1(8);
                internal uint uint_0;
                internal void method_0(uint uint_1)
                {
                    for (uint num = this.uint_0; num < uint_1; num += 1u)
                    {
                        this.struct1_0[(int)((uint)((UIntPtr)num))] = new Struct1(3);
                        this.struct1_1[(int)((uint)((UIntPtr)num))] = new Struct1(3);
                    }
                    this.uint_0 = uint_1;
                }
                internal void method_1()
                {
                    this.struct0_0.method_0();
                    for (uint num = 0u; num < this.uint_0; num += 1u)
                    {
                        this.struct1_0[(int)((uint)((UIntPtr)num))].method_0();
                        this.struct1_1[(int)((uint)((UIntPtr)num))].method_0();
                    }
                    this.struct0_1.method_0();
                    this.struct1_2.method_0();
                }
                internal uint method_2(Class0 class0_0, uint uint_1)
                {
                    bool flag = this.struct0_0.method_1(class0_0) == 0u;
                    uint result;
                    if (flag)
                    {
                        result = this.struct1_0[(int)((uint)((UIntPtr)uint_1))].method_1(class0_0);
                    }
                    else
                    {
                        uint num = 8u;
                        bool flag2 = this.struct0_1.method_1(class0_0) == 0u;
                        if (flag2)
                        {
                            num += this.struct1_1[(int)((uint)((UIntPtr)uint_1))].method_1(class0_0);
                        }
                        else
                        {
                            num += 8u;
                            num += this.struct1_2.method_1(class0_0);
                        }
                        result = num;
                    }
                    return result;
                }
                internal Class2()
                {
                }
            }
            internal class Class3
            {
                internal struct Struct2
                {
                    internal Struct0[] struct0_0;
                    internal void method_0()
                    {
                        this.struct0_0 = new Struct0[768];
                    }
                    internal void method_1()
                    {
                        for (int i = 0; i < 768; i++)
                        {
                            this.struct0_0[i].method_0();
                        }
                    }
                    internal byte method_2(Class0 class0_0)
                    {
                        uint num = 1u;
                        do
                        {
                            num = (num << 1 | this.struct0_0[(int)((uint)((UIntPtr)num))].method_1(class0_0));
                        }
                        while (num < 256u);
                        return (byte)num;
                    }
                    internal byte method_3(Class0 class0_0, byte byte_0)
                    {
                        uint num = 1u;
                        while (true)
                        {
                            uint num2 = (uint)(byte_0 >> 7 & 1);
                            byte_0 = (byte)(byte_0 << 1);
                            uint num3 = this.struct0_0[(int)((uint)((UIntPtr)((1u + num2 << 8) + num)))].method_1(class0_0);
                            num = (num << 1 | num3);
                            bool flag = num2 != num3;
                            if (flag)
                            {
                                break;
                            }
                            bool flag2 = num >= 256u;
                            if (flag2)
                            {
                                goto Block_2;
                            }
                        }
                        while (num < 256u)
                        {
                            num = (num << 1 | this.struct0_0[(int)((uint)((UIntPtr)num))].method_1(class0_0));
                        }
                        Block_2:
                        return (byte)num;
                    }
                }
                internal Class1.Class3.Struct2[] struct2_0;
                internal int int_0;
                internal int int_1;
                internal uint uint_0;
                internal void method_0(int int_2, int int_3)
                {
                    bool flag = this.struct2_0 != null && this.int_1 == int_3 && this.int_0 == int_2;
                    if (!flag)
                    {
                        this.int_0 = int_2;
                        this.uint_0 = (1u << int_2) - 1u;
                        this.int_1 = int_3;
                        uint num = 1u << this.int_1 + this.int_0;
                        this.struct2_0 = new Class1.Class3.Struct2[num];
                        for (uint num2 = 0u; num2 < num; num2 += 1u)
                        {
                            this.struct2_0[(int)((uint)((UIntPtr)num2))].method_0();
                        }
                    }
                }
                internal void method_1()
                {
                    uint num = 1u << this.int_1 + this.int_0;
                    for (uint num2 = 0u; num2 < num; num2 += 1u)
                    {
                        this.struct2_0[(int)((uint)((UIntPtr)num2))].method_1();
                    }
                }
                internal uint method_2(uint uint_1, byte byte_0)
                {
                    return ((uint_1 & this.uint_0) << this.int_1) + (uint)(byte_0 >> 8 - this.int_1);
                }
                internal byte method_3(Class0 class0_0, uint uint_1, byte byte_0)
                {
                    return this.struct2_0[(int)((uint)((UIntPtr)this.method_2(uint_1, byte_0)))].method_2(class0_0);
                }
                internal byte method_4(Class0 class0_0, uint uint_1, byte byte_0, byte byte_1)
                {
                    return this.struct2_0[(int)((uint)((UIntPtr)this.method_2(uint_1, byte_0)))].method_3(class0_0, byte_1);
                }
                internal Class3()
                {
                }
            }
            internal readonly Struct0[] struct0_0 = new Struct0[192];
            internal readonly Struct0[] struct0_1 = new Struct0[192];
            internal readonly Struct0[] struct0_2 = new Struct0[12];
            internal readonly Struct0[] struct0_3 = new Struct0[12];
            internal readonly Struct0[] struct0_4 = new Struct0[12];
            internal readonly Struct0[] struct0_5 = new Struct0[12];
            internal readonly Class1.Class2 class2_0 = new Class1.Class2();
            internal readonly Class1.Class3 class3_0 = new Class1.Class3();
            internal readonly Class4 class4_0 = new Class4();
            internal readonly Struct0[] struct0_6 = new Struct0[114];
            internal readonly Struct1[] struct1_0 = new Struct1[4];
            internal readonly Class0 class0_0 = new Class0();
            internal readonly Class1.Class2 class2_1 = new Class1.Class2();
            internal bool bool_0;
            internal uint uint_0;
            internal uint uint_1;
            internal Struct1 struct1_1 = new Struct1(4);
            internal uint uint_2;
            internal Class1()
            {
                this.uint_0 = 4294967295u;
                int num = 0;
                while ((long)num < 4L)
                {
                    this.struct1_0[num] = new Struct1(6);
                    num++;
                }
            }
            internal void method_0(uint uint_3)
            {
                bool flag = this.uint_0 != uint_3;
                if (flag)
                {
                    this.uint_0 = uint_3;
                    this.uint_1 = Math.Max(this.uint_0, 1u);
                    uint uint_4 = Math.Max(this.uint_1, 4096u);
                    this.class4_0.method_0(uint_4);
                }
            }
            internal void method_1(int int_0, int int_1)
            {
                this.class3_0.method_0(int_0, int_1);
            }
            internal void method_2(int int_0)
            {
                uint num = 1u << int_0;
                this.class2_0.method_0(num);
                this.class2_1.method_0(num);
                this.uint_2 = num - 1u;
            }
            internal void method_3(Stream stream_0, Stream stream_1)
            {
                this.class0_0.method_0(stream_0);
                this.class4_0.method_1(stream_1, this.bool_0);
                for (uint num = 0u; num < 12u; num += 1u)
                {
                    for (uint num2 = 0u; num2 <= this.uint_2; num2 += 1u)
                    {
                        uint value = (num << 4) + num2;
                        this.struct0_0[(int)((uint)((UIntPtr)value))].method_0();
                        this.struct0_1[(int)((uint)((UIntPtr)value))].method_0();
                    }
                    this.struct0_2[(int)((uint)((UIntPtr)num))].method_0();
                    this.struct0_3[(int)((uint)((UIntPtr)num))].method_0();
                    this.struct0_4[(int)((uint)((UIntPtr)num))].method_0();
                    this.struct0_5[(int)((uint)((UIntPtr)num))].method_0();
                }
                this.class3_0.method_1();
                for (uint num3 = 0u; num3 < 4u; num3 += 1u)
                {
                    this.struct1_0[(int)((uint)((UIntPtr)num3))].method_0();
                }
                for (uint num4 = 0u; num4 < 114u; num4 += 1u)
                {
                    this.struct0_6[(int)((uint)((UIntPtr)num4))].method_0();
                }
                this.class2_0.method_1();
                this.class2_1.method_1();
                this.struct1_1.method_0();
            }
            internal void method_4(Stream stream_0, Stream stream_1, long long_0, long long_1)
            {
                this.method_3(stream_0, stream_1);
                Struct3 @struct = default(Struct3);
                @struct.method_0();
                uint num = 0u;
                uint num2 = 0u;
                uint num3 = 0u;
                uint num4 = 0u;
                ulong num5 = 0uL;
                bool flag = 0L < long_1;
                if (flag)
                {
                    this.struct0_0[(int)((uint)((UIntPtr)(@struct.uint_0 << 4)))].method_1(this.class0_0);
                    @struct.method_1();
                    byte byte_ = this.class3_0.method_3(this.class0_0, 0u, 0);
                    this.class4_0.method_5(byte_);
                    num5 += 1uL;
                }
                while (num5 < (ulong)long_1)
                {
                    uint num6 = (uint)num5 & this.uint_2;
                    bool flag2 = this.struct0_0[(int)((uint)((UIntPtr)((@struct.uint_0 << 4) + num6)))].method_1(this.class0_0) == 0u;
                    if (flag2)
                    {
                        byte byte_2 = this.class4_0.method_6(0u);
                        bool flag3 = !@struct.method_5();
                        byte byte_3;
                        if (flag3)
                        {
                            byte_3 = this.class3_0.method_4(this.class0_0, (uint)num5, byte_2, this.class4_0.method_6(num));
                        }
                        else
                        {
                            byte_3 = this.class3_0.method_3(this.class0_0, (uint)num5, byte_2);
                        }
                        this.class4_0.method_5(byte_3);
                        @struct.method_1();
                        num5 += 1uL;
                    }
                    else
                    {
                        bool flag4 = this.struct0_2[(int)((uint)((UIntPtr)@struct.uint_0))].method_1(this.class0_0) == 1u;
                        uint num8;
                        if (flag4)
                        {
                            bool flag5 = this.struct0_3[(int)((uint)((UIntPtr)@struct.uint_0))].method_1(this.class0_0) == 0u;
                            if (flag5)
                            {
                                bool flag6 = this.struct0_1[(int)((uint)((UIntPtr)((@struct.uint_0 << 4) + num6)))].method_1(this.class0_0) == 0u;
                                if (flag6)
                                {
                                    @struct.method_4();
                                    this.class4_0.method_5(this.class4_0.method_6(num));
                                    num5 += 1uL;
                                    continue;
                                }
                            }
                            else
                            {
                                bool flag7 = this.struct0_4[(int)((uint)((UIntPtr)@struct.uint_0))].method_1(this.class0_0) == 0u;
                                uint num7;
                                if (flag7)
                                {
                                    num7 = num2;
                                }
                                else
                                {
                                    bool flag8 = this.struct0_5[(int)((uint)((UIntPtr)@struct.uint_0))].method_1(this.class0_0) == 0u;
                                    if (flag8)
                                    {
                                        num7 = num3;
                                    }
                                    else
                                    {
                                        num7 = num4;
                                        num4 = num3;
                                    }
                                    num3 = num2;
                                }
                                num2 = num;
                                num = num7;
                            }
                            num8 = this.class2_1.method_2(this.class0_0, num6) + 2u;
                            @struct.method_3();
                        }
                        else
                        {
                            num4 = num3;
                            num3 = num2;
                            num2 = num;
                            num8 = 2u + this.class2_0.method_2(this.class0_0, num6);
                            @struct.method_2();
                            uint num9 = this.struct1_0[(int)((uint)((UIntPtr)Class1.smethod_0(num8)))].method_1(this.class0_0);
                            bool flag9 = num9 >= 4u;
                            if (flag9)
                            {
                                int num10 = (int)((num9 >> 1) - 1u);
                                num = (2u | (num9 & 1u)) << num10;
                                bool flag10 = num9 < 14u;
                                if (flag10)
                                {
                                    num += Struct1.smethod_0(this.struct0_6, num - num9 - 1u, this.class0_0, num10);
                                }
                                else
                                {
                                    num += this.class0_0.method_3(num10 - 4) << 4;
                                    num += this.struct1_1.method_2(this.class0_0);
                                }
                            }
                            else
                            {
                                num = num9;
                            }
                        }
                        bool flag11 = ((ulong)num >= num5 || num >= this.uint_1) && num == 4294967295u;
                        if (flag11)
                        {
                            break;
                        }
                        this.class4_0.method_4(num, num8);
                        num5 += (ulong)num8;
                    }
                }
                this.class4_0.method_3();
                this.class4_0.method_2();
                this.class0_0.method_1();
            }
            internal void method_5(byte[] byte_0)
            {
                int int_ = (int)(byte_0[0] % 9);
                int num = (int)(byte_0[0] / 9);
                int int_2 = num % 5;
                int int_3 = num / 5;
                uint num2 = 0u;
                for (int i = 0; i < 4; i++)
                {
                    num2 += (uint)((uint)byte_0[1 + i] << i * 8);
                }
                this.method_0(num2);
                this.method_1(int_2, int_);
                this.method_2(int_3);
            }
            internal static uint smethod_0(uint uint_3)
            {
                uint_3 -= 2u;
                bool flag = uint_3 < 4u;
                uint result;
                if (flag)
                {
                    result = uint_3;
                }
                else
                {
                    result = 3u;
                }
                return result;
            }
        }
        internal class Class4
        {
            internal byte[] byte_0;
            internal uint uint_0;
            internal Stream stream_0;
            internal uint uint_1;
            internal uint uint_2;
            internal void method_0(uint uint_3)
            {
                bool flag = this.uint_2 != uint_3;
                if (flag)
                {
                    this.byte_0 = new byte[uint_3];
                }
                this.uint_2 = uint_3;
                this.uint_0 = 0u;
                this.uint_1 = 0u;
            }
            internal void method_1(Stream stream_1, bool bool_0)
            {
                this.method_2();
                this.stream_0 = stream_1;
                bool flag = !bool_0;
                if (flag)
                {
                    this.uint_1 = 0u;
                    this.uint_0 = 0u;
                }
            }
            internal void method_2()
            {
                this.method_3();
                this.stream_0 = null;
                Buffer.BlockCopy(new byte[this.byte_0.Length], 0, this.byte_0, 0, this.byte_0.Length);
            }
            internal void method_3()
            {
                uint num = this.uint_0 - this.uint_1;
                bool flag = num == 0u;
                if (!flag)
                {
                    this.stream_0.Write(this.byte_0, (int)this.uint_1, (int)num);
                    bool flag2 = this.uint_0 >= this.uint_2;
                    if (flag2)
                    {
                        this.uint_0 = 0u;
                    }
                    this.uint_1 = this.uint_0;
                }
            }
            internal void method_4(uint uint_3, uint uint_4)
            {
                uint num = this.uint_0 - uint_3 - 1u;
                bool flag = num >= this.uint_2;
                if (flag)
                {
                    num += this.uint_2;
                }
                while (uint_4 > 0u)
                {
                    bool flag2 = num >= this.uint_2;
                    if (flag2)
                    {
                        num = 0u;
                    }
                    byte[] arg_75_0 = this.byte_0;
                    uint num2 = this.uint_0;
                    this.uint_0 = num2 + 1u;
                    arg_75_0[(int)((uint)((UIntPtr)num2))] = this.byte_0[(int)((uint)((UIntPtr)(num++)))];
                    bool flag3 = this.uint_0 >= this.uint_2;
                    if (flag3)
                    {
                        this.method_3();
                    }
                    uint_4 -= 1u;
                }
            }
            internal void method_5(byte byte_1)
            {
                byte[] arg_23_0 = this.byte_0;
                uint num = this.uint_0;
                this.uint_0 = num + 1u;
                arg_23_0[(int)((uint)((UIntPtr)num))] = byte_1;
                bool flag = this.uint_0 >= this.uint_2;
                if (flag)
                {
                    this.method_3();
                }
            }
            internal byte method_6(uint uint_3)
            {
                uint num = this.uint_0 - uint_3 - 1u;
                bool flag = num >= this.uint_2;
                if (flag)
                {
                    num += this.uint_2;
                }
                return this.byte_0[(int)((uint)((UIntPtr)num))];
            }
            internal Class4()
            {
            }
        }
        internal struct Struct0
        {
            internal uint uint_0;
            internal void method_0()
            {
                this.uint_0 = 1024u;
            }
            internal uint method_1(Class0 class0_0)
            {
                uint num = (class0_0.uint_1 >> 11) * this.uint_0;
                bool flag = class0_0.uint_0 < num;
                uint result;
                if (flag)
                {
                    class0_0.uint_1 = num;
                    this.uint_0 += 2048u - this.uint_0 >> 5;
                    bool flag2 = class0_0.uint_1 < 16777216u;
                    if (flag2)
                    {
                        class0_0.uint_0 = (class0_0.uint_0 << 8 | (uint)((byte)class0_0.stream_0.ReadByte()));
                        class0_0.uint_1 <<= 8;
                    }
                    result = 0u;
                }
                else
                {
                    class0_0.uint_1 -= num;
                    class0_0.uint_0 -= num;
                    this.uint_0 -= this.uint_0 >> 5;
                    bool flag3 = class0_0.uint_1 < 16777216u;
                    if (flag3)
                    {
                        class0_0.uint_0 = (class0_0.uint_0 << 8 | (uint)((byte)class0_0.stream_0.ReadByte()));
                        class0_0.uint_1 <<= 8;
                    }
                    result = 1u;
                }
                return result;
            }
        }
        internal struct Struct1
        {
            internal readonly Struct0[] struct0_0;
            internal readonly int int_0;
            internal Struct1(int int_1)
            {
                this.int_0 = int_1;
                this.struct0_0 = new Struct0[1 << int_1];
            }
            internal void method_0()
            {
                uint num = 1u;
                while ((ulong)num < 1uL << (this.int_0 & 31))
                {
                    this.struct0_0[(int)((uint)((UIntPtr)num))].method_0();
                    num += 1u;
                }
            }
            internal uint method_1(Class0 class0_0)
            {
                uint num = 1u;
                for (int i = this.int_0; i > 0; i--)
                {
                    num = (num << 1) + this.struct0_0[(int)((uint)((UIntPtr)num))].method_1(class0_0);
                }
                return num - (1u << this.int_0);
            }
            internal uint method_2(Class0 class0_0)
            {
                uint num = 1u;
                uint num2 = 0u;
                for (int i = 0; i < this.int_0; i++)
                {
                    uint num3 = this.struct0_0[(int)((uint)((UIntPtr)num))].method_1(class0_0);
                    num <<= 1;
                    num += num3;
                    num2 |= num3 << i;
                }
                return num2;
            }
            internal static uint smethod_0(Struct0[] struct0_1, uint uint_0, Class0 class0_0, int int_1)
            {
                uint num = 1u;
                uint num2 = 0u;
                for (int i = 0; i < int_1; i++)
                {
                    uint num3 = struct0_1[(int)((uint)((UIntPtr)(uint_0 + num)))].method_1(class0_0);
                    num <<= 1;
                    num += num3;
                    num2 |= num3 << i;
                }
                return num2;
            }
        }
        internal struct Struct3
        {
            internal uint uint_0;
            internal void method_0()
            {
                this.uint_0 = 0u;
            }
            internal void method_1()
            {
                bool flag = this.uint_0 < 4u;
                if (flag)
                {
                    this.uint_0 = 0u;
                }
                else
                {
                    bool flag2 = this.uint_0 < 10u;
                    if (flag2)
                    {
                        this.uint_0 -= 3u;
                    }
                    else
                    {
                        this.uint_0 -= 6u;
                    }
                }
            }
            internal void method_2()
            {
                this.uint_0 = ((this.uint_0 < 7u) ? 7u : 10u);
            }
            internal void method_3()
            {
                this.uint_0 = ((this.uint_0 < 7u) ? 8u : 11u);
            }
            internal void method_4()
            {
                this.uint_0 = ((this.uint_0 < 7u) ? 9u : 11u);
            }
            internal bool method_5()
            {
                return this.uint_0 < 7u;
            }
        }

        internal class Class0
        {
            internal uint uint_0;
            internal uint uint_1;
            internal Stream stream_0;
            internal void method_0(Stream stream_1)
            {
                this.stream_0 = stream_1;
                this.uint_0 = 0u;
                this.uint_1 = 4294967295u;
                for (int i = 0; i < 5; i++)
                {
                    this.uint_0 = (this.uint_0 << 8 | (uint)((byte)this.stream_0.ReadByte()));
                }
            }
            internal void method_1()
            {
                this.stream_0 = null;
            }
            internal void method_2()
            {
                while (this.uint_1 < 16777216u)
                {
                    this.uint_0 = (this.uint_0 << 8 | (uint)((byte)this.stream_0.ReadByte()));
                    this.uint_1 <<= 8;
                }
            }
            internal uint method_3(int int_0)
            {
                uint num = this.uint_1;
                uint num2 = this.uint_0;
                uint num3 = 0u;
                for (int i = int_0; i > 0; i--)
                {
                    num >>= 1;
                    uint num4 = num2 - num >> 31;
                    num2 -= (num & num4 - 1u);
                    num3 = (num3 << 1 | 1u - num4);
                    bool flag = num < 16777216u;
                    if (flag)
                    {
                        num2 = (num2 << 8 | (uint)((byte)this.stream_0.ReadByte()));
                        num <<= 8;
                    }
                }
                this.uint_1 = num;
                this.uint_0 = num2;
                return num3;
            }
            internal Class0()
            {
            }
        }

        internal static byte[] smethod_0(byte[] byte_0)
        {
            MemoryStream memoryStream = new MemoryStream(byte_0);
            Class1 @class = new Class1();
            byte[] array = new byte[5];
            memoryStream.Read(array, 0, 5);
            @class.method_5(array);
            long num = 0L;
            for (int i = 0; i < 8; i++)
            {
                int num2 = memoryStream.ReadByte();
                num |= (long)((long)((ulong)((byte)num2)) << 8 * i);
            }
            byte[] array2 = new byte[(int)num];
            MemoryStream stream_ = new MemoryStream(array2, true);
            long long_ = memoryStream.Length - 13L;
            @class.method_4(memoryStream, stream_, long_, num);
            return array2;
        }

        public static uint getfirst(ModuleDefMD module)
        {
            uint result;
            foreach (TypeDef current in module.Types)
            {
                foreach (MethodDef current2 in current.Methods)
                {
                    if (current2.HasBody)
                    {
                        if (current2.Body.Instructions.Count > 100)
                        {
                            int local = current2.Body.Variables.Count;
                            if(local == 13)
                            {
                                
                                    if (current2.Body.Instructions[5].OpCode == OpCodes.Ldtoken)
                                    {
                                        if (current2.Body.Instructions[6].OpCode == OpCodes.Call)
                                        {
                                            if (current2.Body.Instructions[0].OpCode == OpCodes.Ldc_I4)
                                            {
                                            for (int i = 0; i < current2.Body.Instructions.Count; i++)
                                            {
                                                if (current2.Body.Instructions[i].OpCode == OpCodes.Callvirt && current2.Body.Instructions[i].Operand.ToString().Contains("AssemblyResolve") && current2.Body.Instructions[i-1].OpCode == OpCodes.Newobj)
                                                {
                                                    result = (uint)current2.Body.Instructions[0].GetLdcI4Value();
                                                    cctormethod = current2;
                                                    return result;
                                                }
                                            }
                                            }
                                        }
                                    }
                            }
                        }
                    }
                }
            }
            result = 0u;
            return result;
        }
        public static MethodDef cctormethod = null;
        public static uint getSecond(ModuleDefMD module)
        {
            uint result;
            for (int i = 5; i < cctormethod.Body.Instructions.Count; i++)
            {
                if(cctormethod.Body.Instructions[i].OpCode == OpCodes.Ldc_I4)
                {
                    result = (uint)cctormethod.Body.Instructions[i].GetLdcI4Value();
                    return result;
                }     
            }
            result = 0u;
            return result;
        }
        public static uint[] GetUintValue(ModuleDefMD module)
        {
            uint[] result;
            for (int i = 0; i < cctormethod.Body.Instructions.Count; i++)
            {
                bool flag2 = cctormethod.Body.Instructions[i].OpCode == OpCodes.Ldtoken;
                if (flag2)
                {
                    bool flag3 = cctormethod.Body.Instructions[i + 1].OpCode == OpCodes.Call;
                    if (flag3)
                    {
                        FieldDef fieldDef = (FieldDef)cctormethod.Body.Instructions[i].Operand;
                        byte[] initialValue = fieldDef.InitialValue;
                        uint[] array = new uint[initialValue.Length / 4];
                        Buffer.BlockCopy(initialValue, 0, array, 0, initialValue.Length);
                        result = array;
                        return result;
                    }
                }
            }
            result = null;
            return result;
        }
        public static void initialize(uint valuee, uint[] arrayy, uint secondvalue, ModuleDefMD module)
        {
            uint num = valuee;
            uint[] array = arrayy;
            uint[] array2 = new uint[16];
            uint num2 = secondvalue;
            for (int i = 0; i < 16; i++)
            {
                num2 ^= num2 >> 13;
                num2 ^= num2 << 25;
                num2 ^= num2 >> 27;
                array2[i] = num2;
            }
            int num3 = 0;
            int num4 = 0;
            uint[] array3 = new uint[16];
            byte[] array4 = new byte[num * 4u];
            while ((long)num3 < (long)((ulong)num))
            {
                for (int j = 0; j < 16; j++)
                {
                    array3[j] = array[num3 + j];
                }
                array3[0] = (array3[0] ^ array2[0]);
                array3[1] = (array3[1] ^ array2[1]);
                array3[2] = (array3[2] ^ array2[2]);
                array3[3] = (array3[3] ^ array2[3]);
                array3[4] = (array3[4] ^ array2[4]);
                array3[5] = (array3[5] ^ array2[5]);
                array3[6] = (array3[6] ^ array2[6]);
                array3[7] = (array3[7] ^ array2[7]);
                array3[8] = (array3[8] ^ array2[8]);
                array3[9] = (array3[9] ^ array2[9]);
                array3[10] = (array3[10] ^ array2[10]);
                array3[11] = (array3[11] ^ array2[11]);
                array3[12] = (array3[12] ^ array2[12]);
                array3[13] = (array3[13] ^ array2[13]);
                array3[14] = (array3[14] ^ array2[14]);
                array3[15] = (array3[15] ^ array2[15]);
                for (int k = 0; k < 16; k++)
                {
                    uint num5 = array3[k];
                    array4[num4++] = (byte)num5;
                    array4[num4++] = (byte)(num5 >> 8);
                    array4[num4++] = (byte)(num5 >> 16);
                    array4[num4++] = (byte)(num5 >> 24);
                    array2[k] ^= num5;
                }
                num3 += 16;
            }
            assembly_0 = Assembly.Load(smethod_0(array4));
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(smethod_2);
            Module[] modules = assembly_0.GetModules();
            module.Resources.Clear();
            Module[] mod = modules;
            for (int l = 0; l < mod.Length; l++)
            {
                Module module2 = mod[l];
                string[] manifestResourceNames = module2.Assembly.GetManifestResourceNames();
                string[] array5 = manifestResourceNames;
                for (int m = 0; m < array5.Length; m++)
                {
                    string text = array5[m];
                    Stream manifestResourceStream = module2.Assembly.GetManifestResourceStream(text);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        manifestResourceStream.CopyTo(memoryStream);
                        byte[] data = memoryStream.ToArray();
                        module.Resources.Add(new EmbeddedResource(text, data, ManifestResourceAttributes.Public));
                    }
                }
            }
        }
        internal static Assembly assembly_0;
        public static MethodDef methodd = null;

        internal static Assembly smethod_2(object object_0, ResolveEventArgs resolveEventArgs_0)
        {
            bool flag = assembly_0.FullName == resolveEventArgs_0.Name;
            Assembly result;
            if (flag)
            {
                result = assembly_0;
            }
            else
            {
                result = null;
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
