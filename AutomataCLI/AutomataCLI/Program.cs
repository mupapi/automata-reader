﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomataCLI.Struct;
using System.Diagnostics;
using AutomataCLI.Serialization;

namespace AutomataCLI {
    class Program {
        static void Main(string[] args) {
            String newLine = "\n";
            Stopwatch watch = new Stopwatch();

            Console.WriteLine("Test 1:");

            watch.Start();

            Automata automata = new Automata();

            String input = "123";

            automata.SetSymbols(new String[] {
                "1", "2", "3"
            });

            State state0 = new State("Q0", false),
                  state1 = new State("Q1", false),
                  state2 = new State("Q2", true),
                  state3 = new State("Q3", false),
                  state4 = new State("Q4", false);

            automata.SetStates(new State[]{
                state0,
                state1,
                state2,
                state3,
                state4
            });

            automata.SetInitialState(state1);

            automata.SetTransitions(new Transition[] {
                new Transition(state0, "1", state0),
                new Transition(state0, "2", state0),
                new Transition(state0, "3", state0),
                new Transition(state0, "1", state1),
                new Transition(state0, "2", state3),
                new Transition(state0, "3", state4),

                new Transition(state1, "1", state1),
                new Transition(state1, "2", state1),
                new Transition(state1, "3", state1),
                new Transition(state1, "1", state2),

                new Transition(state3, "1", state3),
                new Transition(state3, "2", state3),
                new Transition(state3, "3", state3),
                new Transition(state3, "2", state2),

                new Transition(state4, "1", state4),
                new Transition(state4, "2", state4),
                new Transition(state4, "3", state4),
                new Transition(state4, "3", state2),
            });

            Console.WriteLine(new AutomataReader(automata).Matches(input));
            watch.Stop();
            Console.WriteLine($"Execution time: {watch.Elapsed.TotalMilliseconds.ToString()}ms");


            Console.WriteLine("Test 2:");

            watch.Reset();
            watch.Start();
            String input2 =
                "AFN" + newLine +
                "q0,q1,q2" + newLine +
                "a, b" + newLine +
                "q0" + newLine +
                "q2" + newLine +
                "(q0, a, q2)" + newLine +
                "(q0, a, q1)" + newLine +
                "(q1, a, q2)" + newLine +
                "(q1, b, q2)" + newLine +
                "(q1, b, q0)" + newLine +
                "(q2, a, q2)" + newLine +
                "####" + newLine;

            Automata automata2 = AutomataSerializer.Deserialize(input2);

            watch.Stop();

            Console.WriteLine("Deserialized Automata:");
            Console.WriteLine(automata2);
            Console.WriteLine($"Deserialization time: {watch.Elapsed.TotalMilliseconds.ToString()}ms");

            Console.ReadKey();
        }
    }
}