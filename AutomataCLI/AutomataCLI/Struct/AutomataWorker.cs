using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomataCLI.Struct {
    public class AutomataWorker {
        private Automata Automata {get; set;}
        private State CurrentState {get; set;}
        private List<String> InputSymbols {get; set;}
        private State LastState {get; set;}
        private AutomataWorker LastWorker {get; set;}

        public AutomataWorker(Automata automata, State currentState, List<String> inputSymbols){
            this.Automata     = automata;
            this.CurrentState = currentState;
            this.InputSymbols = inputSymbols;
        }

        private Boolean Work(){

            var possibleTransitions = new List<Transition>();
            var remainingSymbols    = new List<String>(InputSymbols);

            for(int i = 0; i < InputSymbols.Count; i++){

                var currentSymbol = InputSymbols[i];
                possibleTransitions = this.Automata.GetTransitions().ToList().Where(
                    x => (
                        x.Input == currentSymbol &&
                        x.From  == this.CurrentState
                    )
                ).ToList();

                int TransitionsQuantity = possibleTransitions.Count;

                if(TransitionsQuantity == 0) {
                    return false;
                }

                remainingSymbols.RemoveAt(i);
                this.CurrentState = possibleTransitions[0].To;
                this.LastState    = possibleTransitions[0].From;

                if(TransitionsQuantity >= 1){
                    summonWorkers(possibleTransitions.GetRange(1, TransitionsQuantity - 1), remainingSymbols);
                }
            }
            return true;
        }
        public void summonWorkers(List<Transition> possibleTransitions, List<String> remainingSymbols) {

            foreach(Transition transition in possibleTransitions) {
                var newWorker = new AutomataWorker(this.Automata, transition.To, remainingSymbols);
                Boolean result =  newWorker.Work();
            }
        }
    }
}