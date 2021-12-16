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
			.overlay(loadingOverlay)
			.background(Image("p2"))
		}
		.navigationViewStyle(.stack)
	}
	
	@ViewBuilder
	private var loadingOverlay: some View {
		if viewModel.isLoading {
			ProgressView()
				.progressViewStyle(CircularProgressViewStyle(tint: .blue))
				.padding(50)
				.background(.regularMaterial)
				.mask(RoundedRectangle(cornerRadius: 8))
				.overlay(alignment: .bottom) {
					Text("Please wait")
				}
		}
	}
	
}

//struct ContentView_Previews: PreviewProvider {
//	static var previews: some View {
//		NavigationView {
//			ContentView()
//				.environmentObject(BudgetViewModel(fetcher: BudgetFetcherImpl()))
//		}
//		
//	}
//}
