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
	@State var didFinish = false
	@State var showAlert = false
	@State var error: Error?
	var body: some View {
		NavigationView {
			VStack {
				Spacer()
				TextFieldSection(name: "Budget", description: "Budget", container: $budget)
				TextFieldSection(name: "Monthly Salary", description: "Monthly Salary", container: $salary)
				Spacer()
				Button {
					Task {
						do {
							let server  = try await addUserBudget(ammount: Double(budget) ?? 0.0)
//							let serverSalary = try await addUserSalary(ammount: Double(salary) ?? 0)
							
							didFinish = true
						}
						catch let apiError {
							error = apiError
							showAlert = true
						}
					}
				} label: {
					Text("Forward")
						.font(.title)
						.frame(maxWidth: .infinity)
				}
				.alert("Error", isPresented: $showAlert) {
					Button("Cancel",role: .cancel) {}
				} message: {
					Text(error?.localizedDescription ?? "")
				}
				.buttonStyle(BorderedProminentButtonStyle())
				.buttonBorderShape(ButtonBorderShape.capsule)
				.tint(Color.green)
				NavigationLink("Hidden", isActive: $didFinish) {
					ContentView()
						.environmentObject(BudgetViewModel())
						.navigationBarHidden(true)
				}
				.hidden()
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
				.border(.green.opacity(0.3))
				.shadow(radius: 5)
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
