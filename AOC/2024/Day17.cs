using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day17 : Day
    {
        uint A = 0;
        uint B = 0;
        uint C = 0;
        uint instruction = 0;

        public string Task1()
        {
            List<int> instructions = InputUtils.ReadString("2024/Files/Day17.txt").Split(",").Select(int.Parse).ToList();
            instruction = 0;
            A = 46187030;
            B = 0;
            C = 0;

            List<uint> output = new List<uint>();
            RunProgram(instructions, output);

            return string.Join(",", output);
        }

        private void RunProgram(List<int> instructions, List<uint> output)
        {
            while (instruction < instructions.Count)
            {
                int opcode = instructions[(int)instruction];
                uint operand = (uint)instructions[(int)instruction + 1];

                switch (opcode)
                {
                    case 0:
                        Adv(operand); break;
                    case 1:
                        Bxl(operand); break;
                    case 2:
                        Bst(operand); break;
                    case 3:
                        Jnz(operand); break;
                    case 4:
                        Bxc(operand); break;
                    case 5:
                        output.Add(Out(operand)); break;
                    case 6:
                        Bdv(operand); break;
                    case 7:
                        Cdv(operand); break;
                }
                if (opcode == 3 && A != 0)
                {
                    continue;
                }
                instruction += 2;
            }
        }

        public string Task2()
        {
            string input = InputUtils.ReadString("2024/Files/Day17.txt");
            List<int> instructions = InputUtils.ReadString("2024/Files/Day17.txt").Split(",").Select(int.Parse).ToList();

            long A = 0;
            long modulus = 1;
            for(int i = 0; i < instructions.Count; i++)
            {
                int output = instructions[i]; // Erwartete Ausgabe an Position i
                long powerOfTwo = 1L << i; // 2^i (entspricht der Division in out)

                long target = (output * powerOfTwo) % (powerOfTwo * 8);

                A = SolveChineseRemainder(A, modulus, target, powerOfTwo * 8);
                modulus *= (powerOfTwo * 8); 
            }

            return A.ToString();
        }

        private long SolveChineseRemainder(long a1, long m1, long a2, long m2)
        {
            long m1Inverse = ModInverse(m1, m2);
            long m2Inverse = ModInverse(m2, m1);

            long result = (a1 * m2 * m2Inverse + a2 * m1 * m1Inverse) % (m1 * m2);
            return (result + m1 * m2) % (m1 * m2); 
        }

        private long ModInverse(long a, long m)
        {
            if (m == 0)
                throw new ArgumentException("Modulus darf nicht 0 sein.");

            long gcd = GCD(a, m);
            if (gcd != 1)
                throw new InvalidOperationException("a und m sind nicht teilerfremd. Modulares Inverses existiert nicht.");

            long m0 = m, t, q;
            long x0 = 0, x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                q = a / m;  // Division durch Null prüfen
                t = m;

                m = a % m;
                a = t;
                t = x0;

                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0) x1 += m0; // Sicherstellen, dass das Ergebnis positiv ist
            return x1;
        }

        private long GCD(long a, long b)
        {
            while (b != 0)
            {
                long t = b;
                b = a % b;
                a = t;
            }
            return a;
        }


        public uint GetValueFromComboOperand(uint operand)
        {
            switch (operand)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return operand;
                case 4:
                    return A;
                case 5:
                    return B;
                case 6:
                    return C;
                default:
                    return 0;
            }
        }

        //Operands
        public void Adv(uint operand)
        {
            A = A / (uint)Math.Pow(2, GetValueFromComboOperand(operand));
        }

        public void Bxl(uint operand)
        {
            B = B ^ operand;
        }

        public void Bst(uint operand)
        {
            uint mod = GetValueFromComboOperand(operand) % 8;
            B = mod & 0x7;
        }

        public void Jnz(uint operand)
        {
            if (A == 0) return;
            else
            {
                instruction = operand;
            }
        }

        public void Bxc(uint operand)
        {
            B = B ^ C;
        }

        public uint Out(uint operand)
        {
            return GetValueFromComboOperand(operand)  % 8;
        }

        public void Bdv(uint operand)
        {
            B = A / (uint)Math.Pow(2, GetValueFromComboOperand(operand));
        }

        public void Cdv(uint operand)
        {
            C = A / (uint)Math.Pow(2, GetValueFromComboOperand(operand));
        }

    }
}
