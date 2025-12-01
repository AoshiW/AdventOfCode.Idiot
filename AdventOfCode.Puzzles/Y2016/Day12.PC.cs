using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2016;

public partial class Day12
{
    public class PC
    {
        (Action<ValueOrRegister[]>, ValueOrRegister[]) PareLine(ReadOnlySpan<char> span)
        {
            var enumerator = span.EnumerateSlices(" ");
            enumerator.MoveNext();
            var instruction = enumerator.Current;
            var arg = new List<ValueOrRegister>();
            while (enumerator.MoveNext())
            {
                if (int.TryParse(enumerator.Current, out var value))
                    arg.Add(new(value));
                else
                    arg.Add(new(enumerator.Current[0]));
            }
            var argArr = arg.ToArray();
            return instruction switch
            {
                "inc" => (_incDel, argArr),
                "dec" => (_decDel, argArr),
                "jnz" => (_jnzDel, argArr),
                "cpy" => (_cpyDel, argArr),
                "tgl" => (_tglDel, argArr),
                _ => throw new NotImplementedException(),
            };
        }

        int _counter = 0;

        public Dictionary<char, int> Registers { get; } = new();
        readonly List<(Action<ValueOrRegister[]>, ValueOrRegister[])> _instructionss = new();

        public PC(ReadOnlySpan<char> span)
        {
            _incDel = Inc;
            _decDel = Dec;
            _cpyDel = Cpy;
            _tglDel= Tgl;
            _jnzDel = Jnz;
            foreach (var item in span.EnumerateLines())
            {
                _instructionss.Add(PareLine(item));
            }
        }

        public void Execute()
        {
            while (_counter < _instructionss.Count)
            {
                var inn = _instructionss[_counter];
                inn.Item1(inn.Item2);
            }
        }
        private readonly Action<ValueOrRegister[]> _incDel;
        private readonly Action<ValueOrRegister[]> _decDel;
        private readonly Action<ValueOrRegister[]> _cpyDel;
        private readonly Action<ValueOrRegister[]> _tglDel;
        private readonly Action<ValueOrRegister[]> _jnzDel;
        void Inc(ValueOrRegister[] args)
        {
            ref var val = ref CollectionsMarshal.GetValueRefOrAddDefault(Registers, (char)args[0].Value, out _);
            val++;
            _counter++;
        }

        void Dec(ValueOrRegister[] args)
        {
            ref var val = ref CollectionsMarshal.GetValueRefOrAddDefault(Registers, (char)args[0].Value, out _);
            val--;
            _counter++;
        }

        void Cpy(ValueOrRegister[] args)
        {
            if (args[1].IsRegister)
            {
                ref var reg = ref CollectionsMarshal.GetValueRefOrAddDefault(Registers, (char)args[1].Value, out _);
                reg = args[0].GetValue(Registers);
            }
            _counter++;
        }
        void Jnz(ValueOrRegister[] args)
        {
            if (args[0].GetValue(Registers) != 0)
            {
                _counter += args[1].GetValue(Registers);
            }
            else _counter++;
        }

        /// <remarks>For <see cref="Day23"/></remarks>
        void Tgl(ValueOrRegister[] args)
        {
            var offset = args[0].GetValue(Registers) + _counter;
            if (offset < _instructionss.Count)
            {
                var item = _instructionss[offset];
                if (item.Item1 == _incDel)
                {
                    _instructionss[offset] = (_decDel, item.Item2);
                }
                else if (item.Item2.Length == 1)
                {
                    _instructionss[offset] = (_incDel, item.Item2);
                }
                else if (item.Item1 == _jnzDel)
                {
                    _instructionss[offset] = (_cpyDel, item.Item2);
                }
                else if (item.Item2.Length == 2)
                {
                    _instructionss[offset] = (_jnzDel, item.Item2); ;
                }
            }
            _counter++;
        }

        void Out(ValueOrRegister[] args)
        {

        }

        class ValueOrRegister
        {
            public readonly bool IsRegister;
            public readonly int Value;

            public ValueOrRegister(char reg)
            {
                IsRegister = true;
                Value = reg;
            }
            public ValueOrRegister(int val)
            {
                IsRegister = false;
                Value = val;
            }

            public int GetValue(Dictionary<char, int> registers)
            {
                if (IsRegister)
                {
                    return registers.GetValueOrDefault((char)Value);
                }
                return Value;
            }
        }
    }
}
