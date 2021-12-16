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
		NavigationView {
			VStack {
				Spacer()
				TextFieldSection(name: "Budget", description: "Budget", container: $budget)
				TextFieldSection(name: "Monthly Salary", description: "Monthly Salary", container: $salary)
				Spacer()
				Button {
					
				} label: {
					Text("Forward")
						.font(.title)
						.frame(maxWidth: .infinity)
				}
				.buttonStyle(BorderedProminentButtonStyle())
				.buttonBorderShape(ButtonBorderShape.capsule)
				.tint(Color.green)
				Spacer()
				NavigationLink{
					ContentView()
						.environmentObject(BudgetViewModel())
						.navigationBarHidden(true)
				} label: {
					Text("Skip")
				}
				
			}
			.padding()
			.background {Image("p2")}
		}
	}
}

struct SalaryView_Previews: PreviewProvider {
	static var previews: some View {
		SalaryView()
	}
}

struct TextFieldSection: View {
	let name: String
	let description: String
	@Binding var container: String
	var body: some View {
		Section {
			TextField(name, text: $container)
				.textFieldStyle(RoundedBorderTextFieldStyle())
		} header: {
			TextFieldTitle(description: description)
		}
	}
}

struct TextFieldTitle: View {
	let description: String
	var body: some View {
		HStack {
			Text(description)
				.font(.title)
				.bold()
			Spacer()
		}
	}
}
