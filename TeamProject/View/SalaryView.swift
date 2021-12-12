//
//  SalaryView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 11/12/2021.
//

import SwiftUI

struct SalaryView: View {
	@State var budget: String = "13.01"
	@State var salary: String = "15.01"
	var body: some View {
		VStack {
			Spacer()
			Section("Budżet") {
				TextField("Budżet", text: $budget)
					.textFieldStyle(RoundedBorderTextFieldStyle())
			}
			Section("Miesięczny dochód") {
				TextField("Miesięczny dochód", text: $salary)
					.textFieldStyle(RoundedBorderTextFieldStyle())
			}
			Button("Dodaj budżet") {
				
			}.buttonStyle(.automatic)
			Spacer()
			Button("Pomiń") {
				
			}
		}
		.padding()
		.background {Image("p2")}
	}
}

struct SalaryView_Previews: PreviewProvider {
	static var previews: some View {
		SalaryView()
	}
}
