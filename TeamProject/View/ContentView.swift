//
//  ContentView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct ContentView: View {
	
	@EnvironmentObject var viewModel: BudgetViewModel
	
	let columns = [
		GridItem(.adaptive(minimum: 160))
	]
	var body: some View {
		NavigationView {
			ScrollView {
				ForEach(viewModel.budgetCategories) { budgetType in
					if budgetType.id == 10 || budgetType.id == 11 {
						CashView(budgetType: budgetType)
							.padding(.horizontal)
					}
				}
				.padding([.bottom],48)
				LazyVGrid(columns: columns, spacing: 48){
					ForEach(viewModel.budgetCategories) { budgetType in
						if viewModel.hiddenTheSalaryAndBudget(budgetType.id) {
							NavigationLink {
								CategoryDetailView(budgetType: budgetType)
							} label: {
								CategoryView(budgetType: budgetType)
							}
							.buttonStyle(PlainButtonStyle())
						}
					}
				}
			}
			.navigationTitle("Analizator budżetu")
			.onAppear(perform: {viewModel.getCategories()})
			.overlay(LoadingView(isLoading: $viewModel.isLoading))
			.background(Image("p2"))
		}
		.navigationViewStyle(.stack)
	}
}
