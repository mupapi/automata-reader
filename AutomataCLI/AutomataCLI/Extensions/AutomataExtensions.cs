using System;
using System.Collections.Generic;
using System.Linq;
using AutomataCLI.Exceptions;
using AutomataCLI.Struct;
using AutomataCLI.Utils;

namespace AutomataCLI.Extensions {
    public static class AutomataExtensions {
        public static IEnumerable<Transition> GetTransitionsFromState(this Automata automata, State state) {
            automata.EnsureStateIsValid(state);
            automata.EnsureContainsState(state);

            State automataStateInstance = automata.GetStateLike(state);
            return automata.GetTransitions().ToList().Where(
                x => x.From == automataStateInstance
            );
        }

        public static IEnumerable<Transition> GetTransitionsFromState(this Automata automata, State state, String symbol) {
            automata.EnsureStateIsValid(state);
            automata.EnsureSymbolIsValid(symbol);
            automata.EnsureContainsState(state);
            if (symbol != Automata.SYMBOL_SPONTANEOUS_TRANSITION) {
                automata.EnsureContainsSymbol(symbol);
            }

            State automataStateInstance = automata.GetStateLike(state);
            return automata.GetTransitions().ToList().Where(
                x => x.From  == automataStateInstance &&
                     x.Input == symbol
            );
        }

        public static IEnumerable<Transition> GetTransitionsToState(this Automata automata, State state) {
            automata.EnsureStateIsValid(state);
            automata.EnsureContainsState(state);

            State automataStateInstance = automata.GetStateLike(state);
            return automata.GetTransitions().ToList().Where(
                x => x.To == automataStateInstance
            );
        }

        public static IEnumerable<Transition> GetTransitionsToState(this Automata automata, State state, String symbol) {
            automata.EnsureStateIsValid(state);
            automata.EnsureSymbolIsValid(symbol);
            automata.EnsureContainsState(state);
            if (symbol != Automata.SYMBOL_SPONTANEOUS_TRANSITION) {
                automata.EnsureContainsSymbol(symbol);
            }

            State automataStateInstance = automata.GetStateLike(state);
            return automata.GetTransitions().ToList().Where(
                x => x.To    == automataStateInstance &&
                     x.Input == symbol
            );
        }

        public static IEnumerable<Transition> GetTransitionsWithSymbol(this Automata automata, String symbol) {
            automata.EnsureSymbolIsValid(symbol);
            if (symbol != Automata.SYMBOL_SPONTANEOUS_TRANSITION) {
                automata.EnsureContainsSymbol(symbol);
            }

            return automata.GetTransitions().ToList().Where(
                x => x.Input == symbol
            );
        }

        public static void EnsureContainsSymbol(this Automata automata, String symbol) {
            if (!automata.ContainsSymbol(symbol)) {
                throw new InvalidValueException(
                    symbol
                );
            }
        }

        public static void EnsureContainsState(this Automata automata, State state) {
            automata.EnsureContainsState(state?.Name);
        }

        public static void EnsureContainsState(this Automata automata, String state) {
            if (!automata.ContainsState(state)){
                throw new InvalidValueException(
                    state,
                    typeof(State)
                );
            }
        }

        public static void EnsureContainsTransition(this Automata automata, Transition transition) {
            automata.EnsureContainsTransition(transition?.From, transition?.Input, transition?.To);
        }

        public static void EnsureContainsTransition(this Automata automata, State stateFrom, String input, State stateTo) {
            automata.EnsureContainsTransition(stateFrom?.Name, input, stateTo?.Name);
        }

        public static void EnsureContainsTransition(this Automata automata, String stateFrom, String input, String stateTo) {
            if (!automata.ContainsTransition(stateFrom, input, stateTo)) {
                throw new InvalidValueException(
                    $"({stateFrom}, {input}, {stateTo})",
                    typeof(Transition)
                );
            }
        }

        public static void EnsureNotContainsSymbol(this Automata automata, String symbol) {
            if (automata.ContainsSymbol(symbol)) {
                throw new DuplicateValueException(
                    symbol
                );
            }
        }

        public static void EnsureNotContainsState(this Automata automata, State state) {
            automata.EnsureNotContainsState(state?.Name);
        }

        public static void EnsureNotContainsState(this Automata automata, String state) {
            if (automata.ContainsState(state)) {
                throw new DuplicateValueException(
                    state,
                    typeof(State)
                );
            }
        }

        public static void EnsureNotContainsTransition(this Automata automata, Transition transition) {
            automata.EnsureTransitionNotDuplicate(transition?.From, transition?.Input, transition?.To);
        }

        public static void EnsureTransitionNotDuplicate(this Automata automata, State stateFrom, String input, State stateTo) {
            automata.EnsureNotContainsTransition(stateFrom?.Name, input, stateTo?.Name);
        }

        public static void EnsureNotContainsTransition(this Automata automata, String stateFrom, String input, String stateTo) {
            if (automata.ContainsTransition(stateFrom, input, stateTo)) {
                throw new DuplicateValueException(
                    $"({stateFrom}, {input}, {stateTo})",
                    typeof(Transition)
                );
            }
        }

        public static void EnsureSymbolIsValid(this Automata automata, String symbol) {
            ValidationUtils.EnsureNotNull(
                symbol, new InvalidValueException(
                    symbol
                )
            );
            if (symbol.Length < 1) {
                throw new InvalidValueException(
                    symbol
                );
            }
        }

        public static void EnsureSymbolIsNotSpontaneous(this Automata automata, String symbol) {
            ValidationUtils.EnsureNotEquals(
                Automata.SYMBOL_SPONTANEOUS_TRANSITION,
                symbol, new InvalidValueException(
                    symbol
                )
            );
        }

        public static void EnsureSymbolIsSpontaneous(this Automata automata, String symbol) {
            ValidationUtils.EnsureEquals(
                Automata.SYMBOL_SPONTANEOUS_TRANSITION,
                symbol, new InvalidValueException(
                    symbol
                )
            );
        }

        public static void EnsureStateIsValid(this Automata automata, State state) {
            automata.EnsureStateIsValid(state?.Name);
        }

        public static void EnsureStateIsValid(this Automata automata, String state) {
            ValidationUtils.EnsureNotNullEmptyOrWhitespace(
                state, new InvalidValueException(
                    state,
                    typeof(State)
                )
            );
        }

        public static void EnsureTransitionIsValid(this Automata automata, Transition transition) {
            ValidationUtils.EnsureNotNull(
                transition, new InvalidValueException(
                    transition,
                    typeof(Transition)
                )
            );

            automata.EnsureTransitionIsValid(transition?.From, transition?.Input, transition?.To);
        }

        public static void EnsureTransitionIsValid(this Automata automata, State stateFrom, String input, State stateTo) {
            automata.EnsureTransitionIsValid(stateFrom?.Name, input, stateTo?.Name);
        }

        public static void EnsureTransitionIsValid(this Automata automata, String stateFrom, String input, String stateTo) {
            automata.EnsureSymbolIsValid(input);
            automata.EnsureStateIsValid(stateFrom);
            automata.EnsureStateIsValid(stateTo);
        }

        public static void EnsureAutomataIsOfType(this Automata automata, AutomataType automataType, Exception throwException) {
            ValidationUtils.EnsureEquals(
                automataType.ToString(), 
                automata.GetAutomataType().ToString(), 
                throwException
            );
        }
    }
}