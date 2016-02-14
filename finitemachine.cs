using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Solution {
	class InvalidMachineTransitionException: Exception {}

	class State {
		private readonly int state;
		public bool final {set; get;} = false;
		private Dictionary<char, int> transitions = new Dictionary<char,int>();

		public State(int state) {
			this.state = state;
		}

		public void AddTransition(char input, int nextState) {
			transitions.Add(input, nextState);
		}

		public int nextState(char input) {
			if(!transitions.ContainsKey(input)) {
				throw new InvalidMachineTransitionException();
			}
			return transitions[input];
		}
	}

	class FiniteMachine {
		private IList<State> states = new List<State>();
		private int currentState = 0;

		public FiniteMachine(int num) {
			for(int i = 0; i < num; i++) {
				states.Add(new State(num));
			}			
		}

		public void SetFinal(int n) {
			states[n].final = true;
		}

		public void AddTransition(int state, char input, int nextState) {
			states[state].AddTransition(input, nextState);
		}

		public bool isValid(string str) {
			try {
				foreach(char c in str) {
					currentState = states[currentState].nextState(c);
				}

				return states[currentState].final;
			} catch(InvalidMachineTransitionException) {
				return false;
			}
		}

		public void reset() {
			currentState = 0;
		}
	}


	static void Main(string[] args) {	
		int Z = readNum();
		for(int i = 0; i < Z; i++) {
			int statesCount = readNum();
			FiniteMachine fm = new FiniteMachine(statesCount);

			// states list is ignored for now
			Console.ReadLine();

			int count = readNum();
			foreach(string n in Console.ReadLine().Split(' ')) {
				fm.SetFinal(int.Parse(n));
			}

			int fns = readNum();
			for(int x = 0; x < fns; x++) {
				var parts = Console.ReadLine().Split(' ');

				fm.AddTransition(int.Parse(parts[0]), parts[1][0], int.Parse(parts[2]));
			}

			int examples = readNum();
			for(int x = 0; x < examples; x++) {
				fm.reset();
				Console.WriteLine(fm.isValid(Console.ReadLine()) ? "ANO" : "NE");
			}
		}

	}

	static int readNum() {
		return int.Parse(Console.ReadLine());
	}

}