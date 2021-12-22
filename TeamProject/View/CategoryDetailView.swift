//
//  CategoryDetailView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 10/12/2021.
//

import SwiftUI
import SwiftUICharts

struct CategoryDetailView: View {
	@ObservedObject var viewModel: CategoryDetailViewModel
	
	var body: some View {
		ZStack {
			VStack {
				PieChart(chartData: viewModel.chartData)
				List(viewModel.budgets, id: \.id) { budget in
					HStack {
						Text(budget.description)
						Spacer()
						Text("\(budget.amount)")
					}
				}
			}
			VStack {
				Spacer()
				HStack {
					Spacer()
					Button {
						viewModel.addView.toggle()
					} label: {
						Image(systemName: "plus")
							.foregroundColor(.white)
							.frame(width: 50, height: 50)
							.background(Circle().foregroundColor(.green))
					}.buttonStyle(PlainButtonStyle())
				}
				.padding()
			}
		}
		.onAppear {viewModel.getBudgets()}
		.overlay(LoadingView(isLoading: $viewModel.isLoading))
		.sheet(isPresented: $viewModel.addView) {
			viewModel.getBudgets()
			viewModel.data = Budget.BudgetAPI()
		}
	content: {
		NavigationView {
			AddView(budget: $viewModel.data)
				.toolbar {
					ToolbarItem(placement: .navigationBarLeading) {
						Button("Dissmis") {
							viewModel.addView.toggle()
						}
					}
					ToolbarItem(placement: .navigationBarTrailing) {
						Button("Add") {
							viewModel.add()
							viewModel.addView.toggle()
						}
					}
				}
		}
	}
	}
}

struct CategoryDetailView_Previews: PreviewProvider {
	static var previews: some View {
		CategoryDetailView(viewModel: CategoryDetailViewModel(budgetType: BudgetType.budgetTypeMock))
	}
}


struct AddView: View {
	@Binding var budget: Budget.BudgetAPI
	var body: some View {
		VStack {
			Text("Add new expense")
				.font(.largeTitle)
			TextField("Description", text: $budget.description)
				.textFieldStyle(RoundedBorderTextFieldStyle())
			TextField("Ammount", text: Binding(get: {String(budget.amount)}, set: {budget.amount = Int($0) ?? 0}))
				.textFieldStyle(RoundedBorderTextFieldStyle())
		}
		.padding([.leading, .trailing])
		.background {Image("p2")}
		
	}
}
