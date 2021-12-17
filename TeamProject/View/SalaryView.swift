//
//  SalaryView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 11/12/2021.
//

import SwiftUI

struct SalaryView: View {
	@State var budget: Int = 100
	@State var salary: Int = 100
	@State var didFinish = false
	@State var isLoading = false
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
						isLoading = true
						do {
							_  = try await addBudget(Budget(id: 0, description: "Budżet", amount: budget, addedDate: Date()), category: .Budget)
							_ = try await addBudget(Budget(id: 0, description: "Miesięczny przychód", amount: salary, addedDate: Date()), category: .Month_income)
						}
						catch let apiError {
							error = apiError
							showAlert = true
							isLoading = false
						}
						isLoading = false
						didFinish = true
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
			.overlay(LoadingView(isLoading: $isLoading))
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
	@Binding var container: Int
	var body: some View {
		Section {
			TextField(name, text: Binding(get: {String(container)}, set: {container = Int($0) ?? 0}))
				.textFieldStyle(RoundedBorderTextFieldStyle())
				.keyboardType(.numberPad)
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
