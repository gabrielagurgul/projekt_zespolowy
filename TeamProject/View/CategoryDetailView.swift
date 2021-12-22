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
		VStack {
			PieChart(chartData: PieChartData(dataSets: PieDataSet(dataPoints: [PieChartDataPoint(value: 1),PieChartDataPoint(value: 2)], legendTitle: "")))
			List(viewModel.budgets) { budget in
				HStack {
					Text("\(budget.description) Jakis produkt")
					Spacer()
					Text("\(Double.random(in: 0...10))")
				}
			}
			HStack {
				Button {
					viewModel.addView.toggle()
				} label: {
					Text("Add")
				}
				Spacer()
				Button {
					viewModel.predictView.toggle()
				} label: {
					Text("Predicate")
				}
			}
			.padding([.leading,.trailing])
		}
		.onAppear {viewModel.getBudgets()}
		.overlay(LoadingView(isLoading: $viewModel.isLoading))
		.sheet(isPresented: $viewModel.addView) {
			print("znikam")
		}
	content: {
		NavigationView {
			Text("Add")
				.toolbar {
					ToolbarItem(placement: .navigationBarLeading) {
						Button("Dissmis") {
							viewModel.addView.toggle()
						}
					}
				}
		}
	}
	.sheet(isPresented: $viewModel.predictView) {
		NavigationView {
			Text("Predict")
				.toolbar {
					ToolbarItem(placement: .navigationBarLeading) {
						Button("Dissmis") {
							viewModel.predictView.toggle()
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
